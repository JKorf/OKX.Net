using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Clients.MessageHandlers;
using OKX.Net.Interfaces.Clients.UnifiedApi;
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

    public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new OKXSocketMessageHandler();

    public IOKXSocketClientUnifiedApiShared SharedClient => this;

    /// <inheritdoc />
    public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        => OKXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

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
    public override ReadOnlySpan<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlySpan<byte> data)
    {
        if (type != WebSocketMessageType.Binary)
            return data;

        return data.DecompressGzip();
    }
}
