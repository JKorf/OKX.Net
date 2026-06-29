using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Clients.MessageHandlers;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Options;
using OKX.Net.Objects.Sockets.Queries;
using OKX.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
internal partial class OKXSocketClientUnifiedApi : SocketApiClient<OKXEnvironment, OKXAuthenticationProvider, OKXCredentials>, IOKXSocketClientUnifiedApi
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

    internal OKXSocketClientUnifiedApi(ILoggerFactory? loggerFactory, OKXSocketOptions options) :
        base(loggerFactory, OKXExchange.Metadata.Id, options.Environment.SocketAddress, options, options.UnifiedOptions)
    {
        Account = new OKXSocketClientUnifiedApiAccount(_logger, this);
        ExchangeData = new OKXSocketClientUnifiedApiExchangeData(_logger, this);
        Trading = new OKXSocketClientUnifiedApiTrading(_logger, this);

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
    {
        if (EnvironmentName == OKXEnvironment.Europe.Name)
            return OKXExchange.FormatSymbolEurope(baseAsset, quoteAsset, tradingMode, deliverTime);
        else
            return OKXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }

    internal Task<WebSocketResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
    {
        return SubscribeAsync(url, subscription, ct);
    }

    /// <inheritdoc />
    protected override Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authentication)
    {
        if (_demoTrading && !address.EndsWith("brokerId=9999"))
            address += "?brokerId=9999";

        return Task.FromResult(CallResult.Ok<string?>(address));
    }

    internal async Task<QueryResult<T>> QueryInternalAsync<T>(string url, string operation, Parameters parameters, bool authenticated, int weight, CancellationToken ct = default)
    {
        var query = new OKXIdQuery<T>(this, operation, new object[] { parameters }, authenticated, weight);
        var result = await QueryAsync(url, query, ct).ConfigureAwait(false);
        if (!result.Success)
            return QueryResult.Fail<T>(result);

        return QueryResult.Ok(result, result.Data.Data.First());
    }

    internal async Task<QueryResult<T[]>> QueryInternalAsync<T>(string url, string operation, IEnumerable<object> data, bool authenticated, int weight, CancellationToken ct = default)
    {
        var query = new OKXIdQuery<T>(this, operation, data.ToArray(), authenticated, weight);
        var result = await QueryAsync(url, query, ct).ConfigureAwait(false);
        if (!result.Success)
            return QueryResult.Fail<T[]>(result);

        return QueryResult.Ok(result, result.Data.Data);
    }

    internal string GetUri(string path) => BaseAddress.Trim('/') + path;

    /// <inheritdoc />
    protected override OKXAuthenticationProvider CreateAuthenticationProvider(OKXCredentials credentials)
        => new OKXAuthenticationProvider(credentials);

    /// <inheritdoc />
    public override ReadOnlySpan<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlySpan<byte> data)
    {
        if (type != WebSocketMessageType.Binary)
            return data;

        return data.DecompressGzip();
    }
}
