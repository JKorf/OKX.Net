using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Sockets.Subscriptions;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
internal class OKXSocketClientUnifiedApiTrading : IOKXSocketClientUnifiedApiTrading
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
        OrderSide side,
        OrderType type,
        Enums.TradeMode tradeMode,
        decimal quantity,
        decimal? price = null,
        PositionSide? positionSide = null,

        QuickMarginType? quickMarginType = null,
        int? selfTradePreventionId = null,
        SelfTradePreventionMode? selfTradePreventionMode = null,

        string? asset = null,
        QuantityAsset? quantityAsset = null,
        string? clientOrderId = null,
        bool? reduceOnly = null,
        string? tradeQuoteAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol },
            { "tdMode", EnumConverter.GetString(tradeMode) },
            { "side", EnumConverter.GetString(side) },
            { "ordType", EnumConverter.GetString(type) },
            { "sz", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _client.ClientOptions.AllowAppendingClientOrderId);

        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("tag", OKXExchange.ClientOrderId);
        parameters.AddOptionalParameter("posSide", EnumConverter.GetString(positionSide));
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.AddOptionalParameter("stpId", selfTradePreventionId);
        parameters.AddOptionalParameter("stpMode", EnumConverter.GetString(selfTradePreventionMode));
        parameters.AddOptionalParameter("tradeQuoteCcy", tradeQuoteAsset);

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "order", parameters, true, 1, ct).ConfigureAwait(false);
        if (!result)
            return result;

        if (!result.Data.Success)
            return result.AsError<OKXOrderPlaceResponse>(new ServerError(result.Data.Code, _client.GetErrorInfo(result.Data.Code, result.Data.Message), null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders)
        {
            order.Tag = OKXExchange.ClientOrderId;
            order.ClientOrderId = LibraryHelpers.ApplyBrokerId(order.ClientOrderId, OKXExchange.ClientOrderId, 32, _client.ClientOptions.AllowAppendingClientOrderId);
        }

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "batch-orders", orders.ToArray(), true, 1, ct).ConfigureAwait(false);
        var ordersResult = new List<CallResult<OKXOrderPlaceResponse>>();
        foreach (var item in result.Data)
        {
            if (item.Code > 0)
                ordersResult.Add(new CallResult<OKXOrderPlaceResponse>(item, null, new ServerError(item.Code, _client.GetErrorInfo(item.Code, item.Message!))));
            else
                ordersResult.Add(new CallResult<OKXOrderPlaceResponse>(item));
        }

        if (ordersResult.All(x => !x.Success))
            return result.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All errors failed")), ordersResult.ToArray());

        return result.As(ordersResult.ToArray());
    }

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        if (clientOrderId != null)
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _client.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol }
        };

        parameters.AddOptionalParameter("ordId", orderId);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var result = await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "cancel-order", parameters, true, 1, ct).ConfigureAwait(false);
        if (!result)
            return result;

        if (!result.Data.Success)
            return result.AsError<OKXOrderCancelResponse>(new ServerError(result.Data.Code, _client.GetErrorInfo(result.Data.Code, result.Data.Message), null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> ordersToCancel, CancellationToken ct = default)
    {
        return await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "batch-cancel-orders", ordersToCancel, true, 1, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<OKXOrderAmendResponse>> AmendOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        CancellationToken ct = default)
    {
        if (clientOrderId != null)
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _client.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newPx", newPrice?.ToString(CultureInfo.InvariantCulture));

        var result = await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "amend-order", parameters, true, 1, ct).ConfigureAwait(false);
        if (!result)
            return result;

        if (!result.Data.Success)
            return result.AsError<OKXOrderAmendResponse>(new ServerError(result.Data.Code, _client.GetErrorInfo(result.Data.Code, result.Data.Message), null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> ordersToCancel, CancellationToken ct = default)
    {
        return await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "batch-amend-orders", ordersToCancel, true, 1, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        bool regularUpdates,
        Action<DataEvent<OKXPosition[]>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXPosition[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "positions",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                    ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
                }
            }, x => onData(x.WithDataTimestamp(x.Data.Any() ? x.Data.Max(x => x.UpdateTime) : default)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(InstrumentType instrumentType,
        string? instrumentFamily,
        Action<DataEvent<OKXPosition>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXPosition[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "liquidation-warning",
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().UpdateTime)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<DataEvent<OKXOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXOrderUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "orders",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().UpdateTime)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
        string? symbol,
        Action<DataEvent<OKXUserTradeUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXUserTradeUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "fills",
                    Symbol = symbol
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Timestamp)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<DataEvent<OKXAlgoOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXAlgoOrderUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "orders-algo",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().UpdateTime)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? algoId,
        Action<DataEvent<OKXAlgoOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXAlgoOrderUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "algo-advance",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    AlgoId = algoId,
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().UpdateTime)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }
}
