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
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-24h-total-volume" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default);

    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// <para><a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-ticker" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get block tickers
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// <para><a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-tickers" /></para>
    /// </summary>
    /// <param name="instrumentType">Type of instrument</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXBlockTicker>>> GetBlockTickersAsync(OKXInstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// <para><a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-trades" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXTrade>>> GetBlockTradesAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
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
    Task<WebCallResult<IEnumerable<OKXDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(OKXInstrumentType instrumentType, string? underlying = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve discount rate level and interest-free quota.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-discount-rate-and-interest-free-quota" /></para>
    /// </summary>
    /// <param name="discountLevel">Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-delivery-exercise-price" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Retrieve funding rate.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXFundingRate>>> GetFundingRatesAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the index component information data on the market
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-components" /></para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-candlesticks" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetIndexKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Retrieve index tickers.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-tickers" /></para>
    /// </summary>
    /// <param name="quoteAsset">Quote asset. Currently there is only an index with USD/USDT/BTC as the quote asset.</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXIndexTicker>>> GetIndexTickersAsync(string? quoteAsset = null, string? symbol = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve a list of instruments with open contracts.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-instruments" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXInstrument>>> GetSymbolsAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get insurance fund
    /// Get insurance fund balance information
    /// Rate Limit: 10 requests per 2 seconds
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-insurance-fund" /></para>
    /// </summary>
    /// <param name="instrumentType"></param>
    /// <param name="type"></param>
    /// <param name="underlying"></param>
    /// <param name="asset"></param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit"></param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInsuranceFund>> GetInsuranceFundAsync(OKXInstrumentType instrumentType, OKXInsuranceType type = OKXInsuranceType.All, string? underlying = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get margin interest rate
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlineHistoryAsync(string symbol, OKXPeriod period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the highest buy limit and lowest sell limit of the instrument.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-limit-price" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLimitPrice>> GetLimitPriceAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price-candlesticks" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetMarkPriceKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Retrieve mark price.
    /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXMarkPrice>>> GetMarkPricesAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the total open interest for contracts on OKX.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-open-interest" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXOpenInterest>>> GetOpenInterestsAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve option market data.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-option-market-data" /></para>
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="expiryDate">Contract expiry date</param>
    /// <param name="instrumentFamily">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXOptionSummary>>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get the crypto price of signing using Open Oracle smart contract.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-oracle" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOracle>> GetOracleAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieve a symbol order book.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-order-book" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderBook>> GetOrderBookAsync(string symbol, int depth = 1, CancellationToken ct = default);

    /// <summary>
    /// Position information，Maximum leverage depends on your borrowings and margin ratio.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-position-tiers" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="tier">Tiers</param>
    /// <param name="asset">Margin currency</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXPositionTier>>> GetPositionTiersAsync(
        OKXInstrumentType instrumentType,
        OKXMarginMode marginMode,
        string underlying,
        string? symbol = null,
        string? tier = null,
        string? asset = null,
        string? instrumentFamily = null,
        CancellationToken ct = default);

    /// <summary>
    /// Retrieve the recent transactions of an instrument.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXTrade>>> GetRecentTradesAsync(string symbol, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Open interest is the sum of all long and short futures and perpetual swap positions.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-contracts-open-interest-and-volume" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikContractSummaryAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-expiry" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXInterestVolumeExpiry>>> GetRubikInterestVolumeExpiryAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// This shows what option strikes are the most popular for each expiration.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-strike" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXInterestVolumeStrike>>> GetRubikInterestVolumeStrikeAsync(string asset, string expiryTime, OKXPeriod period = OKXPeriod.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-long-short-ratio" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikLongShortRatioAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-margin-lending-ratio" /></para>
    /// </summary>
    /// <param name="asset">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikMarginLendingRatioAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// This shows the sum of all open positions and how much total trading volume has taken place.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-options-open-interest-and-volume" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikOptionsSummaryAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-put-call-ratio" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXPutCallRatio>>> GetRubikPutCallRatioAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the currency supported by the transaction big data interface
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-support-coin" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSupportCoins>> GetRubikSupportCoinAsync(CancellationToken ct = default);

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-flow" /></para>
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTakerFlow>> GetRubikTakerFlowAsync(string asset, OKXPeriod period = OKXPeriod.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-volume" /></para>
    /// </summary>
    /// <param name="asset">Currency</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXTakerVolume>>> GetRubikTakerVolumeAsync(string asset, OKXInstrumentType instrumentType, OKXPeriod period = OKXPeriod.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve API server time.
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-system-time" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-ticker" /></para>
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-tickers" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXTicker>>> GetTickersAsync(OKXInstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get trades history
    /// Retrieve the recent transactions of an instrument from the last 3 months with pagination.
    /// Rate Limit: 10 requests per 2 seconds
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades-history" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, e.g. BTC-USDT</param>
    /// <param name="type">Pagination Type</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXTrade>>> GetTradeHistoryAsync(string symbol, OKXTradeHistoryPaginationType type = OKXTradeHistoryPaginationType.TradeId, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get Underlying
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-underlying" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<string>>> GetUnderlyingAsync(OKXInstrumentType instrumentType, CancellationToken ct = default);

    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<IEnumerable<OKXVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Convert units
    /// <para><a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-unit-convert" /></para>
    /// </summary>
    /// <param name="type">Convert type</param>
    /// <param name="unit">The unit of currency</param>
    /// <param name="symbol">Symbol</param>
    /// <param name="price">Order price</param>
    /// <param name="quantity">Quantity to buy or sell</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXUnitConvert>> UnitConvertAsync(OKXConvertType type, string symbol, decimal quantity, OKXConvertUnit? unit = null, decimal? price = null, CancellationToken ct = default);
}