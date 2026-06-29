using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.RateLimiting.Guards;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiTrading : IOKXRestClientUnifiedApiTrading
{
    private static readonly RequestDefinitionCache _definitions = new();
    private readonly OKXRestClientUnifiedApi _baseClient;

    internal OKXRestClientUnifiedApiTrading(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrderPlaceResponse>> PlaceOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity,
        decimal? price = null,
        PositionSide? positionSide = null,
        TradeMode? tradeMode = null,

        IEnumerable<OKXAttachedAlgoOrder>? attachedAlgoOrders = null,

        QuickMarginType? quickMarginType = null,
        int? selfTradePreventionId = null,
        SelfTradePreventionMode? selfTradePreventionMode = null,

        string? asset = null,
        QuantityAsset? quantityAsset = null,
        string? clientOrderId = null,
        bool? reduceOnly = null,
        string? tag = null,
        decimal? priceUsd = null,
        decimal? priceVol = null,
        bool? banAmend = null,
        string? tradeQuoteAsset = null,
        int? priceAmendType = null,
        bool? isElpTakerAccess = null,
        decimal? maxSlippagePercentage = null,

        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
            {"sz", quantity.ToString(CultureInfo.InvariantCulture) },
            {"tag", !string.IsNullOrEmpty(tag) ? tag! : LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
        };

        parameters.Add("clOrdId", clientOrderId);
        parameters.Add("tdMode", tradeMode ?? TradeMode.Cash);
        parameters.Add("side", side);
        parameters.Add("ordType", type);
        parameters.Add("px", price);
        parameters.Add("ccy", asset);

        parameters.Add("tgtCcy", quantityAsset);
        parameters.Add("quickMgnType", quickMarginType);
        parameters.Add("stpId", selfTradePreventionId);
        parameters.Add("stpMode", selfTradePreventionMode);
        parameters.Add("tradeQuoteCcy", tradeQuoteAsset);
        parameters.Add("pxAmendType", priceAmendType);
        parameters.Add("isElpTakerAccess", isElpTakerAccess);
        parameters.Add("slippagePct", maxSlippagePercentage);

        if (attachedAlgoOrders != null)
        {
            foreach (var attachOrder in attachedAlgoOrders)
                attachOrder.ClientOrderId = LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange);
        }
        parameters.AddArray("attachAlgoOrds", attachedAlgoOrders?.ToArray());

        parameters.Add("reduceOnly", reduceOnly);
        parameters.Add("posSide", positionSide);

        parameters.Add("pxUsd", priceUsd);
        parameters.Add("pxVol", priceVol);
        parameters.Add("banAmend", banAmend);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderPlaceResponse[]>>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<OKXOrderPlaceResponse>(result);

        var detailed = result.Data.Data?.FirstOrDefault();
        if (result.Data.ErrorCode > 0)
        {
            if (detailed != null)
                return HttpResult.Fail<OKXOrderPlaceResponse>(result, new ServerError(detailed.Code, _baseClient.GetErrorInfo(detailed.Code, detailed.Message)));

            return HttpResult.Fail<OKXOrderPlaceResponse>(result, new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));
        }

        return HttpResult.Ok(result, detailed!);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXCheckOrderResponse>> CheckOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity,
        decimal? price = null,
        PositionSide? positionSide = null,
        TradeMode? tradeMode = null,

        decimal? takeProfitTriggerPrice = null,
        decimal? stopLossTriggerPrice = null,
        decimal? takeProfitOrderPrice = null,
        decimal? stopLossOrderPrice = null,
        TriggerPriceType? takeProfitTriggerPriceType = null,
        TriggerPriceType? stopLossTriggerPriceType = null,

        QuantityAsset? quantityAsset = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
            {"sz", quantity.ToString(CultureInfo.InvariantCulture) }
        };
        parameters.Add("tdMode", tradeMode ?? TradeMode.Cash);
        parameters.Add("side", side);
        parameters.Add("ordType", type);
        parameters.Add("px", price?.ToString(CultureInfo.InvariantCulture));

        parameters.Add("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.Add("tpTriggerPx", takeProfitTriggerPrice);
        parameters.Add("slTriggerPx", stopLossTriggerPrice);
        parameters.Add("tpOrdPx", takeProfitOrderPrice);
        parameters.Add("slOrdPx", stopLossOrderPrice);
        parameters.Add("tpTriggerPxType", EnumConverter.GetString(takeProfitTriggerPriceType));
        parameters.Add("slTriggerPxType", EnumConverter.GetString(stopLossTriggerPriceType));

        parameters.Add("reduceOnly", reduceOnly);
        parameters.Add("posSide", positionSide);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/order-precheck", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXCheckOrderResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<OKXCheckOrderResponse>(result);

        return HttpResult.Ok(result, result.Data.Data!.First());
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders)
            order.Tag = LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange);

        var parameters = new Parameters(orders.ToArray(), OKXExchange._parameterSerializationSettings);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/batch-orders", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(300, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderPlaceResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<CallResult<OKXOrderPlaceResponse>[]>(result);

        if (result.Data.ErrorCode > 0 && result.Data.Data?.Any() != true)
            return HttpResult.Fail<CallResult<OKXOrderPlaceResponse>[]>(result, new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));

        var ordersResult = new List<CallResult<OKXOrderPlaceResponse>>();
        foreach (var item in result.Data.Data!)
        {
            if (item.Code > 0)
                ordersResult.Add(CallResult.Fail<OKXOrderPlaceResponse>(new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
            else
                ordersResult.Add(CallResult.Ok(item));
        }

        if (ordersResult.All(x => !x.Success))
            return HttpResult.Fail<CallResult<OKXOrderPlaceResponse>[]>(result, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), ordersResult.ToArray());

        return HttpResult.Ok(result, ordersResult.ToArray());
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
        };
        parameters.Add("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("clOrdId", clientOrderId);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/cancel-order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderCancelResponse[]>>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);

        if (!result.Success)
            return HttpResult.Fail<OKXOrderCancelResponse>(result);

        if (result.Data.ErrorCode != 0 && result.Data.ErrorCode != 1)
            return HttpResult.Fail<OKXOrderCancelResponse>(result, new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage)));

        var order = result.Data.Data?.FirstOrDefault();
        if (order == null)
            // Shouldn't happen with error code 0/1
            return HttpResult.Fail<OKXOrderCancelResponse>(result, new ServerError(ErrorInfo.Unknown));

        if (order.Code != 0)
            return HttpResult.Fail<OKXOrderCancelResponse>(result, new ServerError(order.Code, _baseClient.GetErrorInfo(order.Code, order.Message)), order);

        return HttpResult.Ok(result, result.Data.Data!.First());
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXCancelAllAfterResponse>> CancelAllAfterAsync(TimeSpan timeout, string? tag = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("timeOut", (int)timeout.TotalSeconds);
        parameters.Add("tag", tag);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/cancel-all-after", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCancelAllAfterResponse>(request, parameters, ct, rateLimitKeySuffix: tag ?? "").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Parameters(orders.ToArray(), OKXExchange._parameterSerializationSettings);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/cancel-batch-orders", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(300, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderCancelResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<OKXOrderCancelResponse[]>(result);

        if (result.Data.ErrorCode > 0 && result.Data.ErrorCode != 2)
            return HttpResult.Fail<OKXOrderCancelResponse[]>(result, new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));

        return HttpResult.Ok<OKXOrderCancelResponse[]>(result, result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrderAmendResponse>> AmendOrderAsync(
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
        TriggerPriceType? newTakeProfitPriceTriggerType = null,
        TriggerPriceType? newStopLossPriceTriggerType = null,
        int? priceAmendType = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            { "instId", symbol },
        };
        parameters.Add("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("clOrdId", clientOrderId);
        parameters.Add("cxlOnFail", cancelOnFail);
        parameters.Add("reqId", requestId);
        parameters.Add("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newPx", newPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.Add("newTpTriggerPx", newTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newTpOrdPx", newTakeProfitOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newSlTriggerPx", newStopLossTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newSlOrdPx", newStopLossOrderPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.Add("newTpTriggerPxType", EnumConverter.GetString(newTakeProfitPriceTriggerType));
        parameters.Add("newSlTriggerPxType", EnumConverter.GetString(newStopLossPriceTriggerType));
        parameters.Add("pxAmendType", priceAmendType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/amend-order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXOrderAmendResponse>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Parameters(orders.ToArray(), OKXExchange._parameterSerializationSettings);
        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/amend-batch-orders", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(300, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrderAmendResponse[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXClosePositionResponse>> ClosePositionAsync(
        string symbol,
        MarginMode marginMode,
        PositionSide? positionSide = null,
        string? asset = null,
        bool? autoCancel = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
            {"tag", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
        };
        parameters.Add("clOrdId", clientOrderId);
        parameters.Add("mgnMode", marginMode);
        parameters.Add("posSide", positionSide);
        parameters.Add("ccy", asset);
        parameters.Add("autoCxl", autoCancel);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/close-position", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXClosePositionResponse>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrder>> GetOrderDetailsAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
        };
        parameters.Add("ordId", orderId?.ToString());
        parameters.Add("clOrdId", clientOrderId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXOrder>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrder[]>> GetOrdersAsync(
        InstrumentType? instrumentType = null,
        string? symbol = null,
        string? underlying = null,
        OrderType? orderType = null,
        OrderStatus? state = null,
        string? fromId = null,
        string? toId = null,
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instId", symbol);
        parameters.Add("uly", underlying);
        parameters.Add("before", fromId);
        parameters.Add("after", toId);
        parameters.Add("limit", limit.ToString());
        parameters.Add("instFamily", instrumentFamily);

        parameters.Add("instType", instrumentType);
        parameters.Add("ordType", orderType);
        parameters.Add("state", state);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/orders-pending", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrder[]>> GetOrderHistoryAsync(
        InstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        OrderType? orderType = null,
        OrderStatus? state = null,
        OrderCategory? category = null,
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

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("uly", underlying);
        parameters.Add("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("limit", limit.ToString());
        parameters.Add("after", fromId);
        parameters.Add("before", toId);

        parameters.Add("ordType", orderType);
        parameters.Add("state", state);
        parameters.Add("category", category);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/orders-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOrder[]>> GetOrderArchiveAsync(
        InstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        OrderType? orderType = null,
        OrderStatus? state = null,
        OrderCategory? category = null,
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

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("uly", underlying);
        parameters.Add("instFamily", instrumentFamily);
        parameters.Add("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("limit", limit.ToString());
        parameters.Add("after", fromId);
        parameters.Add("before", toId);

        parameters.Add("ordType", orderType);
        parameters.Add("state", state);
        parameters.Add("category", category);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/orders-history-archive", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXTransaction[]>> GetUserTradesAsync(
        InstrumentType? instrumentType = null,
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

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("uly", underlying);
        parameters.Add("ordId", orderId?.ToString());
        parameters.Add("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("after", fromId);
        parameters.Add("before", toId);
        parameters.Add("limit", limit.ToString());
        parameters.Add("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/fills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXTransaction[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXTransaction[]>> GetUserTradesArchiveAsync(
        InstrumentType instrumentType,
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

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("uly", underlying);
        parameters.Add("ordId", orderId?.ToString());
        parameters.Add("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("after", fromId);
        parameters.Add("before", toId);
        parameters.Add("limit", limit.ToString());
        parameters.Add("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/fills-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXTransaction[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAlgoOrderResponse>> PlaceAlgoOrderAsync(
        string symbol,
        TradeMode tradeMode,
        OrderSide orderSide,
        AlgoOrderType algoOrderType,
        decimal? quantity = null,
        string? asset = null,
        bool? reduceOnly = null,
        PositionSide? positionSide = null,
        QuantityAsset? quantityType = null,

        AlgoPriceType? tpTriggerPxType = null,
        decimal? tpTriggerPrice = null,
        decimal? tpOrderPrice = null,
        AlgoPriceType? slTriggerPxType = null,
        decimal? slTriggerPrice = null,
        decimal? slOrderPrice = null,

        decimal? triggerPrice = null,
        decimal? orderPrice = null,

        PriceVariance? pxVar = null,
        decimal? priceRatio = null,
        decimal? sizeLimit = null,
        decimal? priceLimit = null,

        long? timeInterval = null,

        decimal? callbackRatio = null,
        decimal? activePx = null,
        decimal? callbackSpread = null,

        decimal? closeFraction = null,
        bool? cancelOnClose = null,
        QuickMarginType? quickMarginType = null,
        string? clientOrderId = null,

        ChaseType? chaseType = null,
        decimal? chaseValue = null,
        ChaseType? maxChaseType = null,
        decimal? maxChaseValue = null,

        string? tradeQuoteAsset = null,
        AdvancedOrderType? advancedOrderType = null,

        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
            {"tag", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
        };
        parameters.Add("clOrdId", clientOrderId);
        parameters.Add("tdMode", tradeMode);
        parameters.Add("side", orderSide);
        parameters.Add("ordType", algoOrderType);
        parameters.Add("sz", quantity);
        parameters.Add("ccy", asset);
        parameters.Add("reduceOnly", reduceOnly);

        parameters.Add("posSide", positionSide);
        parameters.Add("tgtCcy", EnumConverter.GetString(quantityType));

        parameters.Add("tpTriggerPxType", tpTriggerPxType);
        parameters.Add("tpTriggerPx", tpTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("tpOrdPx", tpOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("slTriggerPxType", slTriggerPxType);
        parameters.Add("slTriggerPx", slTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("slOrdPx", slOrderPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.Add("triggerPx", triggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("orderPx", orderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("advanceOrdType", advancedOrderType);

        parameters.Add("pxVar", pxVar);
        parameters.Add("pxSpread", priceRatio?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("szLimit", sizeLimit?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("pxLimit", priceLimit?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("cxlOnClosePos", cancelOnClose);
        parameters.Add("quickMgnType", EnumConverter.GetString(quickMarginType));

        parameters.Add("callbackRatio", callbackRatio?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("callbackSpread", callbackSpread?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("activePx", activePx?.ToString(CultureInfo.InvariantCulture));

        parameters.Add("timeInterval", timeInterval?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("closeFraction", closeFraction?.ToString(CultureInfo.InvariantCulture));

        parameters.Add("chaseType", chaseType);
        parameters.Add("chaseVal", chaseValue);
        parameters.Add("maxChaseType", maxChaseType);
        parameters.Add("maxChaseVal", maxChaseValue);
        parameters.Add("tradeQuoteCcy", tradeQuoteAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/order-algo", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXAlgoOrderResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<OKXAlgoOrderResponse>(result);

        var detailed = result.Data.Data?.FirstOrDefault();
        if (result.Data.ErrorCode > 0)
        {
            if (detailed != null)
                return HttpResult.Fail<OKXAlgoOrderResponse>(result, new ServerError(detailed.Code, _baseClient.GetErrorInfo(detailed.Code, detailed.Message)));

            return HttpResult.Fail<OKXAlgoOrderResponse>(result, new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));
        }

        return HttpResult.Ok(result, detailed!);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Parameters(orders.ToArray(), OKXExchange._parameterSerializationSettings);
        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/cancel-algos", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrderResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAlgoOrder[]>> GetAlgoOrderListAsync(
        AlgoOrderType algoOrderType,
        string? algoId = null,
        InstrumentType? instrumentType = null,
        string? symbol = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ordType", algoOrderType);
        parameters.Add("algoId", algoId);
        parameters.Add("instId", symbol);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("limit", limit.ToString());
        parameters.Add("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/orders-algo-pending", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAlgoOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAlgoOrder[]>> GetAlgoOrderHistoryAsync(
        AlgoOrderType algoOrderType,
        AlgoOrderState? algoOrderState = null,
        string? algoId = null,
        InstrumentType? instrumentType = null,
        string? symbol = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ordType", algoOrderType);
        parameters.Add("algoId", algoId);
        parameters.Add("instId", symbol);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("limit", limit.ToString());

        parameters.Add("state", algoOrderState);
        parameters.Add("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/orders-algo-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAlgoOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAlgoOrder>> GetAlgoOrderAsync(string? algoId = null, string? clientAlgoId = null, CancellationToken ct = default)
    {
        if ((algoId == null) == (clientAlgoId == null))
            throw new ArgumentException("Either algoId or clientAlgoId needs to be provided");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("algoId", algoId);
        parameters.Add("algoClOrdId", clientAlgoId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/order-algo", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrder>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountRateLimit[]>> GetAccountRateLimitAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/account-rate-limit", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAccountRateLimit[]>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOneClickRepayCurrencyList[]>> GetOneClickRepayCurrencyListAsync(string? debtType = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("debtType", debtType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/one-click-repay-currency-list", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOneClickRepayCurrencyList[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOneClickRepayCurrencyListV2[]>> GetOneClickRepayCurrencyListV2Async(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/one-click-repay-currency-list-v2", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOneClickRepayCurrencyListV2[]>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOneClickRepayResponse[]>> OneClickRepayAsync(string debtAsset, string repayAsset, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("debtCcy", debtAsset);
        parameters.Add("repayCcy", repayAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/one-click-repay", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOneClickRepayResponse[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOneClickRepayResponseV2[]>> OneClickRepayV2Async(string debtAsset, IEnumerable<string> repayAssetList, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("debtCcy", debtAsset);
        parameters.Add("repayCcyList", repayAssetList);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/one-click-repay-v2", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOneClickRepayResponseV2[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOneClickRepayHistory[]>> GetOneClickRepayHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/one-click-repay-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOneClickRepayHistory[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXOneClickRepayHistoryV2[]>> GetOneClickRepayHistoryV2Async(DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/trade/one-click-repay-history-v2", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOneClickRepayHistoryV2[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAlgoOrderAmendResponse>> AmendAlgoOrderAsync(
        string symbol,
        string? algoId = null,
        string? clientAlgoId = null,
        string? requestId = null,
        bool? cancelOnFail = null,
        decimal? newQuantity = null,
        decimal? newTakeProfitTriggerPrice = null,
        decimal? newStopLossTriggerPrice = null,
        decimal? newTakeProfitOrderPrice = null,
        decimal? newStopLossOrderPrice = null,
        TriggerPriceType? newTakeProfitPriceTriggerType = null,
        TriggerPriceType? newStopLossPriceTriggerType = null,
        CancellationToken ct = default)
    {
        if ((algoId == null) == (clientAlgoId == null))
            throw new ArgumentException("Either algoId or clientAlgoId needs to be provided");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            { "instId", symbol },
        };
        parameters.Add("algoId", algoId?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("algoClOrdId", clientAlgoId);
        parameters.Add("cxlOnFail", cancelOnFail);
        parameters.Add("reqId", requestId);
        parameters.Add("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newTpTriggerPx", newTakeProfitTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newTpOrdPx", newTakeProfitOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newSlTriggerPx", newStopLossTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newSlOrdPx", newStopLossOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("newTpTriggerPxType", newTakeProfitPriceTriggerType);
        parameters.Add("newSlTriggerPxType", newStopLossPriceTriggerType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/amend-algos", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrderAmendResponse>(request, parameters, ct).ConfigureAwait(false);
    }
}
