using OKX.Net.Enums;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API trading endpoints
/// </summary>
public interface IOKXRestClientUnifiedApiTrading
{
    /// <summary>
    /// Edit incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-amend-multiple-orders" /></para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Edit an incomplete order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-amend-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="requestId">Request ID</param>
    /// <param name="cancelOnFail">Cancel On Fail</param>
    /// <param name="newQuantity">New Quantity</param>
    /// <param name="newPrice">New Price</param>
    /// <param name="newTriggerPrice">New trigger price</param>
    /// <param name="newTakeProfitTriggerPrice">New take profit trigger price</param>
    /// <param name="newStopLossTriggerPrice">New stop loss trigger price</param>
    /// <param name="newTakeProfitOrderPrice">New take profit order price</param>
    /// <param name="newStopLossOrderPrice">New stop loss order price</param>
    /// <param name="newTakeProfitPriceTriggerType">New take profit price trigger type</param>
    /// <param name="newStopLossPriceTriggerType">New stop loss price trigger type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderAmendResponse>> AmendOrderAsync(
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
        CancellationToken ct = default);

    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-cancel-advance-algo-order" /></para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrderResponse>> CancelAdvanceAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-cancel-algo-order" /></para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel all pending orders after a certain time. Recalling this endpoint resets the timeout. Sending TimeSpan.Zero disables the countdown
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-cancel-all-after" /></para>
    /// </summary>
    /// <param name="timeout">Timeout, between 10 and 120 seconds. TimeSpan.Zero disables timeout</param>
    /// <param name="tag">Only cancel orders with this Tag</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCancelAllAfterResponse>> CancelAllAfterAsync(TimeSpan timeout, string? tag = null, CancellationToken ct = default);

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-cancel-multiple-orders" /></para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel an incomplete order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-cancel-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Close all positions of an instrument via a market order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-close-positions" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="asset">Asset, for example `USDT`</param>
    /// <param name="autoCancel">Whether any pending orders for closing out needs to be automatically canceled when close position via a market order.</param>
    /// <param name="clientOrderId">Client order id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXClosePositionResponse>> ClosePositionAsync(string symbol, MarginMode marginMode, PositionSide? positionSide = null, string? asset = null, bool? autoCancel = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of untriggered Algo orders under the current account.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-history" /></para>
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoOrderState">Algo Order State</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrder[]>> GetAlgoOrderHistoryAsync(AlgoOrderType algoOrderType, AlgoOrderState? algoOrderState = null, string? algoId = null, InstrumentType? instrumentType = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get a list of untriggered Algo orders under the current account.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-list" /></para>
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrder[]>> GetAlgoOrderListAsync(AlgoOrderType algoOrderType, string? algoId = null, InstrumentType? instrumentType = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-history-last-3-months" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="fromId">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder[]>> GetOrderArchiveAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, OrderType? orderType = null, OrderStatus? state = null, OrderCategory? category = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? fromId = null, string? toId = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get order details.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-details" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder>> GetOrderDetailsAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Get the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-history-last-7-days" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="fromId">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder[]>> GetOrderHistoryAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, OrderType? orderType = null, OrderStatus? state = null, OrderCategory? category = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100,
        string? instrumentFamily = null,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get all incomplete orders under the current account.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-list" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder[]>> GetOrdersAsync(InstrumentType? instrumentType = null, string? symbol = null, string? underlying = null, OrderType? orderType = null, OrderStatus? state = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get recently-filled transaction details in the last 3 months.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-transaction-details-last-3-months" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="fromId">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransaction[]>> GetUserTradesArchiveAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Get recently-filled transaction details in the last 3 day.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-transaction-details-last-3-days" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="fromId">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransaction[]>> GetUserTradesAsync(InstrumentType? instrumentType = null, string? symbol = null, string? underlying = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Place new algo order. The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-place-algo-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="orderSide">Order Side</param>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="asset">Asset</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="tpTriggerPxType">Take-profit trigger price type</param>
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
    /// <param name="slOrderPrice">Stop Loss Order Price</param>
    /// <param name="triggerPrice">Trigger Price</param>
    /// <param name="orderPrice">Order Price</param>
    /// <param name="pxVar">Price Variance</param>
    /// <param name="priceRatio">Price Ratio</param>
    /// <param name="sizeLimit">Size Limit</param>
    /// <param name="priceLimit">Price Limit</param>
    /// <param name="callbackRatio">Callback ratio</param>
    /// <param name="callbackSpread">Callback spread</param>
    /// <param name="activePx">Active price</param>
    /// <param name="timeInterval">Time Interval</param>
    /// <param name="closeFraction">Fraction of position to be closed when the algo order is triggered. Currently the system supports fully closing the position only so the only accepted value is 1.</param>
    /// <param name="cancelOnClose">Whether the TP/SL order placed by the user is associated with the corresponding position of the instrument. If it is associated, the TP/SL order will be cancelled when the position is fully closed; if it is not, the TP/SL order will not be affected when the position is fully closed.</param>
    /// <param name="quickMarginType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin</param>
    /// <param name="clientOrderId">Client order id</param>
    /// <param name="chaseType">Chase order value type</param>
    /// <param name="chaseValue">Chase value, with chaseType.Distance it represents the USD chase value, with chaseType.Ratio 0.1 means 10%</param>
    /// <param name="maxChaseType">Max chase order value type</param>
    /// <param name="maxChaseValue">Max chase value, with chaseType.Distance it represents the USD chase value, with chaseType.Ratio 0.1 means 10%</param>
    /// <param name="tradeQuoteAsset">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrderResponse>> PlaceAlgoOrderAsync(
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
        CancellationToken ct = default);

    /// <summary>
    /// Place orders in batches. Maximum 20 orders can be placed at a time.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-multiple-orders" /></para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Place a new order
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="side">Order Side</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="type">Order Type</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="price">Price</param>
    /// <param name="asset">Asset</param>
    /// <param name="attachedAlgoOrders">Attached take profit / stop loss orders</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="quickMarginType">Quick margin type</param>
    /// <param name="selfTradePreventionId">Self trade prevention id</param>
    /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
    /// <param name="quantityAsset">Asset of the quantity when placing market order</param>
    /// <param name="tag">Order tag</param>
    /// <param name="priceUsd">Place options orders in USD, only applicable to OPTIONS</param>
    /// <param name="priceVol">Place options orders based on implied volatility, where 1 represents 100%. Only applicable to OPTIONS</param>
    /// <param name="banAmend">Whether to disallow the system from amending the size of the SPOT Market Order, if true, system will not amend and reject the market order if user does not have sufficient funds.</param>
    /// <param name="tradeQuoteAsset">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(
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
        CancellationToken ct = default);

    /// <summary>
    /// Check the results of an order, returns account info before and after the order would be completed
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="side">Order Side</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="type">Order Type</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="price">Price</param>
    /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="takeProfitTriggerPrice">Take profit trigger price</param>
    /// <param name="stopLossTriggerPrice">Stop loss trigger price</param>
    /// <param name="takeProfitOrderPrice">Take profit order price</param>
    /// <param name="stopLossOrderPrice">Stop loss order price</param>
    /// <param name="takeProfitTriggerPriceType">Take profit price type</param>
    /// <param name="stopLossTriggerPriceType">Stop loss price type</param>
    /// <param name="quantityAsset">Asset of the quantity when placing market order</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCheckOrderResponse>> CheckOrderAsync(
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
        CancellationToken ct = default);

    /// <summary>
    /// Get a specific algo order
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-details" /></para>
    /// </summary>
    /// <param name="algoId">Algo id, this or clientAlgoId should be provided</param>
    /// <param name="clientAlgoId">Client algo order id, this or algoId should be provided</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrder>> GetAlgoOrderAsync(string? algoId = null, string? clientAlgoId = null, CancellationToken ct = default);

    /// <summary>
    /// Edit an incomplete algo order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-amend-algo-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="clientAlgoId">Client Algo Order ID</param>
    /// <param name="requestId">Request ID</param>
    /// <param name="cancelOnFail">Cancel On Fail</param>
    /// <param name="newQuantity">New Quantity</param>
    /// <param name="newTakeProfitTriggerPrice">New take profit trigger price</param>
    /// <param name="newStopLossTriggerPrice">New stop loss trigger price</param>
    /// <param name="newTakeProfitOrderPrice">New take profit order price</param>
    /// <param name="newStopLossOrderPrice">New stop loss order price</param>
    /// <param name="newTakeProfitPriceTriggerType">New take profit price trigger type</param>
    /// <param name="newStopLossPriceTriggerType">New stop loss price trigger type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrderAmendResponse>> AmendAlgoOrderAsync(
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
        CancellationToken ct = default);
}
