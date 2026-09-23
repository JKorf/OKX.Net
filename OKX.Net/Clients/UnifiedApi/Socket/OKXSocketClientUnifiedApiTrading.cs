using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Sockets.Models;
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
    public async Task<QueryResult<OKXOrderPlaceResponse>> PlaceOrderAsync(
        long symbolCode,
        OrderSide side,
        OrderType type,
        TradeMode tradeMode,
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
        decimal? maxSlippagePercentage = null,

        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            { "tdMode", EnumConverter.GetString(tradeMode) },
            { "side", EnumConverter.GetString(side) },
            { "ordType", EnumConverter.GetString(type) },
            { "sz", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        parameters.AddParameter("instIdCode", symbolCode);
        parameters.Add("ccy", asset);
        parameters.Add("clOrdId", clientOrderId);
        parameters.Add("tag", LibraryHelpers.GetClientReference(() => _client.ClientOptions.BrokerId, _client.Exchange));
        parameters.Add("posSide", EnumConverter.GetString(positionSide));
        parameters.Add("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("reduceOnly", reduceOnly);
        parameters.Add("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.Add("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.Add("stpId", selfTradePreventionId);
        parameters.Add("stpMode", EnumConverter.GetString(selfTradePreventionMode));
        parameters.Add("tradeQuoteCcy", tradeQuoteAsset);
        parameters.Add("slippagePct", maxSlippagePercentage?.ToString(CultureInfo.InvariantCulture));

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "order", parameters, true, 1, ct).ConfigureAwait(false);
        if (!result.Success)
            return result;

        if (!result.Data.Success)
            return QueryResult.Fail<OKXOrderPlaceResponse>(result, new ServerError(result.Data.Code, _client.GetErrorInfo(result.Data.Code, result.Data.Message)));

        return result;
    }

    /// <inheritdoc />
    public async Task<QueryResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders)
            order.Tag = LibraryHelpers.GetClientReference(() => _client.ClientOptions.BrokerId, _client.Exchange);        

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "batch-orders", orders.ToArray(), true, 1, ct).ConfigureAwait(false);
        if (!result.Success)
            return QueryResult.Fail<CallResult<OKXOrderPlaceResponse>[]>(result);

        var ordersResult = new List<CallResult<OKXOrderPlaceResponse>>();
        foreach (var item in result.Data)
        {
            if (item.Code > 0)
                ordersResult.Add(CallResult.Fail<OKXOrderPlaceResponse>(new ServerError(item.Code, _client.GetErrorInfo(item.Code, item.Message!))));
            else
                ordersResult.Add(CallResult.Ok(item));
        }

        if (ordersResult.All(x => !x.Success))
            return QueryResult.Fail(result, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All errors failed")), ordersResult.ToArray());

        return QueryResult.Ok(result, ordersResult.ToArray());
    }

    /// <inheritdoc />
    public async Task<QueryResult<OKXOrderCancelResponse>> CancelOrderAsync(long symbolCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.AddParameter("instIdCode", symbolCode);
        parameters.Add("ordId", orderId);
        parameters.Add("clOrdId", clientOrderId);

        var result = await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "cancel-order", parameters, true, 1, ct).ConfigureAwait(false);
        if (!result.Success)
            return result;

        if (!result.Data.Success)
            return QueryResult.Fail<OKXOrderCancelResponse>(result, new ServerError(result.Data.Code, _client.GetErrorInfo(result.Data.Code, result.Data.Message)));

        return result;
    }

    /// <inheritdoc />
    public async Task<QueryResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelSocketRequest> ordersToCancel, CancellationToken ct = default)
    {
        return await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "batch-cancel-orders", ordersToCancel, true, 1, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<OKXOrderAmendResponse>> AmendOrderAsync(
        long symbolCode,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.AddParameter("instIdCode", symbolCode);
        parameters.Add("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("clOrdId", clientOrderId);
        parameters.Add("reqId", requestId);
        parameters.Add("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newPx", newPrice?.ToString(CultureInfo.InvariantCulture));

        var result = await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "amend-order", parameters, true, 1, ct).ConfigureAwait(false);
        if (!result.Success)
            return result;

        if (!result.Data.Success)
            return QueryResult.Fail<OKXOrderAmendResponse>(result, new ServerError(result.Data.Code, _client.GetErrorInfo(result.Data.Code, result.Data.Message)));

        return result;
    }

    /// <inheritdoc />
    public async Task<QueryResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> ordersToCancel, CancellationToken ct = default)
    {
        return await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "batch-amend-orders", ordersToCancel, true, 1, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        bool regularUpdates,
        Action<DataEvent<OKXPosition[]>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXPosition[]>>((receiveTime, originalData, data) =>
        {
            DateTime? timestamp = data.Data.Length > 0 ? data.Data.Max(x => x.UpdateTime) : null;
            if (timestamp != null)
                _client.UpdateTimeOffset(timestamp.Value);

            onData(
                new DataEvent<OKXPosition[]>(OKXExchange.ExchangeName, data.Data, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset())
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXPosition[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "positions",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                    ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebSocketResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(InstrumentType instrumentType,
        string? instrumentFamily,
        Action<DataEvent<OKXPosition>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXPosition[]>>((receiveTime, originalData, data) =>
        {
            if (!data.Data.Any())
                return;

            _client.UpdateTimeOffset(data.Data.Max(x => x.Time));
            foreach (var item in data.Data)
            {
                onData(
                    new DataEvent<OKXPosition>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                        .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithDataTimestamp(item.UpdateTime, _client.GetTimeOffset())
                        .WithStreamId(data.Arg.Channel)
                        .WithSymbol(data.Arg.Symbol)
                    );
            }
        });

        var subscription = new OKXSubscription<OKXPosition[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "liquidation-warning",
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<DataEvent<OKXOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXOrderUpdate[]>>((receiveTime, originalData, data) =>
        {
            if (!data.Data.Any())
                return;

            _client.UpdateTimeOffset(data.Data.Max(x => x.UpdateTime));
            foreach (var item in data.Data)
            {
                onData(
                    new DataEvent<OKXOrderUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                        .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithDataTimestamp(item.UpdateTime, _client.GetTimeOffset())
                        .WithStreamId(data.Arg.Channel)
                        .WithSymbol(data.Arg.Symbol)
                    );
            }
        });

        var subscription = new OKXSubscription<OKXOrderUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "orders",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
        string? symbol,
        Action<DataEvent<OKXUserTradeUpdate>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXUserTradeUpdate[]>>((receiveTime, originalData, data) =>
        {
            if (!data.Data.Any())
                return;

            _client.UpdateTimeOffset(data.Data.Max(x => x.Timestamp));
            foreach (var item in data.Data)
            {

                onData(
                    new DataEvent<OKXUserTradeUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                        .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithDataTimestamp(item.Timestamp, _client.GetTimeOffset())
                        .WithStreamId(data.Arg.Channel)
                        .WithSymbol(data.Arg.Symbol)
                    );
            }
        });

        var subscription = new OKXSubscription<OKXUserTradeUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "fills",
                    Symbol = symbol
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebSocketResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<DataEvent<OKXAlgoOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXAlgoOrderUpdate[]>>((receiveTime, originalData, data) =>
        {
            if (!data.Data.Any())
                return;

            var maxTime = data.Data.Max(x => x.UpdateTime);
            if (maxTime != null)
                _client.UpdateTimeOffset(maxTime.Value);

            foreach (var item in data.Data)
            {
                onData(
                    new DataEvent<OKXAlgoOrderUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                        .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithDataTimestamp(item.UpdateTime, _client.GetTimeOffset())
                        .WithStreamId(data.Arg.Channel)
                        .WithSymbol(data.Arg.Symbol)
                    );
            }
        });

        var subscription = new OKXSubscription<OKXAlgoOrderUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "orders-algo",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    InstrumentFamily = instrumentFamily,
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebSocketResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        InstrumentType instrumentType,
        string? symbol,
        string? algoId,
        Action<DataEvent<OKXAlgoOrderUpdate>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXAlgoOrderUpdate[]>>((receiveTime, originalData, data) =>
        {
            if (!data.Data.Any())
                return;

            var maxTime = data.Data.Max(x => x.UpdateTime);
            if (maxTime != null)
                _client.UpdateTimeOffset(maxTime.Value);

            foreach (var item in data.Data)
            {
                onData(
                    new DataEvent<OKXAlgoOrderUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                        .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithDataTimestamp(item.UpdateTime, _client.GetTimeOffset())
                        .WithStreamId(data.Arg.Channel)
                        .WithSymbol(data.Arg.Symbol)
                    );
            }
        });

        var subscription = new OKXSubscription<OKXAlgoOrderUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "algo-advance",
                    Symbol = symbol,
                    InstrumentType = instrumentType,
                    AlgoId = algoId,
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }
}
