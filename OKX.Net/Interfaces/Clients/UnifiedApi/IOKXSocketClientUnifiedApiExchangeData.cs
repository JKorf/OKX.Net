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
public interface IOKXSocketClientUnifiedApiExchangeData
{
    /// <summary>
    /// Subscribe to the estimated delivery/exercise price of FUTURES contracts and OPTION updates.
    /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-estimated-delivery-exercise-price-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument family. Required if symbol is not set</param>
    /// <param name="symbol">Symbol. Required if instrumentFamily is not set</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceUpdatesAsync(OKXInstrumentType instrumentType, string? instrumentFamily, string? symbol, Action<OKXEstimatedPrice> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to funding rate updates. Data will be pushed every minute.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-funding-rate-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<OKXFundingRate> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the candlesticks data of the index updates. Data will be pushed every 500 ms.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-index-candlesticks-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period">Period</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string symbol, OKXPeriod period, Action<OKXCandlestick> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to index tickers data updates
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-index-tickers-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(string symbol, Action<OKXIndexTicker> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the open interest updates. Data will by pushed every 3 seconds.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-open-interest-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(string symbol, Action<OKXOpenInterest> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the candlesticks data updates of a symbol. Data will be pushed every 500 ms.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-ws-candlesticks-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period"></param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, OKXPeriod period, Action<OKXCandlestick> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the mark price updates. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-mark-price-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<OKXMarkPrice> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the candlesticks data updates of the mark price. Data will be pushed every 500 ms.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-mark-price-candlesticks-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period">Period</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, OKXPeriod period, Action<OKXCandlestick> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to detailed pricing information updates of all OPTION contracts. Data will be pushed at once.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-option-summary-channel" /></para>
    /// </summary>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryUpdatesAsync(string instrumentFamily, Action<OKXOptionSummary> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to order book data updates.
    /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
    /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
    /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
    /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-ws-order-book-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="orderBookType">Order Book Type</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, OKXOrderBookType orderBookType, Action<DataEvent<OKXOrderBook>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the maximum buy price and minimum sell price of the instrument updates. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-price-limit-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(string symbol, Action<OKXLimitPrice> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to symbols updates. The instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-websocket-instruments-channel" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(OKXInstrumentType instrumentType, Action<OKXInstrument> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to status updates of system maintenance and push when the system maintenance status changes. First subscription: "Push the latest change data"; every time there is a state change, push the changed content
    /// <para><a href="https://www.okx.com/docs-v5/en/#status-ws-status-channel" /></para>
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<OKXStatus> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the last traded price updates, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-ws-tickers-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<OKXTicker> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to the recent trades data updates. Data will be pushed whenever there is a trade.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-ws-trades-channel" /></para>
    /// </summary>
    /// <param name="symbol">Symbols</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<OKXTrade> onData, CancellationToken ct = default);

}