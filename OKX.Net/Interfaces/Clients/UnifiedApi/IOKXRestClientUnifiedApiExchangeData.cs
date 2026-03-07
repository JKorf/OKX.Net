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
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-24h-total-volume" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/platform-24-volume
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default);

    /// <summary>
    /// Get block ticker. Retrieve the latest block trading volume in the last 24 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-ticker" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/block-ticker
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get block tickers. Retrieve the latest block trading volume in the last 24 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-tickers" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/block-tickers
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Type of instrument</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTicker[]>> GetBlockTickersAsync(InstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get block trades. Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-trades" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/block-trades
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXBlockTrade[]>> GetBlockTradesAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-delivery-exercise-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/delivery-exercise-history
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDeliveryExerciseHistory[]>> GetDeliveryExerciseHistoryAsync(InstrumentType instrumentType, string? underlying = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get discount rate level and interest-free quota.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-discount-rate-and-interest-free-quota" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/discount-rate-interest-free-quota
    /// </para>
    /// </summary>
    /// <param name="discountLevel">["<c>discountLv</c>"] Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDiscountInfo[]>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default);

    /// <summary>
    /// Get the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-delivery-exercise-price" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/estimated-price
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-200214`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get funding rate history. This endpoint can retrieve data from the last 3 months.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/funding-rate-history
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 400; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get funding rates
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/funding-rate
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`. If not provided funding rates for all SWAP symbols will be returned</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingRate[]>> GetFundingRatesAsync(string? symbol = null, CancellationToken ct = default);

    /// <summary>
    /// Get the index component information data on the market
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-components" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/index-components
    /// </para>
    /// </summary>
    /// <param name="index">["<c>index</c>"] Index, for example `BTC-USD`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default);

    /// <summary>
    /// Get the kline/candlestick data of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-candlesticks" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/index-candles
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD`</param>
    /// <param name="period">["<c>bar</c>"] Kline interval</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetIndexKlinesAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get index tickers.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-tickers" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/index-tickers
    /// </para>
    /// </summary>
    /// <param name="quoteAsset">["<c>quoteCcy</c>"] Quote asset. Currently there is only an index with USD/USDT/BTC as the quote asset.</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXIndexTicker[]>> GetIndexTickersAsync(string? quoteAsset = null, string? symbol = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of instruments with open contracts.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-instruments" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/instruments
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="symbol">["<c>instId</c>"] Filter by symbol, for example `ETH-USDT`</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInstrument[]>> GetSymbolsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get insurance fund balance information
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-insurance-fund" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/insurance-fund
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="type">["<c>type</c>"] Insurance type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="asset">["<c>ccy</c>"] Asset</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Max number of results</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInsuranceFund>> GetInsuranceFundAsync(InstrumentType instrumentType, InsuranceType type = InsuranceType.All, string? underlying = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get margin interest rate
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/interest-rate-loan-quota
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Get history kline/candlestick data from recent years.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/history-candles
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="period">["<c>bar</c>"] Kline interval</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the kline/candlestick data. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/candles
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="period">["<c>bar</c>"] Kline interval</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetKlinesAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the highest buy limit and lowest sell limit of the instrument.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-limit-price" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/price-limit
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLimitPrice>> GetPriceLimitsAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the kline/candlestick data of the mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price-candlesticks" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/mark-price-candles
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="period">["<c>bar</c>"] Kline interval</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval period, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get mark price. Mark price is set based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/mark-price
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMarkPrice[]>> GetMarkPricesAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get the total open interest for contracts on OKX.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-open-interest" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/open-interest
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOpenInterest[]>> GetOpenInterestsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get option market data.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-option-market-data" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/opt-summary
    /// </para>
    /// </summary>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="expiryDate">["<c>expTime</c>"] Contract expiry date</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOptionSummary[]>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get a symbol order book.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-order-book" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/books
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="depth">["<c>sz</c>"] Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXOrderBook>> GetOrderBookAsync(string symbol, int depth = 1, CancellationToken ct = default);

    /// <summary>
    /// Get position information，Maximum leverage depends on your borrowings and margin ratio.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-position-tiers" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/position-tiers
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="marginMode">["<c>tdMode</c>"] Margin Mode</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="tier">["<c>tier</c>"] Tiers</param>
    /// <param name="asset">["<c>ccy</c>"] Margin currency</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
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
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/trades
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTrade[]>> GetRecentTradesAsync(string symbol, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get open interest and trading volume
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-contracts-open-interest-and-volume" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/contracts/open-interest-volume
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolume[]>> GetTradeStatsContractSummaryAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-expiry" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/option/open-interest-volume-expiry
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolumeExpiry[]>> GetTradeStatsInterestVolumeExpiryAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get what option strikes are the most popular for each expiration.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-strike" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/option/open-interest-volume-strike
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="expiryTime">["<c>expTime</c>"] expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">["<c>period</c>"] period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolumeStrike[]>> GetTradeStatsInterestVolumeStrikeAsync(string asset, string expiryTime, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-long-short-ratio" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/contracts/long-short-account-ratio
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXRatio[]>> GetTradeStatsLongShortRatioAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get the ratio of cumulative data value between currency pair leverage quote asset and underlying asset over a given period of time.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-margin-lending-ratio" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/margin/loan-ratio
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXRatio[]>> GetTradeStatsMarginLendingRatioAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get the sum of all open positions and how much total trading volume has taken place.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-options-open-interest-and-volume" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/option/open-interest-volume
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestVolume[]>> GetTradeStatsOptionsSummaryAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-put-call-ratio" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/option/open-interest-volume-ratio
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPutCallRatio[]>> GetTradeStatsPutCallRatioAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the assets supported by the transaction big data interface
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-support-coin" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/trading-data/support-coin
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSupportCoins>> GetTradeStatsSupportedAssetsAsync(CancellationToken ct = default);

    /// <summary>
    /// Get the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-flow" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/option/taker-block-volume
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="period">["<c>period</c>"] period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTakerFlow>> GetTradeStatsTakerFlowAsync(string asset, KlineInterval period = KlineInterval.FiveMinutes, CancellationToken ct = default);

    /// <summary>
    /// Get the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-volume" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/rubik/stat/taker-volume
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `BTC`</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="period">["<c>period</c>"] period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTakerVolume[]>> GetTradeStatsTakerVolumeAsync(string asset, InstrumentType instrumentType, KlineInterval period = KlineInterval.FiveMinutes, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    /// <summary>
    /// Get API server time.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-system-time" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/time
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

    /// <summary>
    /// Get the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-ticker" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/ticker
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-tickers" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/tickers
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTicker[]>> GetTickersAsync(InstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Get trades history
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/market/history-trades
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="type">["<c>type</c>"] Pagination Type</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTrade[]>> GetTradeHistoryAsync(string symbol, TradeHistoryPaginationType type = TradeHistoryPaginationType.TradeId, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get Underlying
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-underlying" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/underlying
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<string[]>> GetUnderlyingAsync(InstrumentType instrumentType, CancellationToken ct = default);

    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/vip-interest-rate-loan-quota
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXVipInterestRate[]>> GetVIPInterestRatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Convert units
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-unit-convert" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/convert-contract-coin
    /// </para>
    /// </summary>
    /// <param name="type">["<c>type</c>"] Convert type</param>
    /// <param name="unit">["<c>unit</c>"] The unit of currency</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="price">["<c>px</c>"] Order price</param>
    /// <param name="quantity">["<c>sz</c>"] Quantity to buy or sell</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXUnitConvert>> UnitConvertAsync(ConvertType type, string symbol, decimal quantity, ConvertUnit? unit = null, decimal? price = null, CancellationToken ct = default);

    /// <summary>
    /// Get announcements
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#announcement-get-announcements" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/support/announcements
    /// </para>
    /// </summary>
    /// <param name="announcementType">["<c>annType</c>"] Announcement type filter</param>
    /// <param name="page">["<c>page</c>"] Page number</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXAnnouncementsPage>> GetAnnouncementsAsync(string? announcementType = null, int? page = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of different announcement type values
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#announcement-get-announcement-types" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/support/announcement-types
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXAnnouncementType[]>> GetAnnouncementTypesAsync(CancellationToken ct = default);

    /// <summary>
    /// Get estimated futures settlement price
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-future-settlement-price" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/estimated-settlement-info
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol name, for example `XRP-USDT-250307`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSettlementPrice>> GetEstimatedFuturesSettlementPriceAsync(string symbol, CancellationToken ct = default);

    /// <summary>
    /// Get settlement history
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#public-data-rest-api-get-futures-settlement-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/public/settlement-history
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol name, for example `XRP-USDT-250307`</param>
    /// <param name="startTime">["<c>after</c>"] Filter by start time</param>
    /// <param name="endTime">["<c>before</c>"] Filter by end time</param>
    /// <param name="limit">["<c>limit</c>"] Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSettlementInfo[]>> GetSettlementHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
}

