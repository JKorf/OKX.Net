using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Enums;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API
/// </summary>
public interface IOKXSocketClientUnifiedApiTrading
{
    /// <summary>
    /// Subscribe to advance algo orders (includes iceberg order and twap order) updates. Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-advance-algo-orders-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="algoId">Algo order id</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(InstrumentType instrumentType, string? symbol, string? algoId, Action<DataEvent<OKXAlgoOrderUpdate>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to algo orders (includes trigger order, oco order, conditional order) updates. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-algo-orders-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(InstrumentType instrumentType, string? symbol, string? instrumentFamily, Action<DataEvent<OKXAlgoOrderUpdate>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to order information updates. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(InstrumentType instrumentType, string? symbol, string? instrumentFamily, Action<DataEvent<OKXOrderUpdate>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to user trade updates. Note that this subscription is only available to VIP5 accounts or above.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-fills-channel" /></para>
    /// </summary>
    /// <param name="symbol">Filter updates by symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
        string? symbol,
        Action<DataEvent<OKXUserTradeUpdate>> onData,
        CancellationToken ct = default);

    /// <summary>
    /// Subscribe to position information updates. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-websocket-positions-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="regularUpdates">If true will send updates regularly even if nothing has changed. If false only send update on change</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(InstrumentType instrumentType, string? symbol, string? instrumentFamily, bool regularUpdates, Action<DataEvent<OKXPosition[]>> onData, CancellationToken ct = default);

    /// <summary>
    /// This push channel is only used as a risk warning, and is not recommended as a risk judgment for strategic trading. In the case that the market is volatile, there may be the possibility that the position has been liquidated at the same time that this message is pushed. The warning is sent when a position is at risk of liquidation for isolated margin positions.The warning is sent when all the positions are at risk of liquidation for cross margin positions.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-websocket-position-risk-warning" /></para>
    /// </summary>
    /// <param name="instrumentType">The instrument type</param>
    /// <param name="instrumentFamily">Optional instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(InstrumentType instrumentType,
        string? instrumentFamily,
        Action<DataEvent<OKXPosition>> onData,
        CancellationToken ct = default);

    /// <summary>
    /// Place a new order
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="side">Order Side</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="type">Order Type</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="price">Price</param>
    /// <param name="asset">Asset</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="quickMarginType">Quick margin type</param>
    /// <param name="selfTradePreventionId">Self trade prevention id</param>
    /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
    /// <param name="quantityAsset">Asset of the quantity when placing market order</param>
    /// <param name="tradeQuoteAsset">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<CallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(string symbol,
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
        CancellationToken ct = default);

    /// <summary>
    /// Place orders in a batch. Maximum 20 orders can be placed per request
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-multiple-orders" /></para>
    /// </summary>
    /// <param name="orders">The orders to place</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<CallResult<CallResult<OKXOrderPlaceResponse>[]>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders, CancellationToken ct = default);

    /// <summary>
    /// Cancel an incomplete order
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-cancel-order" /></para>
    /// </summary>
    /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">Cancel by order id. This or clientOrderId should be provided</param>
    /// <param name="clientOrderId">Cancel by client order id. This or orderId should be provided</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<CallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled per request.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-cancel-multiple-orders" /></para>
    /// </summary>
    /// <param name="ordersToCancel">Orders to cancel</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<CallResult<OKXOrderCancelResponse[]>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> ordersToCancel, CancellationToken ct = default);

    /// <summary>
    /// Edit an incomplete order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-order" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="orderId">Amend by order id. This or clientOrderId should be provided</param>
    /// <param name="clientOrderId">Amend by client order id. This or orderId should be provided</param>
    /// <param name="requestId">Request id</param>
    /// <param name="newQuantity">New quantity</param>
    /// <param name="newPrice">New price</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<CallResult<OKXOrderAmendResponse>> AmendOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        CancellationToken ct = default);

    /// <summary>
    /// Edit incomplete orders in batches. Maximum 20 orders can be amended per request.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-multiple-orders" /></para>
    /// </summary>
    /// <param name="ordersToCancel">Orders to cancel</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<CallResult<OKXOrderAmendResponse[]>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> ordersToCancel, CancellationToken ct = default);
}
