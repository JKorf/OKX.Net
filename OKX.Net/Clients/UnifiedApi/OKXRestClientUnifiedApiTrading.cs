using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiTrading : IOKXRestClientUnifiedApiTrading
{
    private const string _bodyParameterKey = "<BODY>";

    private static Random _random = new Random();
    private readonly OKXRestClientUnifiedApi _baseClient;

    #region Trade Endpoints
    private const string Endpoints_V5_Trade_Order = "api/v5/trade/order";
    private const string Endpoints_V5_Trade_BatchOrders = "api/v5/trade/batch-orders";
    private const string Endpoints_V5_Trade_CancelOrder = "api/v5/trade/cancel-order";
    private const string Endpoints_V5_Trade_CancelBatchOrders = "api/v5/trade/cancel-batch-orders";
    private const string Endpoints_V5_Trade_AmendOrder = "api/v5/trade/amend-order";
    private const string Endpoints_V5_Trade_AmendBatchOrders = "api/v5/trade/amend-batch-orders";
    private const string Endpoints_V5_Trade_ClosePosition = "api/v5/trade/close-position";
    private const string Endpoints_V5_Trade_OrdersPending = "api/v5/trade/orders-pending";
    private const string Endpoints_V5_Trade_OrdersHistory = "api/v5/trade/orders-history";
    private const string Endpoints_V5_Trade_OrdersHistoryArchive = "api/v5/trade/orders-history-archive";
    private const string Endpoints_V5_Trade_Fills = "api/v5/trade/fills";
    private const string Endpoints_V5_Trade_FillsHistory = "api/v5/trade/fills-history";
    private const string Endpoints_V5_Trade_OrderAlgo = "api/v5/trade/order-algo";
    private const string Endpoints_V5_Trade_CancelAlgos = "api/v5/trade/cancel-algos";
    private const string Endpoints_V5_Trade_CancelAdvanceAlgos = "api/v5/trade/cancel-advance-algos";
    private const string Endpoints_V5_Trade_OrdersAlgoPending = "api/v5/trade/orders-algo-pending";
    private const string Endpoints_V5_Trade_OrdersAlgoHistory = "api/v5/trade/orders-algo-history";
    #endregion

    internal OKXRestClientUnifiedApiTrading(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(
        string symbol,
        OKXOrderSide side,
        OKXOrderType type,
        decimal quantity,
        decimal? price = null,
        OKXPositionSide? positionSide = null,
        OKXTradeMode? tradeMode = null,

        decimal? takeProfitTriggerPrice = null,
        decimal? stopLossTriggerPrice = null,
        decimal? takeProfitOrderPrice = null,
        decimal? stopLossOrderPrice = null,
        OXKTriggerPriceType? takeProfitTriggerPriceType = null,
        OXKTriggerPriceType? stopLossTriggerPriceType = null,
        OKXQuickMarginType? quickMarginType = null,
        int? selfTradePreventionId = null,
        OKXSelfTradePreventionMode? selfTradePreventionMode = null,

        string? asset = null,
        OKXQuantityAsset? quantityAsset = null,
        string? clientOrderId = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
            {"tdMode", JsonConvert.SerializeObject(tradeMode ?? OKXTradeMode.Cash, new TradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
            {"sz", quantity.ToString(CultureInfo.InvariantCulture) },
            {"tag", _baseClient._ref },
            {"clOrdId",  _baseClient._ref + (clientOrderId ?? RandomString(15)) },
        };
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("ccy", asset);

        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("tpTriggerPx", takeProfitTriggerPrice);
        parameters.AddOptionalParameter("slTriggerPx", stopLossTriggerPrice);
        parameters.AddOptionalParameter("tpOrdPx", takeProfitOrderPrice);
        parameters.AddOptionalParameter("slOrdPx", stopLossOrderPrice);
        parameters.AddOptionalParameter("tpTriggerPxType", EnumConverter.GetString(takeProfitTriggerPriceType));
        parameters.AddOptionalParameter("slTriggerPxType", EnumConverter.GetString(stopLossTriggerPriceType));
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.AddOptionalParameter("stpId", selfTradePreventionId);
        parameters.AddOptionalParameter("stpMode", EnumConverter.GetString(selfTradePreventionMode));

        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderPlaceResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_Order), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXOrderPlaceResponse>(result.Error!);
        if (result.Data.ErrorCode > 0)
        {
            var detailed = result.Data.Data.FirstOrDefault();
            if (detailed != null)
            {
                return result.AsError<OKXOrderPlaceResponse>(new OKXRestApiError(Convert.ToInt32(detailed.Code), detailed.Message, null));
            }

            return result.AsError<OKXOrderPlaceResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));
        }

        var orderResult = result.Data.Data.FirstOrDefault();
        _baseClient.InvokeOrderPlaced(new CryptoExchange.Net.CommonObjects.OrderId
        {
            Id = orderResult.OrderId.ToString(),
            SourceObject = result.Data
        });

        return result.As(orderResult);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders)
        {
            order.Tag = _baseClient._ref;
            order.ClientOrderId = _baseClient._ref + (order.ClientOrderId ?? RandomString(15));
        }

        var parameters = new Dictionary<string, object>
        {
            { _bodyParameterKey, orders },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderPlaceResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_BatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOrderPlaceResponse>>(result.Error!);
        if (result.Data.ErrorCode > 0)
        {
            var detailed = result.Data.Data.FirstOrDefault(x => x.Code != "0");
            if (detailed != null)
            {
                return result.AsError<IEnumerable<OKXOrderPlaceResponse>>(new OKXRestApiError(Convert.ToInt32(detailed.Code), detailed.Message, null));
            }

            return result.AsError<IEnumerable<OKXOrderPlaceResponse>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));
        }

        foreach (var order in result.Data.Data!)
        {
            if (order.OrderId.HasValue)
            {
                _baseClient.InvokeOrderPlaced(new CryptoExchange.Net.CommonObjects.OrderId
                {
                    Id = order.OrderId.ToString(),
                    SourceObject = result.Data
                });
            }
        }

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderCancelResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_CancelOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXOrderCancelResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXOrderCancelResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        var orderResult = result.Data.Data.FirstOrDefault();
        _baseClient.InvokeOrderCanceled(new CryptoExchange.Net.CommonObjects.OrderId
        {
            Id = orderResult.OrderId.ToString(),
            SourceObject = result.Data
        });

        return result.As(orderResult);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { _bodyParameterKey, orders },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderCancelResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_CancelBatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOrderCancelResponse>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOrderCancelResponse>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));


        foreach (var order in result.Data.Data!)
        {
            if (order.OrderId.HasValue)
            {
                _baseClient.InvokeOrderCanceled(new CryptoExchange.Net.CommonObjects.OrderId
                {
                    Id = order.OrderId.ToString(),
                    SourceObject = result.Data
                });
            }
        }

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderAmendResponse>> AmendOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        bool? cancelOnFail = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        decimal? newTriggerPrice = null,
        decimal? newTakeProfitTriggerPrice = null,
        decimal? newStopLossTriggerPrice = null,
        decimal? newTakeProfitOrderPrice = null,
        decimal? newStopLossOrderPrice = null,
        OXKTriggerPriceType? newTakeProfitPriceTriggerType = null,
        OXKTriggerPriceType? newStopLossPriceTriggerType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("cxlOnFail", cancelOnFail);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newPx", newPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalParameter("newTpTriggerPx", newTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newTpOrdPx", newTakeProfitOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newSlTriggerPx", newStopLossTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newSlOrdPx", newStopLossOrderPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalParameter("newTpTriggerPxType", EnumConverter.GetString(newTakeProfitPriceTriggerType));
        parameters.AddOptionalParameter("newSlTriggerPxType", EnumConverter.GetString(newStopLossPriceTriggerType));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderAmendResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_AmendOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXOrderAmendResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXOrderAmendResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { _bodyParameterKey, orders },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderAmendResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_AmendBatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOrderAmendResponse>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOrderAmendResponse>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXClosePositionResponse>> ClosePositionAsync(
        string symbol,
        OKXMarginMode marginMode,
        OKXPositionSide? positionSide = null,
        string? asset = null,
        bool? autoCancel = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
            {"tag", _baseClient._ref },
            {"clOrdId", _baseClient._ref + RandomString(15) }

        };
        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("autoCxl", autoCancel);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXClosePositionResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_ClosePosition), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXClosePositionResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXClosePositionResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrder>> GetOrderDetailsAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrder>>>(_baseClient.GetUri(Endpoints_V5_Trade_Order), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXOrder>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXOrder>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOrder>>> GetOrdersAsync(
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        string? underlying = null,
        OKXOrderType? orderType = null,
        OKXOrderState? state = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        if (orderType.HasValue)
            parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));

        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrder>>>(_baseClient.GetUri(Endpoints_V5_Trade_OrdersPending), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOrder>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOrder>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOrder>>> GetOrderHistoryAsync(
        OKXInstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        OKXOrderType? orderType = null,
        OKXOrderState? state = null,
        OKXOrderCategory? category = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
        };
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());

        if (orderType.HasValue)
            parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));

        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

        if (category.HasValue)
            parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrder>>>(_baseClient.GetUri(Endpoints_V5_Trade_OrdersHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOrder>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOrder>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOrder>>> GetOrderArchiveAsync(
        OKXInstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        OKXOrderType? orderType = null,
        OKXOrderState? state = null,
        OKXOrderCategory? category = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
        };
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());

        if (orderType.HasValue)
            parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));

        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

        if (category.HasValue)
            parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrder>>>(_baseClient.GetUri(Endpoints_V5_Trade_OrdersHistoryArchive), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOrder>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOrder>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTransaction>>> GetUserTradesAsync(
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        string? underlying = null,
        long? orderId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTransaction>>>(_baseClient.GetUri(Endpoints_V5_Trade_Fills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTransaction>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTransaction>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTransaction>>> GetUserTradesArchiveAsync(
        OKXInstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        long? orderId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTransaction>>>(_baseClient.GetUri(Endpoints_V5_Trade_FillsHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTransaction>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTransaction>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderResponse>> PlaceAlgoOrderAsync(
        string symbol,
        OKXTradeMode tradeMode,
        OKXOrderSide orderSide,
        OKXAlgoOrderType algoOrderType,
        decimal quantity,
        string? asset = null,
        bool? reduceOnly = null,
        OKXPositionSide? positionSide = null,
        OKXQuantityAsset? quantityType = null,

        OKXAlgoPriceType? tpTriggerPxType = null,
        decimal? tpTriggerPrice = null,
        decimal? tpOrderPrice = null,
        OKXAlgoPriceType? slTriggerPxType = null,
        decimal? slTriggerPrice = null,
        decimal? slOrderPrice = null,

        decimal? triggerPrice = null,
        decimal? orderPrice = null,

        OKXPriceVariance? pxVar = null,
        decimal? priceRatio = null,
        decimal? sizeLimit = null,
        decimal? priceLimit = null,

        long? timeInterval = null,

        decimal? callbackRatio = null,
        decimal? activePx = null,
        decimal? callbackSpread = null,

        decimal? closeFraction = null,
        bool? cancelOnClose = null,
        OKXQuickMarginType? quickMarginType = null,

        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false)) },
            {"sz", quantity.ToString() },
            {"tag", _baseClient._ref },
            {"clOrdId", _baseClient._ref + RandomString(15) }
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityType));

        if (tpTriggerPxType.HasValue)
            parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(tpTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToString(CultureInfo.InvariantCulture));
        if (slTriggerPxType.HasValue)
            parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(slTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("slTriggerPx", slTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("slOrdPx", slOrderPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalParameter("triggerPx", triggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("orderPx", orderPrice?.ToString(CultureInfo.InvariantCulture));

        if (pxVar.HasValue)
            parameters.AddOptionalParameter("pxVar", JsonConvert.SerializeObject(pxVar, new PriceVarianceConverter(false)));
        parameters.AddOptionalParameter("pxSpread", priceRatio?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("szLimit", sizeLimit?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("pxLimit", priceLimit?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("cxlOnClosePos", cancelOnClose);
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));

        parameters.AddOptionalParameter("callbackRatio", callbackRatio?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("callbackSpread", callbackSpread?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("activePx", activePx?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalParameter("timeInterval", timeInterval?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("closeFraction", closeFraction?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAlgoOrderResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_OrderAlgo), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAlgoOrderResponse>(result.Error!);

        if (result.Data.ErrorCode > 0)
        {
            var detailed = result.Data.Data.FirstOrDefault();
            if (detailed != null)
            {
                return result.AsError<OKXAlgoOrderResponse>(new OKXRestApiError(Convert.ToInt32(detailed.Code), detailed.Message, null));
            }

            return result.AsError<OKXAlgoOrderResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));
        }

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {_bodyParameterKey, orders },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAlgoOrderResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_CancelAlgos), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAlgoOrderResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXAlgoOrderResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderResponse>> CancelAdvanceAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {_bodyParameterKey, orders },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAlgoOrderResponse>>>(_baseClient.GetUri(Endpoints_V5_Trade_CancelAdvanceAlgos), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAlgoOrderResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXAlgoOrderResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAlgoOrder>>> GetAlgoOrderListAsync(
        OKXAlgoOrderType algoOrderType,
        string? algoId = null,
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            {"ordType",   JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false))}
        };
        parameters.AddOptionalParameter("algoId", algoId);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAlgoOrder>>>(_baseClient.GetUri(Endpoints_V5_Trade_OrdersAlgoPending), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXAlgoOrder>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXAlgoOrder>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAlgoOrder>>> GetAlgoOrderHistoryAsync(
        OKXAlgoOrderType algoOrderType,
        OKXAlgoOrderState? algoOrderState = null,
        string? algoId = null,
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            {"ordType",   JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false))}
        };
        parameters.AddOptionalParameter("algoId", algoId);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (algoOrderState.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(algoOrderState, new AlgoOrderStateConverter(false)));

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAlgoOrder>>>(_baseClient.GetUri(Endpoints_V5_Trade_OrdersAlgoHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXAlgoOrder>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXAlgoOrder>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    private string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
