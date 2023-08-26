using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Sockets;
using OKX.Net.Enums;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.System;
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
    /// <param name="symbol">Symbol</param>
    /// <param name="algoId">Algo order id</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? algoId, Action<OKXAlgoOrderUpdate> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to algo orders (includes trigger order, oco order, conditional order) updates. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-algo-orders-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? instrumentFamily, Action<OKXAlgoOrderUpdate> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to order information updates. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? instrumentFamily, Action<OKXOrderUpdate> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to position information updates. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-websocket-positions-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="regularUpdates">If true will send updates regularly even if nothing has changed. If false only send update on change</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? instrumentFamily, bool regularUpdates, Action<OKXPosition> onData, CancellationToken ct = default);

    /// <summary>
    /// This push channel is only used as a risk warning, and is not recommended as a risk judgment for strategic trading. In the case that the market is volatile, there may be the possibility that the position has been liquidated at the same time that this message is pushed. The warning is sent when a position is at risk of liquidation for isolated margin positions.The warning is sent when all the positions are at risk of liquidation for cross margin positions.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-websocket-position-risk-warning" /></para>
    /// </summary>
    /// <param name="instrumentType">The instrument type</param>
    /// <param name="instrumentFamily">Optional instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(OKXInstrumentType instrumentType,
        string? instrumentFamily,
        Action<OKXPosition> onData,
        CancellationToken ct = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="side"></param>
    /// <param name="type"></param>
    /// <param name="tradeMode"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    /// <param name="positionSide"></param>
    /// <param name="quickMarginType"></param>
    /// <param name="selfTradePreventionId"></param>
    /// <param name="selfTradePreventionMode"></param>
    /// <param name="asset"></param>
    /// <param name="quantityAsset"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="reduceOnly"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<CallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(string symbol,
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
        bool? reduceOnly = null,
        CancellationToken ct = default);
}