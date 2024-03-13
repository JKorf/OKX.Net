using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Sockets.Subscriptions;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApiTrading : IOKXSocketClientUnifiedApiTrading
{
    private readonly OKXSocketClientUnifiedApi _client;

    private readonly ILogger _logger;

    #region ctor

    internal OKXSocketClientUnifiedApiTrading(ILogger logger, OKXSocketClientUnifiedApi client)
    {
        _client = client;
        _logger = logger;
    }
    #endregion

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(string symbol,
        OKXOrderSide side,
        OKXOrderType type,
        OKXTradeMode tradeMode,
        decimal quantity,
        decimal? price = null,
        OKXPositionSide? positionSide = null,

        OKXQuickMarginType? quickMarginType = null,
        int? selfTradePreventionId = null,
        OKXSelfTradePreventionMode? selfTradePreventionMode = null,

        string? asset = null,
        OKXQuantityAsset? quantityAsset = null,
        string? clientOrderId = null,
        bool? reduceOnly = null)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol },
            { "tdMode", EnumConverter.GetString(tradeMode) },
            { "side", EnumConverter.GetString(side) },
            { "ordType", EnumConverter.GetString(type) },
            { "sz", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        clientOrderId = clientOrderId ?? ExchangeHelpers.AppendRandomString(_client._ref, 32);

        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("tag", _client._ref);
        parameters.AddOptionalParameter("posSide", EnumConverter.GetString(positionSide));
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.AddOptionalParameter("stpId", selfTradePreventionId);
        parameters.AddOptionalParameter("stpMode", EnumConverter.GetString(selfTradePreventionMode));

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "order", parameters, true, 1).ConfigureAwait(false);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderPlaceResponse>(new ServerError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<IEnumerable<OKXOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders)
    {
        foreach (var order in orders)
        {
            order.Tag = _client._ref;
            order.ClientOrderId = order.ClientOrderId ?? ExchangeHelpers.AppendRandomString(_client._ref, 32);
        }

        return await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "batch-orders", orders, true, 1).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol }
        };

        parameters.AddOptionalParameter("ordId", orderId);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var result = await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "cancel-order", parameters, true, 1).ConfigureAwait(false);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderCancelResponse>(new ServerError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<IEnumerable<OKXOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> ordersToCancel)
    {
        return await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "batch-cancel-orders", ordersToCancel, true, 1).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<OKXOrderAmendResponse>> AmendOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newPx", newPrice?.ToString(CultureInfo.InvariantCulture));

        var result = await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "amend-order", parameters, true, 1).ConfigureAwait(false);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderAmendResponse>(new ServerError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<IEnumerable<OKXOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> ordersToCancel)
    {
        return await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "batch-amend-orders", ordersToCancel, true, 1).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        bool regularUpdates,
        Action<DataEvent<IEnumerable<OKXPosition>>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXPosition>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "positions",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                    ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
                }
            }, null, onData, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(OKXInstrumentType instrumentType,
        string? instrumentFamily,
        Action<DataEvent<OKXPosition>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXPosition>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "liquidation-warning",
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily
                }
            }, onData, null, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<DataEvent<OKXOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXOrderUpdate>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "orders",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                }
            }, onData, null, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<DataEvent<OKXAlgoOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXAlgoOrderUpdate>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "orders-algo",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                }
            }, onData, null, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? algoId,
        Action<DataEvent<OKXAlgoOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXAlgoOrderUpdate>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "algo-advance",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    AlgoId = algoId,
                }
            }, onData, null, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }
}
