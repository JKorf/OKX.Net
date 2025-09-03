using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;
using OKX.Net.Objects.Sockets.Subscriptions;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
internal partial class OKXSocketClientUnifiedApi : SocketApiClient, IOKXSocketClientUnifiedApi
{
    private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
    private static readonly MessagePath _eventPath = MessagePath.Get().Property("event");
    private static readonly MessagePath _errorMsgPath = MessagePath.Get().Property("msg");
    private static readonly MessagePath _channelPath = MessagePath.Get().Property("arg").Property("channel");
    private static readonly MessagePath _instIdPath = MessagePath.Get().Property("arg").Property("instId");
    private static readonly MessagePath _instTypePath = MessagePath.Get().Property("arg").Property("instType");
    private static readonly MessagePath _instFamilyPath = MessagePath.Get().Property("arg").Property("instFamily");

    public new OKXSocketOptions ClientOptions => (OKXSocketOptions)base.ClientOptions;

    protected override ErrorMapping ErrorMapping => OKXErrors.ErrorMapping;

    /// <inheritdoc />
    public IOKXSocketClientUnifiedApiAccount Account { get; }
    /// <inheritdoc />
    public IOKXSocketClientUnifiedApiExchangeData ExchangeData { get; }
    /// <inheritdoc />
    public IOKXSocketClientUnifiedApiTrading Trading { get; }

    private readonly bool _demoTrading;

    #region ctor

    internal OKXSocketClientUnifiedApi(ILogger logger, OKXSocketOptions options) :
        base(logger, options.Environment.SocketAddress, options, options.UnifiedOptions)
    {
        Account = new OKXSocketClientUnifiedApiAccount(logger, this);
        ExchangeData = new OKXSocketClientUnifiedApiExchangeData(logger, this);
        Trading = new OKXSocketClientUnifiedApiTrading(logger, this);

        ProcessUnparsableMessages = true;

        _demoTrading = options.Environment.Name == TradeEnvironmentNames.Testnet;

        AddSystemSubscription(new OKXConnCountSubscription(_logger));

        RegisterPeriodicQuery(
            "Ping",
            TimeSpan.FromSeconds(20),
            x => new OKXPingQuery(),
            (connection, result) =>
            {
                if (result.Error?.ErrorType == ErrorType.Timeout)
                {
                    // Ping timeout, reconnect
                    _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                    _ = connection.TriggerReconnectAsync();
                }
            });

        SetDedicatedConnection(GetUri("/ws/v5/private"), true);
    }
    #endregion

    /// <inheritdoc />
    protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(OKXExchange._serializerContext));
    /// <inheritdoc />
    protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(OKXExchange._serializerContext));

    public IOKXSocketClientUnifiedApiShared SharedClient => this;

    /// <inheritdoc />
    public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        => OKXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

    /// <inheritdoc />
    public override string GetListenerIdentifier(IMessageAccessor message)
    {
        if (!message.IsValid)
            return "pong";

        var id = message.GetValue<string>(_idPath);
        if (id != null)
            return id;

        var evnt = message.GetValue<string>(_eventPath);
        if (evnt == "error")
        {
            var errorMsg = message.GetValue<string?>(_errorMsgPath);
            if (errorMsg != null && errorMsg.StartsWith("Wrong URL or channel:"))
            {
                // Try parse which sub request produced the error so it can be linked to the specific request
                var subParams = errorMsg.Substring(21, errorMsg.IndexOf(" doesn't exist") - 21);
                var subParamsSplit = subParams.Split(',');
                return evnt + string.Join("", subParamsSplit.Select(x => x.Split(':').Last())).ToLowerInvariant();
            }

            return evnt;
        }

        var channel = message.GetValue<string>(_channelPath);
        var instType = message.GetValue<string>(_instTypePath);
        var instId = message.GetValue<string>(_instIdPath);
        var instFamily = message.GetValue<string>(_instFamilyPath);
        return $"{evnt}{channel?.ToLowerInvariant()}{instType?.ToLowerInvariant()}{instFamily?.ToLowerInvariant()}{instId?.ToLowerInvariant()}";
    }

    /// <inheritdoc />
    protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
    {
        var okxAuthProvider = (OKXAuthenticationProvider)AuthenticationProvider!;
        var timestamp = (DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow) / 1000).Value.ToString(CultureInfo.InvariantCulture);
        var signature = okxAuthProvider.SignWebsocket(timestamp);
        var request = new OKXSocketAuthRequest
        {
            Op = "login",
            Args =
            [
                new OKXSocketAuthArgs
                {
                    ApiKey = okxAuthProvider.ApiKey,
                    Passphrase = okxAuthProvider.Pass!,
                    Timestamp = timestamp,
                    Sign = signature,
                }
            ]
        };
        return Task.FromResult<Query?>(new OKXQuery(this, request, false));
    }

    internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
    {
        return SubscribeAsync(url, subscription, ct);
    }

    /// <inheritdoc />
    protected override Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authentication)
    {
        if (_demoTrading && !address.EndsWith("brokerId=9999"))
            address += "?brokerId=9999";

        return Task.FromResult(new CallResult<string?>(address));
    }

    internal async Task<CallResult<T>> QueryInternalAsync<T>(string url, string operation, Dictionary<string, object> parameters, bool authenticated, int weight, CancellationToken ct = default)
    {
        var query = new OKXIdQuery<T>(this, operation, new object[] { parameters }, authenticated, weight);
        var result = await QueryAsync(url, query, ct).ConfigureAwait(false);
        if (!result)
            return result.AsError<T>(result.Error!);

        return result.As(result.Data.Data.First());
    }

    internal async Task<CallResult<T[]>> QueryInternalAsync<T>(string url, string operation, IEnumerable<object> data, bool authenticated, int weight, CancellationToken ct = default)
    {
        var query = new OKXIdQuery<T>(this, operation, data.ToArray(), authenticated, weight);
        var result = await QueryAsync(url, query, ct).ConfigureAwait(false);
        if (!result)
            return result.AsError<T[]>(result.Error!);

        return result.As(result.Data.Data);
    }

    internal string GetUri(string path) => BaseAddress.Trim(['/']) + path;

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OKXAuthenticationProvider(credentials);

    /// <inheritdoc />
    public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
    {
        if (type != WebSocketMessageType.Binary)
            return data;

        using var decompressedStream = new MemoryStream();
        using var deflateStream = new GZipStream(new MemoryStream(data.ToArray()), CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        return new ReadOnlyMemory<byte>(decompressedStream.GetBuffer(), 0, (int)decompressedStream.Length);
    }
}
