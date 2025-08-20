using OKX.Net.Enums;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Trading;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified exchange data endpoints
/// </summary>
public interface IOKXRestClientUnifiedApiExchangeData
{
    /// <summary>
    /// Get 24 hour volumes. The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-24h-total-volume" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default);

    /// <summary>
    /// Get block ticker. Retrieve the latest block trading volume in the last 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-ticker" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get block tickers. Retrieve the latest block trading volume in the last 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-tickers" /></para>
    /// </summary>
    /// <param name="instrumentType">Type of instrument</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTicker[]>> GetBlockTickersAsync(InstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get block trades. Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// <para><a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-trades" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTrade[]>> GetBlockTradesAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-delivery-exercise-history" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDeliveryExerciseHistory[]>> GetDeliveryExerciseHistoryAsync(InstrumentType instrumentType, string? underlying = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get discount rate level and interest-free quota.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-discount-rate-and-interest-free-quota" /></para>
    /// </summary>
    /// <param name="discountLevel">Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDiscountInfo[]>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default);

    /// <summary>
    /// Get the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-delivery-exercise-price" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-200214`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get funding rate history. This endpoint can retrieve data from the last 3 months.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 400; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get funding rates
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`. If not provided funding rates for all SWAP symbols will be returned</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingRate[]>> GetFundingRatesAsync(string? symbol = null, CancellationToken ct = default);

    /// <summary>
    /// Get the index component information data on the market
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-components" /></para>
    /// </summary>
    /// <param name="index">Index, for example `BTC-USD`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default);

    /// <summary>
    /// Get the kline/candlestick data of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-candlesticks" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD`</param>
    /// <param name="period">Kline interval</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetIndexKlinesAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get index tickers.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-tickers" /></para>
    /// </summary>
    /// <param name="quoteAsset">Quote asset. Currently there is only an index with USD/USDT/BTC as the quote asset.</param>
    /// <param name="symbol">Symbol, for example `BTC-USD`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXIndexTicker[]>> GetIndexTickersAsync(string? quoteAsset = null, string? symbol = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of instruments with open contracts.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-instruments" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Filter by symbol, for example `ETH-USDT`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInstrument[]>> GetSymbolsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get insurance fund balance information
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-insurance-fund" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="type">Insurance type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="asset">Asset</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Max number of results</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInsuranceFund>> GetInsuranceFundAsync(InstrumentType instrumentType, InsuranceType type = InsuranceType.All, string? underlying = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get margin interest rate
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Get history kline/candlestick data from recent years.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="period">Kline interval</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the kline/candlestick data. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="period">Kline interval</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetKlinesAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the highest buy limit and lowest sell limit of the instrument.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-limit-price" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLimitPrice>> GetPriceLimitsAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the kline/candlestick data of the mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price-candlesticks" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="period">Kline interval</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get mark price. Mark price is set based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMarkPrice[]>> GetMarkPricesAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get the total open interest for contracts on OKX.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-open-interest" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOpenInterest[]>> GetOpenInterestsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get option market data.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-option-market-data" /></para>
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="expiryDate">Contract expiry date</param>
    /// <param name="instrumentFamily">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOptionSummary[]>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get a symbol order book.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-order-book" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderBook>> GetOrderBookAsync(string symbol, int depth = 1, CancellationToken ct = default);

    /// <summary>
    /// Get position information，Maximum leverage depends on your borrowings and margin ratio.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-position-tiers" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="tier">Tiers</param>
    /// <param name="asset">Margin currency</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPositionTier[]>> GetPositionTiersAsync(
        InstrumentType instrumentType,
        MarginMode marginMode,
        string underlying,
        string? symbol = null,
        string? tier = null,
        string? asset = null,
        string? instrumentFamily = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get the recent transactions of an instrument.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTrade[]>> GetRecentTradesAsync(string symbol, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get open interest and trading volume
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-contracts-open-interest-and-volume" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolume[]>> GetTradeStatsContractSummaryAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-expiry" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolumeExpiry[]>> GetTradeStatsInterestVolumeExpiryAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get what option strikes are the most popular for each expiration.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-strike" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolumeStrike[]>> GetTradeStatsInterestVolumeStrikeAsync(string asset, string expiryTime, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-long-short-ratio" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXRatio[]>> GetTradeStatsLongShortRatioAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get the ratio of cumulative data value between currency pair leverage quote asset and underlying asset over a given period of time.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-margin-lending-ratio" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXRatio[]>> GetTradeStatsMarginLendingRatioAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get the sum of all open positions and how much total trading volume has taken place.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-options-open-interest-and-volume" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolume[]>> GetTradeStatsOptionsSummaryAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-put-call-ratio" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPutCallRatio[]>> GetTradeStatsPutCallRatioAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the assets supported by the transaction big data interface
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-support-coin" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSupportCoins>> GetTradeStatsSupportedAssetsAsync(CancellationToken ct = default);

    /// <summary>
    /// Get the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-flow" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTakerFlow>> GetTradeStatsTakerFlowAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-volume" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `BTC`</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTakerVolume[]>> GetTradeStatsTakerVolumeAsync(string asset, InstrumentType instrumentType, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get API server time.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-system-time" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

    /// <summary>
    /// Get the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-ticker" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-tickers" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTicker[]>> GetTickersAsync(InstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get trades history
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="type">Pagination Type</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTrade[]>> GetTradeHistoryAsync(string symbol, TradeHistoryPaginationType type = TradeHistoryPaginationType.TradeId, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get Underlying
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-underlying" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<string[]>> GetUnderlyingAsync(InstrumentType instrumentType, CancellationToken ct = default);

    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXVipInterestRate[]>> GetVIPInterestRatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Convert units
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-unit-convert" /></para>
    /// </summary>
    /// <param name="type">Convert type</param>
    /// <param name="unit">The unit of currency</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="price">Order price</param>
    /// <param name="quantity">Quantity to buy or sell</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXUnitConvert>> UnitConvertAsync(ConvertType type, string symbol, decimal quantity, ConvertUnit? unit = null, decimal? price = null, CancellationToken ct = default);

    /// <summary>
    /// Get announcements
    /// <para><a href="https://www.okx.com/docs-v5/en/#announcement-get-announcements" /></para>
    /// </summary>
    /// <param name="announcementType">Announcement type filter</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXAnnouncementsPage>> GetAnnouncementsAsync(string? announcementType = null, int? page = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of different announcement type values
    /// <para><a href="https://www.okx.com/docs-v5/en/#announcement-get-announcement-types" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXAnnouncementType[]>> GetAnnouncementTypesAsync(CancellationToken ct = default);

    /// <summary>
    /// Get estimated futures settlement price
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-future-settlement-price" /></para>
    /// </summary>
    /// <param name="symbol">Symbol name, for example `XRP-USDT-250307`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSettlementPrice>> GetEstimatedFuturesSettlementPriceAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get settlement history
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-futures-settlement-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol name, for example `XRP-USDT-250307`</param>
    /// <param name="startTime">Filter by start time</param>
    /// <param name="endTime">Filter by end time</param>
    /// <param name="limit">Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSettlementInfo[]>> GetSettlementHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
}