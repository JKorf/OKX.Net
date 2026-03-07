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
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-amend-multiple-orders" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/amend-batch-orders
    /// </para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Edit an incomplete order.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-amend-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/amend-order
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">["<c>ordId</c>"] Order ID</param>
    /// <param name="clientOrderId">["<c>clOrdId</c>"] Client Order ID</param>
    /// <param name="requestId">["<c>reqId</c>"] Request ID</param>
    /// <param name="cancelOnFail">["<c>cxlOnFail</c>"] Cancel On Fail</param>
    /// <param name="newQuantity">["<c>newSz</c>"] New Quantity</param>
    /// <param name="newPrice">["<c>newPx</c>"] New Price</param>
    /// <param name="newTriggerPrice">["<c>newTpTriggerPx</c>"] New trigger price</param>
    /// <param name="newTakeProfitTriggerPrice">New take profit trigger price</param>
    /// <param name="newStopLossTriggerPrice">["<c>newSlTriggerPx</c>"] New stop loss trigger price</param>
    /// <param name="newTakeProfitOrderPrice">["<c>newTpOrdPx</c>"] New take profit order price</param>
    /// <param name="newStopLossOrderPrice">["<c>newSlOrdPx</c>"] New stop loss order price</param>
    /// <param name="newTakeProfitPriceTriggerType">["<c>newTpTriggerPxType</c>"] New take profit price trigger type</param>
    /// <param name="newStopLossPriceTriggerType">["<c>newSlTriggerPxType</c>"] New stop loss price trigger type</param>
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
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-cancel-advance-algo-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/cancel-advance-algos
    /// </para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrderResponse>> CancelAdvanceAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-cancel-algo-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/cancel-algos
    /// </para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OKXAlgoOrderRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel all pending orders after a certain time. Recalling this endpoint resets the timeout. Sending TimeSpan.Zero disables the countdown
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-cancel-all-after" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/cancel-all-after
    /// </para>
    /// </summary>
    /// <param name="timeout">["<c>timeOut</c>"] Timeout, between 10 and 120 seconds. TimeSpan.Zero disables timeout</param>
    /// <param name="tag">["<c>tag</c>"] Only cancel orders with this Tag</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCancelAllAfterResponse>> CancelAllAfterAsync(TimeSpan timeout, string? tag = null, CancellationToken ct = default);

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-cancel-multiple-orders" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/cancel-batch-orders
    /// </para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel an incomplete order.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-cancel-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/cancel-order
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">["<c>ordId</c>"] Order ID</param>
    /// <param name="clientOrderId">["<c>clOrdId</c>"] Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Close all positions of an instrument via a market order.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-close-positions" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/close-position
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="positionSide">["<c>posSide</c>"] Position Side</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `USDT`</param>
    /// <param name="autoCancel">["<c>autoCxl</c>"] Whether any pending orders for closing out needs to be automatically canceled when close position via a market order.</param>
    /// <param name="clientOrderId">["<c>clOrdId</c>"] Client order id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXClosePositionResponse>> ClosePositionAsync(string symbol, MarginMode marginMode, PositionSide? positionSide = null, string? asset = null, bool? autoCancel = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of untriggered Algo orders under the current account.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/orders-algo-history
    /// </para>
    /// </summary>
    /// <param name="algoOrderType">["<c>ordType</c>"] Algo Order Type</param>
    /// <param name="algoOrderState">["<c>state</c>"] Algo Order State</param>
    /// <param name="algoId">["<c>algoId</c>"] Algo ID</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrder[]>> GetAlgoOrderHistoryAsync(AlgoOrderType algoOrderType, AlgoOrderState? algoOrderState = null, string? algoId = null, InstrumentType? instrumentType = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get a list of untriggered Algo orders under the current account.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-list" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/orders-algo-pending
    /// </para>
    /// </summary>
    /// <param name="algoOrderType">["<c>ordType</c>"] Algo Order Type</param>
    /// <param name="algoId">["<c>algoId</c>"] Algo ID</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrder[]>> GetAlgoOrderListAsync(AlgoOrderType algoOrderType, string? algoId = null, InstrumentType? instrumentType = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-history-last-3-months" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/orders-history-archive
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="orderType">["<c>ordType</c>"] Order Type</param>
    /// <param name="state">["<c>state</c>"] State</param>
    /// <param name="category">["<c>category</c>"] Category</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="fromId">["<c>after</c>"] Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">["<c>before</c>"] Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder[]>> GetOrderArchiveAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, OrderType? orderType = null, OrderStatus? state = null, OrderCategory? category = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? fromId = null, string? toId = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get order details.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-details" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/order
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">["<c>ordId</c>"] Order ID</param>
    /// <param name="clientOrderId">["<c>clOrdId</c>"] Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder>> GetOrderDetailsAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Get the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-history-last-7-days" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/orders-history
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="orderType">["<c>ordType</c>"] Order Type</param>
    /// <param name="state">["<c>state</c>"] State</param>
    /// <param name="category">["<c>category</c>"] Category</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="fromId">["<c>after</c>"] Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">["<c>before</c>"] Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder[]>> GetOrderHistoryAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, OrderType? orderType = null, OrderStatus? state = null, OrderCategory? category = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100,
        string? instrumentFamily = null,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get all incomplete orders under the current account.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-order-list" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/orders-pending
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="orderType">["<c>ordType</c>"] Order Type</param>
    /// <param name="state">["<c>state</c>"] State</param>
    /// <param name="fromId">["<c>before</c>"] Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">["<c>after</c>"] Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrder[]>> GetOrdersAsync(InstrumentType? instrumentType = null, string? symbol = null, string? underlying = null, OrderType? orderType = null, OrderStatus? state = null, string? fromId = null, string? toId = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get recently-filled transaction details in the last 3 months.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-transaction-details-last-3-months" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/fills-history
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="orderId">["<c>ordId</c>"] Order ID</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="fromId">["<c>after</c>"] Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">["<c>before</c>"] Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransaction[]>> GetUserTradesArchiveAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Get recently-filled transaction details in the last 3 day.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-transaction-details-last-3-days" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/fills
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="orderId">["<c>ordId</c>"] Order ID</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="fromId">["<c>after</c>"] Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="toId">["<c>before</c>"] Pagination of data to return records newer than the requested ordId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransaction[]>> GetUserTradesAsync(InstrumentType? instrumentType = null, string? symbol = null, string? underlying = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Place new algo order. The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-place-algo-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/order-algo
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">["<c>tdMode</c>"] Trade Mode</param>
    /// <param name="orderSide">["<c>side</c>"] Order Side</param>
    /// <param name="algoOrderType">["<c>ordType</c>"] Algo Order Type</param>
    /// <param name="quantity">["<c>sz</c>"] Quantity</param>
    /// <param name="asset">["<c>ccy</c>"] Asset</param>
    /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce Only</param>
    /// <param name="positionSide">["<c>posSide</c>"] Position Side</param>
    /// <param name="quantityType">["<c>tgtCcy</c>"] Quantity Type</param>
    /// <param name="tpTriggerPxType">["<c>tpTriggerPxType</c>"] Take-profit trigger price type</param>
    /// <param name="tpTriggerPrice">["<c>tpTriggerPx</c>"] Take Profit Trigger Price</param>
    /// <param name="tpOrderPrice">["<c>tpOrdPx</c>"] Take Profit Order Price</param>
    /// <param name="slTriggerPxType">["<c>slTriggerPxType</c>"] Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slTriggerPrice">["<c>slTriggerPx</c>"] Stop Loss Trigger Price</param>
    /// <param name="slOrderPrice">["<c>slOrdPx</c>"] Stop Loss Order Price</param>
    /// <param name="triggerPrice">["<c>triggerPx</c>"] Trigger Price</param>
    /// <param name="orderPrice">["<c>orderPx</c>"] Order Price</param>
    /// <param name="pxVar">["<c>pxVar</c>"] Price Variance</param>
    /// <param name="priceRatio">["<c>pxSpread</c>"] Price Ratio</param>
    /// <param name="sizeLimit">["<c>szLimit</c>"] Size Limit</param>
    /// <param name="priceLimit">["<c>pxLimit</c>"] Price Limit</param>
    /// <param name="callbackRatio">["<c>callbackRatio</c>"] Callback ratio</param>
    /// <param name="callbackSpread">["<c>callbackSpread</c>"] Callback spread</param>
    /// <param name="activePx">["<c>activePx</c>"] Active price</param>
    /// <param name="timeInterval">["<c>timeInterval</c>"] Time Interval</param>
    /// <param name="closeFraction">["<c>closeFraction</c>"] Fraction of position to be closed when the algo order is triggered. Currently the system supports fully closing the position only so the only accepted value is 1.</param>
    /// <param name="cancelOnClose">["<c>cxlOnClosePos</c>"] Whether the TP/SL order placed by the user is associated with the corresponding position of the instrument. If it is associated, the TP/SL order will be cancelled when the position is fully closed; if it is not, the TP/SL order will not be affected when the position is fully closed.</param>
    /// <param name="quickMarginType">["<c>quickMgnType</c>"] Quick Margin type. Only applicable to Quick Margin Mode of isolated margin</param>
    /// <param name="clientOrderId">["<c>clOrdId</c>"] Client order id</param>
    /// <param name="chaseType">["<c>chaseType</c>"] Chase order value type</param>
    /// <param name="chaseValue">["<c>chaseVal</c>"] Chase value, with chaseType.Distance it represents the USD chase value, with chaseType.Ratio 0.1 means 10%</param>
    /// <param name="maxChaseType">["<c>maxChaseType</c>"] Max chase order value type</param>
    /// <param name="maxChaseValue">["<c>maxChaseVal</c>"] Max chase value, with chaseType.Distance it represents the USD chase value, with chaseType.Ratio 0.1 means 10%</param>
    /// <param name="tradeQuoteAsset">["<c>tradeQuoteCcy</c>"] The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
    /// <param name="advancedOrderType">["<c>advanceOrdType</c>"] Advanced order type for trigger orders</param>
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
        AdvancedOrderType? advancedOrderType = null,
        CancellationToken ct = default);

    /// <summary>
    /// Place orders in batches. Maximum 20 orders can be placed at a time.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-multiple-orders" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/batch-orders
    /// </para>
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Place a new order
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/order
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">["<c>tdMode</c>"] Trade Mode</param>
    /// <param name="side">["<c>side</c>"] Order Side</param>
    /// <param name="positionSide">["<c>posSide</c>"] Position Side</param>
    /// <param name="type">["<c>ordType</c>"] Order Type</param>
    /// <param name="quantity">["<c>sz</c>"] Quantity</param>
    /// <param name="price">["<c>px</c>"] Price</param>
    /// <param name="asset">["<c>ccy</c>"] Asset</param>
    /// <param name="attachedAlgoOrders">["<c>attachAlgoOrds</c>"] Attached take profit / stop loss orders</param>
    /// <param name="clientOrderId">["<c>clOrdId</c>"] Client Order ID</param>
    /// <param name="reduceOnly">["<c>reduceOnly</c>"] Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="quickMarginType">["<c>quickMgnType</c>"] Quick margin type</param>
    /// <param name="selfTradePreventionId">["<c>stpId</c>"] Self trade prevention id</param>
    /// <param name="selfTradePreventionMode">["<c>stpMode</c>"] Self trade prevention mode</param>
    /// <param name="quantityAsset">["<c>tgtCcy</c>"] Asset of the quantity when placing market order</param>
    /// <param name="tag">["<c>tag</c>"] Order tag</param>
    /// <param name="priceUsd">["<c>pxUsd</c>"] Place options orders in USD, only applicable to OPTIONS</param>
    /// <param name="priceVol">["<c>pxVol</c>"] Place options orders based on implied volatility, where 1 represents 100%. Only applicable to OPTIONS</param>
    /// <param name="banAmend">["<c>banAmend</c>"] Whether to disallow the system from amending the size of the SPOT Market Order, if true, system will not amend and reject the market order if user does not have sufficient funds.</param>
    /// <param name="tradeQuoteAsset">["<c>tradeQuoteCcy</c>"] The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
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
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">["<c>tdMode</c>"] Trade Mode</param>
    /// <param name="side">["<c>side</c>"] Order Side</param>
    /// <param name="positionSide">["<c>posSide</c>"] Position Side</param>
    /// <param name="type">["<c>ordType</c>"] Order Type</param>
    /// <param name="quantity">["<c>sz</c>"] Quantity</param>
    /// <param name="price">["<c>px</c>"] Price</param>
    /// <param name="reduceOnly">["<c>reduceOnly</c>"] Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="takeProfitTriggerPrice">["<c>tpTriggerPx</c>"] Take profit trigger price</param>
    /// <param name="stopLossTriggerPrice">["<c>slTriggerPx</c>"] Stop loss trigger price</param>
    /// <param name="takeProfitOrderPrice">["<c>tpOrdPx</c>"] Take profit order price</param>
    /// <param name="stopLossOrderPrice">["<c>slOrdPx</c>"] Stop loss order price</param>
    /// <param name="takeProfitTriggerPriceType">["<c>tpTriggerPxType</c>"] Take profit price type</param>
    /// <param name="stopLossTriggerPriceType">["<c>slTriggerPxType</c>"] Stop loss price type</param>
    /// <param name="quantityAsset">["<c>tgtCcy</c>"] Asset of the quantity when placing market order</param>
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
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-details" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/order-algo
    /// </para>
    /// </summary>
    /// <param name="algoId">["<c>algoId</c>"] Algo id, this or clientAlgoId should be provided</param>
    /// <param name="clientAlgoId">["<c>algoClOrdId</c>"] Client algo order id, this or algoId should be provided</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAlgoOrder>> GetAlgoOrderAsync(string? algoId = null, string? clientAlgoId = null, CancellationToken ct = default);

    /// <summary>
    /// Edit an incomplete algo order.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-amend-algo-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/amend-algos
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="algoId">["<c>algoId</c>"] Algo ID</param>
    /// <param name="clientAlgoId">["<c>algoClOrdId</c>"] Client Algo Order ID</param>
    /// <param name="requestId">["<c>reqId</c>"] Request ID</param>
    /// <param name="cancelOnFail">["<c>cxlOnFail</c>"] Cancel On Fail</param>
    /// <param name="newQuantity">["<c>newSz</c>"] New Quantity</param>
    /// <param name="newTakeProfitTriggerPrice">["<c>newTpTriggerPx</c>"] New take profit trigger price</param>
    /// <param name="newStopLossTriggerPrice">["<c>newSlTriggerPx</c>"] New stop loss trigger price</param>
    /// <param name="newTakeProfitOrderPrice">["<c>newTpOrdPx</c>"] New take profit order price</param>
    /// <param name="newStopLossOrderPrice">["<c>newSlOrdPx</c>"] New stop loss order price</param>
    /// <param name="newTakeProfitPriceTriggerType">["<c>newTpTriggerPxType</c>"] New take profit price trigger type</param>
    /// <param name="newStopLossPriceTriggerType">["<c>newSlTriggerPxType</c>"] New stop loss price trigger type</param>
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

