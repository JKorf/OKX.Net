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
    public virtual async Task<WebCallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity,
        decimal? price = null,
        PositionSide? positionSide = null,
        Enums.TradeMode? tradeMode = null,

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

        CancellationToken ct = default)
    {
        clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"sz", quantity.ToString(CultureInfo.InvariantCulture) },
            {"tag", tag ?? OKXExchange.ClientOrderId },
            {"clOrdId",  clientOrderId },
        };
        parameters.AddEnum("tdMode", tradeMode ?? Enums.TradeMode.Cash);
        parameters.AddEnum("side", side);
        parameters.AddEnum("ordType", type);
        parameters.AddOptionalString("px", price);
        parameters.AddOptional("ccy", asset);

        parameters.AddOptionalEnum("tgtCcy", quantityAsset);
        parameters.AddOptionalEnum("quickMgnType", quickMarginType);
        parameters.AddOptional("stpId", selfTradePreventionId);
        parameters.AddOptionalEnum("stpMode", selfTradePreventionMode);
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteAsset);

        if (attachedAlgoOrders != null)
        {
            foreach (var attachOrder in attachedAlgoOrders)
                attachOrder.ClientOrderId = LibraryHelpers.ApplyBrokerId(attachOrder.ClientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);
        }
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders?.ToArray());

        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptionalEnum("posSide", positionSide);

        parameters.AddOptionalString("pxUsd", priceUsd);
        parameters.AddOptionalString("pxVol", priceVol);
        parameters.AddOptional("banAmend", banAmend);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderPlaceResponse[]>>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
        if (!result)
            return result.As<OKXOrderPlaceResponse>(default);

        var detailed = result.Data.Data?.FirstOrDefault();
        if (result.Data.ErrorCode > 0)
        {
            if (detailed != null)
                return result.AsError<OKXOrderPlaceResponse>(new ServerError(detailed.Code, _baseClient.GetErrorInfo(detailed.Code, detailed.Message)));

            return result.AsError<OKXOrderPlaceResponse>(new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));
        }

        return result.As(detailed!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXCheckOrderResponse>> CheckOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity,
        decimal? price = null,
        PositionSide? positionSide = null,
        Enums.TradeMode? tradeMode = null,

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
        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"sz", quantity.ToString(CultureInfo.InvariantCulture) }
        };
        parameters.AddEnum("tdMode", tradeMode ?? Enums.TradeMode.Cash);
        parameters.AddEnum("side", side);
        parameters.AddEnum("ordType", type);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("tpTriggerPx", takeProfitTriggerPrice);
        parameters.AddOptionalParameter("slTriggerPx", stopLossTriggerPrice);
        parameters.AddOptionalParameter("tpOrdPx", takeProfitOrderPrice);
        parameters.AddOptionalParameter("slOrdPx", stopLossOrderPrice);
        parameters.AddOptionalParameter("tpTriggerPxType", EnumConverter.GetString(takeProfitTriggerPriceType));
        parameters.AddOptionalParameter("slTriggerPxType", EnumConverter.GetString(stopLossTriggerPriceType));

        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalEnum("posSide", positionSide);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/order-precheck", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXCheckOrderResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        return result.As<OKXCheckOrderResponse>(result.Data?.Data?.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders)
        {
            var clientOrderId = LibraryHelpers.ApplyBrokerId(order.ClientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);
            order.Tag = OKXExchange.ClientOrderId;
            order.ClientOrderId = clientOrderId;
        }

        var parameters = new ParameterCollection();
        parameters.SetBody(orders.ToArray());

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/batch-orders", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(300, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderPlaceResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result.As<CallResult<OKXOrderPlaceResponse>[]>(default);

        if (result.Data.ErrorCode > 0 && result.Data.Data?.Any() != true)
            return result.AsError<CallResult<OKXOrderPlaceResponse>[]>(new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));
        
        var ordersResult = new List<CallResult<OKXOrderPlaceResponse>>();
        foreach (var item in result.Data.Data!)
        {
            if (item.Code > 0)
                ordersResult.Add(new CallResult<OKXOrderPlaceResponse>(item, null, new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
            else
                ordersResult.Add(new CallResult<OKXOrderPlaceResponse>(item));
        }

        if (ordersResult.All(x => !x.Success))
            return result.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), ordersResult.ToArray());

        return result.As(ordersResult.ToArray());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        if (clientOrderId != null)
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection {
            {"instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/cancel-order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderCancelResponse[]>>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);

        if (!result)
            return result.As<OKXOrderCancelResponse>(default);

        if (result.Data.ErrorCode != 0 && result.Data.ErrorCode != 1)
            return result.AsError<OKXOrderCancelResponse>(new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage)));

        var order = result.Data.Data?.FirstOrDefault();
        if (order == null)
            // Shouldn't happen with error code 0/1
            return result.AsError<OKXOrderCancelResponse>(new ServerError(ErrorInfo.Unknown));

        if (order.Code != 0)
            return result.AsErrorWithData<OKXOrderCancelResponse>(new ServerError(order.Code, _baseClient.GetErrorInfo(order.Code, order.Message)), order);

        return result.As(result.Data.Data!.First());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXCancelAllAfterResponse>> CancelAllAfterAsync(TimeSpan timeout, string? tag = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("timeOut", (int)timeout.TotalSeconds);
        parameters.AddOptional("tag", tag);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/cancel-all-after", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCancelAllAfterResponse>(request, parameters, ct, rateLimitKeySuffix: tag ?? "").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders.ToArray());

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/cancel-batch-orders", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(300, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXOrderCancelResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result.AsError<OKXOrderCancelResponse[]>(result.Error!);

        if (result.Data.ErrorCode > 0 && result.Data.ErrorCode != 2)
            return result.AsError<OKXOrderCancelResponse[]>(new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));

        return result.As<OKXOrderCancelResponse[]>(result.Data.Data);
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
        TriggerPriceType? newTakeProfitPriceTriggerType = null,
        TriggerPriceType? newStopLossPriceTriggerType = null,
        CancellationToken ct = default)
    {
        if (clientOrderId != null)
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection
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

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/amend-order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXOrderAmendResponse>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders.ToArray());

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/amend-batch-orders", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(300, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrderAmendResponse[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXClosePositionResponse>> ClosePositionAsync(
        string symbol,
        MarginMode marginMode,
        PositionSide? positionSide = null,
        string? asset = null,
        bool? autoCancel = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"tag", OKXExchange.ClientOrderId },
            {"clOrdId", clientOrderId }
        };
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("autoCxl", autoCancel);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/close-position", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXClosePositionResponse>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrder>> GetOrderDetailsAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        if (clientOrderId != null)
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection {
            {"instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXOrder>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrder[]>> GetOrdersAsync(
        InstrumentType? instrumentType = null,
        string? symbol = null,
        string? underlying = null,
        OrderType? orderType = null,
        OrderStatus? state = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/orders-pending", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrder[]>> GetOrderHistoryAsync(
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

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());

        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptionalEnum("category", category);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/orders-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrder[]>> GetOrderArchiveAsync(
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

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());

        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptionalEnum("category", category);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/orders-history-archive", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransaction[]>> GetUserTradesAsync(
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

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/fills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXTransaction[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransaction[]>> GetUserTradesArchiveAsync(
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

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", fromId?.ToString());
        parameters.AddOptionalParameter("before", toId?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/fills-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXTransaction[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderResponse>> PlaceAlgoOrderAsync(
        string symbol,
        Enums.TradeMode tradeMode,
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

        CancellationToken ct = default)
    {
        clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"tag", OKXExchange.ClientOrderId },
            {"clOrdId", clientOrderId }
        };
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddEnum("side", orderSide);
        parameters.AddEnum("ordType", algoOrderType);
        parameters.AddOptionalString("sz", quantity);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityType));

        parameters.AddOptionalEnum("tpTriggerPxType", tpTriggerPxType);
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalEnum("slTriggerPxType", slTriggerPxType);
        parameters.AddOptionalParameter("slTriggerPx", slTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("slOrdPx", slOrderPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalParameter("triggerPx", triggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("orderPx", orderPrice?.ToString(CultureInfo.InvariantCulture));

        parameters.AddOptionalEnum("pxVar", pxVar);
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

        parameters.AddOptionalEnum("chaseType", chaseType);
        parameters.AddOptionalString("chaseVal", chaseValue);
        parameters.AddOptionalEnum("maxChaseType", maxChaseType);
        parameters.AddOptionalString("maxChaseVal", maxChaseValue);
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/order-algo", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendRawAsync<OKXRestApiResponse<OKXAlgoOrderResponse[]>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result.As<OKXAlgoOrderResponse>(default);

        var detailed = result.Data.Data?.FirstOrDefault();
        if (result.Data.ErrorCode > 0)
        {
            if (detailed != null)
                return result.AsError<OKXAlgoOrderResponse>(new ServerError(detailed.Code, _baseClient.GetErrorInfo(detailed.Code, detailed.Message)));

            return result.AsError<OKXAlgoOrderResponse>(new ServerError(result.Data.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));
        }

        return result.As<OKXAlgoOrderResponse>(detailed);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders.ToArray());

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/cancel-algos", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrderResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderResponse>> CancelAdvanceAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders.ToArray());

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/cancel-advance-algos", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrderResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrder[]>> GetAlgoOrderListAsync(
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

        var parameters = new ParameterCollection();
        parameters.AddEnum("ordType", algoOrderType);
        parameters.AddOptionalParameter("algoId", algoId);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalEnum("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/orders-algo-pending", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAlgoOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrder[]>> GetAlgoOrderHistoryAsync(
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

        var parameters = new ParameterCollection();
        parameters.AddEnum("ordType", algoOrderType);
        parameters.AddOptionalParameter("algoId", algoId);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        parameters.AddOptionalEnum("state", algoOrderState);
        parameters.AddOptionalEnum("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/orders-algo-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAlgoOrder[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrder>> GetAlgoOrderAsync(string? algoId = null, string? clientAlgoId = null, CancellationToken ct = default)
    {
        if ((algoId == null) == (clientAlgoId == null))
            throw new ArgumentException("Either algoId or clientAlgoId needs to be provided");

        if (clientAlgoId != null)
            clientAlgoId = LibraryHelpers.ApplyBrokerId(clientAlgoId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoId);
        parameters.AddOptional("algoClOrdId", clientAlgoId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/trade/order-algo", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrder>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAlgoOrderAmendResponse>> AmendAlgoOrderAsync(
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

        if (clientAlgoId != null)
            clientAlgoId = LibraryHelpers.ApplyBrokerId(clientAlgoId, OKXExchange.ClientOrderId, 32, _baseClient.ClientOptions.AllowAppendingClientOrderId);

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };
        parameters.AddOptional("algoId", algoId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptional("algoClOrdId", clientAlgoId);
        parameters.AddOptional("cxlOnFail", cancelOnFail);
        parameters.AddOptional("reqId", requestId);
        parameters.AddOptional("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptional("newTpTriggerPx", newTakeProfitTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptional("newTpOrdPx", newTakeProfitOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptional("newSlTriggerPx", newStopLossTriggerPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptional("newSlOrdPx", newStopLossOrderPrice?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalEnum("newTpTriggerPxType", newTakeProfitPriceTriggerType);
        parameters.AddOptionalEnum("newSlTriggerPxType", newStopLossPriceTriggerType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/trade/amend-algos", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAlgoOrderAmendResponse>(request, parameters, ct).ConfigureAwait(false);
    }
}
