using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Options;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Options;
using System.IO;
using System.IO.Compression;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApi : SocketApiClient, IOKXSocketClientUnifiedApi
{
    /// <inheritdoc />
    public IOKXSocketClientUnifiedApiAccount Account { get; }
    /// <inheritdoc />
    public IOKXSocketClientUnifiedApiExchangeData ExchangeData { get; }
    /// <inheritdoc />
    public IOKXSocketClientUnifiedApiTrading Trading { get; }

    internal readonly string _ref = "078ee129065aBCDE";
    private bool _demoTrading;
    #region ctor

    internal OKXSocketClientUnifiedApi(ILogger logger, OKXSocketOptions options) :
        base(logger, options.Environment.SocketAddress, options, options.UnifiedOptions)
    {
        SetDataInterpreter(DecompressData, HandlePongData);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
        AddGenericHandler("Ping", (a) => { });

        Account = new OKXSocketClientUnifiedApiAccount(logger, this);
        ExchangeData = new OKXSocketClientUnifiedApiExchangeData(logger, this);
        Trading = new OKXSocketClientUnifiedApiTrading(logger, this);

        _ref = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId : "078ee129065aBCDE";

        _demoTrading = options.Environment.EnvironmentName == TradeEnvironmentNames.Testnet;
    }
    #endregion

    internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
    {
        if (_demoTrading)
            url += "?brokerId=9999";

        return SubscribeAsync(url, request, identifier, authenticated, dataHandler, ct);
    }

    internal async Task<CallResult<T>> QueryInternalAsync<T>(string url, string operation, Dictionary<string, object> parameters, bool authenticated, int weight)
    {
        var requestWrapper = new OKXSocketMessage
        {
            Id = ExchangeHelpers.NextId().ToString(),
            Operation = operation,
            Args = new[] { parameters }
        };

        var result = await QueryAsync<IEnumerable<T>>(url, requestWrapper, authenticated, weight).ConfigureAwait(false);
        if (!result)
            return result.AsError<T>(result.Error!);

        return result.As(result.Data.Single());
    }

    internal Task<CallResult<T>> QueryInternalAsync<T>(string url, string operation, IEnumerable<object> data, bool authenticated, int weight)
    {
        var requestWrapper = new OKXSocketMessage
        {
            Id = ExchangeHelpers.NextId().ToString(),
            Operation = operation,
            Args = data
        };

        return QueryAsync<T>(url, requestWrapper, authenticated, weight);
    }

    internal string GetUri(string path) => BaseAddress.Trim(new[] { '/' }) + path;

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OKXAuthenticationProvider((OKXApiCredentials)credentials);

    internal static string DecompressData(byte[] byteData)
    {
        using var decompressedStream = new MemoryStream();
        using var compressedStream = new MemoryStream(byteData);
        using var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;

        using var streamReader = new StreamReader(decompressedStream);
        return streamReader.ReadToEnd();
    }

    internal static string HandlePongData(string data)
    {
        if (data == "pong")
            return "{ \"op\": \"pong\" }";

        return data;
    }


    /// <inheritdoc />
    protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
    {
        // Check Point
        if (AuthenticationProvider == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        var okxAuthProvider = (OKXAuthenticationProvider)AuthenticationProvider;

        var timestamp = (DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow) / 1000).Value.ToString(CultureInfo.InvariantCulture);
        var signature = okxAuthProvider.SignWebsocket(timestamp);
        var request = new OKXSocketAuthRequest(OKXSocketOperation.Login, new OKXSocketAuthRequestArgument
        {
            ApiKey = okxAuthProvider.ApiKey,
            Passphrase = okxAuthProvider.Passphrase,
            Timestamp = timestamp,
            Signature = signature,
        });

        // Try to Login
        var result = new CallResult<bool>(new ServerError("No response from server"));
        await s.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), null, 1, data =>
        {
            var @event = (string?)data["event"];
            if (@event != "login" && @event != "error")
                return false;

            var authResponse = Deserialize<OKXSocketResponse>(data);
            if (!authResponse)
            {
                _logger.Log(LogLevel.Warning, "Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error!);
                return true;
            }

            if (!authResponse.Data.Success)
            {
                _logger.Log(LogLevel.Warning, "Authorization failed: " + authResponse.Data.ErrorMessage);
                result = new CallResult<bool>(new ServerError(int.Parse(authResponse.Data.ErrorCode), authResponse.Data.ErrorMessage!));
                return true;
            }

            _logger.Log(LogLevel.Debug, "Authorization completed");
            result = new CallResult<bool>(true);
            return true;
        }).ConfigureAwait(false);

        return result;
    }

    /// <inheritdoc />
    protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null!;

        var msgId = data["id"];
        if (msgId != null && request is OKXSocketMessage socketMessage)
        {
            if (msgId.ToString() == socketMessage.Id)
            {
                var responseCode = int.Parse(data["code"]!.ToString());
                if (responseCode > 2)
                {
                    callResult = new CallResult<T>(new ServerError(responseCode, data["msg"]!.ToString()));
                    return true;
                }

                var dataObj = data["data"];
                if (dataObj == null)
                {
                    callResult = new CallResult<T>(new ServerError("Unknown response format"));
                    return true;
                }

                var dataArr = (JArray)dataObj;
                callResult = Deserialize<T>(dataArr);
                return true;
            }

            return false;
        }

        // Check for Error
        if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["code"] != null && data["msg"] != null)
        {
            _logger.Log(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
            callResult = new CallResult<T>(new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
            return true;
        }

        // Login Request
        if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
        {
            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                _logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null!;

        // Check for Error
        // 30040: {0} Channel : {1} doesn't exist
        if (message.HasValues && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null)
        {
            _logger.Log(LogLevel.Warning, "Subscription failed: " + (string)message["msg"]!);
            callResult = new CallResult<object>(new ServerError($"{(string)message["code"]!}, {(string)message["msg"]!}"));
            return true;
        }

        // Check for Success
        if (message.HasValues && message["event"] != null && (string)message["event"]! == "subscribe" && message["arg"]?["channel"] != null)
        {
            if (request is OKXSocketRequest socRequest)
            {
                if (socRequest.Arguments.FirstOrDefault().Channel == (string)message["arg"]?["channel"]!)
                {
                    _logger.Log(LogLevel.Debug, "Subscription completed");
                    callResult = new CallResult<object>(true);
                    return true;
                }
            }
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool MessageMatchesHandler(SocketConnection s, JToken message, string identifier)
    {
        if (identifier == "Ping" && message["op"]?.ToString() == "pong")
        {
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool MessageMatchesHandler(SocketConnection s, JToken message, object request)
    {
        // Check Point
        if (message.Type != JTokenType.Object)
            return false;

        if (message["op"]?.ToString() == "pong")
            return false;

        // Socket Request
        if (request is OKXSocketRequest hRequest)
        {
            // Check for Error
            if (message is JObject && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null && message["msg"] != null)
                return false;

            // Check for Channel
            if (hRequest.Operation != OKXSocketOperation.Subscribe || message["arg"]?["channel"] == null)
                return false;

            // Compare Request and Response Arguments
            var reqArg = hRequest.Arguments.FirstOrDefault();
            var resArg = JsonConvert.DeserializeObject<OKXSocketRequestArgument>(message["arg"]!.ToString());

            // Check Data
            if (reqArg.Channel == resArg!.Channel &&
                reqArg.InstrumentFamily == resArg.InstrumentFamily &&
                reqArg.Symbol == resArg.Symbol &&
                reqArg.InstrumentType == resArg.InstrumentType)
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
    {
        if (s == null || s.Request == null)
            return false;

        var request = new OKXSocketRequest(OKXSocketOperation.Unsubscribe, ((OKXSocketRequest)s.Request).Arguments[0]);
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), s, 1, data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            if ((string?)data["event"] == "unsubscribe")
            {
                return (string?)data["arg"]?["channel"] == request.Arguments.FirstOrDefault().Channel;
            }

            return false;
        }).ConfigureAwait(false);
        return false;
    }
}
