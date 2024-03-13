using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApi : SocketApiClient, IOKXSocketClientUnifiedApi
{
    private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
    private static readonly MessagePath _eventPath = MessagePath.Get().Property("event");
    private static readonly MessagePath _channelPath = MessagePath.Get().Property("arg").Property("channel");
    private static readonly MessagePath _instIdPath = MessagePath.Get().Property("arg").Property("instId");
    private static readonly MessagePath _instTypePath = MessagePath.Get().Property("arg").Property("instType");
    private static readonly MessagePath _instFamilyPath = MessagePath.Get().Property("arg").Property("instFamily");

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
        Account = new OKXSocketClientUnifiedApiAccount(logger, this);
        ExchangeData = new OKXSocketClientUnifiedApiExchangeData(logger, this);
        Trading = new OKXSocketClientUnifiedApiTrading(logger, this);

        _ref = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "078ee129065aBCDE";

        _demoTrading = options.Environment.EnvironmentName == TradeEnvironmentNames.Testnet;

        RegisterPeriodicQuery("Ping", TimeSpan.FromSeconds(20), x => new OKXPingQuery(), null);
    }
    #endregion

    /// <inheritdoc />
    public override string GetListenerIdentifier(IMessageAccessor message)
    {
        if (!message.IsJson)
            return "pong";

        var id = message.GetValue<string>(_idPath);
        if (id != null)
            return id;

        var evnt = message.GetValue<string>(_eventPath);
        var channel = message.GetValue<string>(_channelPath);
        var instType = message.GetValue<string>(_instTypePath);
        var instId = message.GetValue<string>(_instIdPath);
        var instFamily = message.GetValue<string>(_instFamilyPath);
        return $"{evnt}{channel?.ToLowerInvariant()}{instType?.ToLowerInvariant()}{instFamily?.ToLowerInvariant()}{instId?.ToLowerInvariant()}";
    }

    /// <inheritdoc />
    protected override Query GetAuthenticationRequest()
    {
        var okxAuthProvider = (OKXAuthenticationProvider)AuthenticationProvider!;
        var timestamp = (DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow) / 1000).Value.ToString(CultureInfo.InvariantCulture);
        var signature = okxAuthProvider.SignWebsocket(timestamp);
        var request = new OKXSocketAuthRequest
        {
            Op = "login",
            Args = new List<OKXSocketAuthArgs>
            {
                new OKXSocketAuthArgs
                {
                    ApiKey = okxAuthProvider.ApiKey,
                    Passphrase = okxAuthProvider.Passphrase,
                    Timestamp = timestamp,
                    Sign = signature,
                }
            }
        };
        return new OKXQuery(request, false);
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

    internal async Task<CallResult<T>> QueryInternalAsync<T>(string url, string operation, Dictionary<string, object> parameters, bool authenticated, int weight)
    {
        var query = new OKXIdQuery<T>(operation, new object[] { parameters }, authenticated, weight);
        var result = await QueryAsync(url, query).ConfigureAwait(false);
        if (!result)
            return result.AsError<T>(result.Error!);

        return result.As(result.Data.Data.First());
    }

    internal async Task<CallResult<IEnumerable<T>>> QueryInternalAsync<T>(string url, string operation, IEnumerable<object> data, bool authenticated, int weight)
    {
        var query = new OKXIdQuery<T>(operation, data, authenticated, weight);
        var result = await QueryAsync(url, query).ConfigureAwait(false);
        if (!result)
            return result.AsError<IEnumerable<T>>(result.Error!);

        return result.As(result.Data.Data);
    }

    internal string GetUri(string path) => BaseAddress.Trim(new[] { '/' }) + path;

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OKXAuthenticationProvider((OKXApiCredentials)credentials);

    /// <inheritdoc />
    public override ReadOnlyMemory<byte> PreprocessStreamMessage(WebSocketMessageType type, ReadOnlyMemory<byte> data)
    {
        if (type != WebSocketMessageType.Binary)
            return data;

        var decompressedStream = new MemoryStream();
        using var deflateStream = new GZipStream(new MemoryStream(data.ToArray()), CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;
        return new ReadOnlyMemory<byte>(decompressedStream.ToArray());
    }
}
