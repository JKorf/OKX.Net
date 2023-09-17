using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Trading;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiExchangeData : IOKXRestClientUnifiedApiExchangeData
{
    private readonly OKXRestClientUnifiedApi _baseClient;

    #region Market Data
    private const string Endpoints_V5_Market_Tickers = "api/v5/market/tickers";
    private const string Endpoints_V5_Market_Ticker = "api/v5/market/ticker";
    private const string Endpoints_V5_Market_IndexTickers = "api/v5/market/index-tickers";
    private const string Endpoints_V5_Market_Books = "api/v5/market/books";
    private const string Endpoints_V5_Market_Candles = "api/v5/market/candles";
    private const string Endpoints_V5_Market_HistoryCandles = "api/v5/market/history-candles";
    private const string Endpoints_V5_Market_IndexCandles = "api/v5/market/index-candles";
    private const string Endpoints_V5_Market_MarkPriceCandles = "api/v5/market/mark-price-candles";
    private const string Endpoints_V5_Market_Trades = "api/v5/market/trades";
    private const string Endpoints_V5_Market_TradesHistory = "api/v5/market/history-trades";
    private const string Endpoints_V5_Market_Platform24Volume = "api/v5/market/platform-24-volume";
    private const string Endpoints_V5_Market_OpenOracle = "api/v5/market/open-oracle";
    private const string Endpoints_V5_Market_IndexComponents = "api/v5/market/index-components";
    private const string Endpoints_V5_Market_BlockTickers = "api/v5/market/block-tickers";
    private const string Endpoints_V5_Market_BlockTicker = "api/v5/market/block-ticker";
    private const string Endpoints_V5_Market_BlockTrades = "api/v5/market/block-trades";
    #endregion

    #region Public Data
    private const string Endpoints_V5_Public_Instruments = "api/v5/public/instruments";
    private const string Endpoints_V5_Public_DeliveryExerciseHistory = "api/v5/public/delivery-exercise-history";
    private const string Endpoints_V5_Public_OpenInterest = "api/v5/public/open-interest";
    private const string Endpoints_V5_Public_FundingRate = "api/v5/public/funding-rate";
    private const string Endpoints_V5_Public_FundingRateHistory = "api/v5/public/funding-rate-history";
    private const string Endpoints_V5_Public_PriceLimit = "api/v5/public/price-limit";
    private const string Endpoints_V5_Public_OptionSummary = "api/v5/public/opt-summary";
    private const string Endpoints_V5_Public_EstimatedPrice = "api/v5/public/estimated-price";
    private const string Endpoints_V5_Public_DiscountRateInterestFreeQuota = "api/v5/public/discount-rate-interest-free-quota";
    private const string Endpoints_V5_Public_Time = "api/v5/public/time";
    private const string Endpoints_V5_Public_LiquidationOrders = "api/v5/public/liquidation-orders";
    private const string Endpoints_V5_Public_MarkPrice = "api/v5/public/mark-price";
    private const string Endpoints_V5_Public_PositionTiers = "api/v5/public/position-tiers";
    private const string Endpoints_V5_Public_InterestRateLoanQuota = "api/v5/public/interest-rate-loan-quota";
    private const string Endpoints_V5_Public_VIPInterestRateLoanQuota = "api/v5/public/vip-interest-rate-loan-quota";
    private const string Endpoints_V5_Public_Underlying = "api/v5/public/underlying";
    private const string Endpoints_V5_Public_InsuranceFund = "api/v5/public/insurance-fund";
    private const string Endpoints_V5_Public_ConvertContractCoin = "api/v5/public/convert-contract-coin";
    #endregion

    #region Trading Data
    private const string Endpoints_V5_RubikStat_TradingDataSupportCoin = "api/v5/rubik/stat/trading-data/support-coin";
    private const string Endpoints_V5_RubikStat_TakerVolume = "api/v5/rubik/stat/taker-volume";
    private const string Endpoints_V5_RubikStat_MarginLoanRatio = "api/v5/rubik/stat/margin/loan-ratio";
    private const string Endpoints_V5_RubikStat_ContractsLongShortAccountRatio = "api/v5/rubik/stat/contracts/long-short-account-ratio";
    private const string Endpoints_V5_RubikStat_ContractsOpenInterestVolume = "api/v5/rubik/stat/contracts/open-interest-volume";
    private const string Endpoints_V5_RubikStat_OptionOpenInterestVolume = "api/v5/rubik/stat/option/open-interest-volume";
    private const string Endpoints_V5_RubikStat_OptionOpenInterestVolumeRatio = "api/v5/rubik/stat/option/open-interest-volume-ratio";
    private const string Endpoints_V5_RubikStat_OptionOpenInterestVolumeExpiry = "api/v5/rubik/stat/option/open-interest-volume-expiry";
    private const string Endpoints_V5_RubikStat_OptionOpenInterestVolumeStrike = "api/v5/rubik/stat/option/open-interest-volume-strike";
    private const string Endpoints_V5_RubikStat_OptionTakerBlockVolume = "api/v5/rubik/stat/option/taker-block-volume";
    #endregion

    internal OKXRestClientUnifiedApiExchangeData(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTicker>>> GetTickersAsync(OKXInstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTicker>>>(_baseClient.GetUri(Endpoints_V5_Market_Tickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTicker>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTicker>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTicker>>>(_baseClient.GetUri(Endpoints_V5_Market_Ticker), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXTicker>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXTicker>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXIndexTicker>>> GetIndexTickersAsync(string? quoteAsset = null, string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteCcy", quoteAsset);
        parameters.AddOptionalParameter("instId", symbol);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXIndexTicker>>>(_baseClient.GetUri(Endpoints_V5_Market_IndexTickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXIndexTicker>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXIndexTicker>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderBook>> GetOrderBookAsync(string symbol, int depth = 1, CancellationToken ct = default)
    {
        if (depth < 1 || depth > 400)
            throw new ArgumentException("Depth can be between 1-400.");

        var parameters = new Dictionary<string, object>
        {
            {"instId", symbol},
            {"sz", depth},
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOrderBook>>>(_baseClient.GetUri(Endpoints_V5_Market_Books), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success || result.Data.Data.Count() == 0) return result.AsError<OKXOrderBook>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXOrderBook>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        var orderbook = result.Data.Data.FirstOrDefault();
        orderbook.Symbol = symbol;
        return result.As(orderbook);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 300)
            throw new ArgumentException("Limit can be between 1-300.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXCandlestick>>>(_baseClient.GetUri(Endpoints_V5_Market_Candles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXCandlestick>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXCandlestick>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        foreach (var candle in result.Data.Data!) candle.Symbol = symbol;
        return result.As(result.Data.Data);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlineHistoryAsync(string symbol, OKXPeriod period, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXCandlestick>>>(_baseClient.GetUri(Endpoints_V5_Market_HistoryCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXCandlestick>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXCandlestick>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        foreach (var candle in result.Data.Data!) candle.Symbol = symbol;
        return result.As(result.Data.Data);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetIndexKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXCandlestick>>>(_baseClient.GetUri(Endpoints_V5_Market_IndexCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXCandlestick>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXCandlestick>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        foreach (var candle in result.Data.Data!) candle.Symbol = symbol;
        return result.As(result.Data.Data);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetMarkPriceKlinesAsync(string symbol, OKXPeriod period,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXCandlestick>>>(_baseClient.GetUri(Endpoints_V5_Market_MarkPriceCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXCandlestick>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXCandlestick>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        foreach (var candle in result.Data.Data!) candle.Symbol = symbol;
        return result.As(result.Data.Data);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTrade>>> GetRecentTradesAsync(string symbol, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 500)
            throw new ArgumentException("Limit can be between 1-500.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTrade>>>(_baseClient.GetUri(Endpoints_V5_Market_Trades), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTrade>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTrade>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTrade>>> GetTradeHistoryAsync(string symbol, OKXTradeHistoryPaginationType type = OKXTradeHistoryPaginationType.TradeId,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new TradeHistoryPaginationTypeConverter(false)));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTrade>>>(_baseClient.GetUri(Endpoints_V5_Market_TradesHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTrade>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTrade>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKX24HourVolume>>>(_baseClient.GetUri(Endpoints_V5_Market_Platform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKX24HourVolume>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKX24HourVolume>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOracle>> GetOracleAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOracle>>>(_baseClient.GetUri(Endpoints_V5_Market_OpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXOracle>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXOracle>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "index", index },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<OKXIndexComponents>>(_baseClient.GetUri(Endpoints_V5_Market_IndexComponents), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXIndexComponents>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXIndexComponents>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXBlockTicker>>> GetBlockTickersAsync(OKXInstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXBlockTicker>>>(_baseClient.GetUri(Endpoints_V5_Market_BlockTickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXBlockTicker>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXBlockTicker>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXBlockTicker>>>(_baseClient.GetUri(Endpoints_V5_Market_BlockTicker), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXBlockTicker>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXBlockTicker>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTrade>>> GetBlockTradesAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTrade>>>(_baseClient.GetUri(Endpoints_V5_Market_BlockTrades), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTrade>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTrade>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInstrument>>> GetSymbolsAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInstrument>>>(_baseClient.GetUri(Endpoints_V5_Public_Instruments), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInstrument>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInstrument>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(
        OKXInstrumentType instrumentType,
        string? underlying = null,
        DateTime? startTime = null,
        DateTime? endTime = null, 
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Futures, OKXInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Futures or Option.");

        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) }
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXDeliveryExerciseHistory>>>(_baseClient.GetUri(Endpoints_V5_Public_DeliveryExerciseHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXDeliveryExerciseHistory>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXDeliveryExerciseHistory>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOpenInterest>>> GetOpenInterestsAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        if (instrumentType == OKXInstrumentType.Swap && string.IsNullOrEmpty(underlying))
            throw new ArgumentException("Underlying is required for Option.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOpenInterest>>>(_baseClient.GetUri(Endpoints_V5_Public_OpenInterest), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOpenInterest>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOpenInterest>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingRate>>> GetFundingRatesAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXFundingRate>>>(_baseClient.GetUri(Endpoints_V5_Public_FundingRate), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXFundingRate>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXFundingRate>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXFundingRateHistory>>>(_baseClient.GetUri(Endpoints_V5_Public_FundingRateHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXFundingRateHistory>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXFundingRateHistory>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXLimitPrice>> GetLimitPriceAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXLimitPrice>>>(_baseClient.GetUri(Endpoints_V5_Public_PriceLimit), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXLimitPrice>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXLimitPrice>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOptionSummary>>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uly", underlying },
        };
        parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXOptionSummary>>>(_baseClient.GetUri(Endpoints_V5_Public_OptionSummary), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXOptionSummary>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXOptionSummary>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXEstimatedPrice>>>(_baseClient.GetUri(Endpoints_V5_Public_EstimatedPrice), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXEstimatedPrice>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXEstimatedPrice>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default)
    {

        if (discountLevel.HasValue && (discountLevel < 1 || discountLevel > 5))
            throw new ArgumentException("Limit can be between 1-5.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("discountLv", discountLevel?.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXDiscountInfo>>>(_baseClient.GetUri(Endpoints_V5_Public_DiscountRateInterestFreeQuota), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXDiscountInfo>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXDiscountInfo>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTime>>>(_baseClient.GetUri(Endpoints_V5_Public_Time), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<DateTime>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<DateTime>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault().Time);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMarkPrice>>> GetMarkPricesAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Margin, OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXMarkPrice>>>(_baseClient.GetUri(Endpoints_V5_Public_MarkPrice), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXMarkPrice>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXMarkPrice>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPositionTier>>> GetPositionTiersAsync(
        OKXInstrumentType instrumentType,
        OKXMarginMode marginMode,
        string underlying,
        string? symbol = null,
        string? tier = null,
        string? asset = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Margin, OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "tdMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("tier", tier);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXPositionTier>>>(_baseClient.GetUri(Endpoints_V5_Public_PositionTiers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXPositionTier>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXPositionTier>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestRate>>>(_baseClient.GetUri(Endpoints_V5_Public_InterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXInterestRate>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXInterestRate>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXVipInterestRate>>>(_baseClient.GetUri(Endpoints_V5_Public_VIPInterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXVipInterestRate>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXVipInterestRate>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<string>>> GetUnderlyingAsync(OKXInstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<IEnumerable<string>>>>(_baseClient.GetUri(Endpoints_V5_Public_Underlying), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<string>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInsuranceFund>> GetInsuranceFundAsync(
        OKXInstrumentType instrumentType,
        OKXInsuranceType type = OKXInsuranceType.All,
        string? underlying = null,
        string? asset = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Margin, OKXInstrumentType.Swap, OKXInstrumentType.Futures, OKXInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Margin, Swap, Futures or Option.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        if (type != OKXInsuranceType.All)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new InsuranceTypeConverter(false)));

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInsuranceFund>>>(_baseClient.GetUri(Endpoints_V5_Public_InsuranceFund), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXInsuranceFund>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXInsuranceFund>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXUnitConvert>> UnitConvertAsync(
        OKXConvertType type,
        string symbol,
        decimal quantity,
        OKXConvertUnit? unit = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ConvertTypeConverter(false)));
        if (unit != null) parameters.AddOptionalParameter("unit", JsonConvert.SerializeObject(type, new ConvertUnitConverter(false)));
        if (!string.IsNullOrEmpty(symbol)) parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("sz", quantity.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXUnitConvert>>>(_baseClient.GetUri(Endpoints_V5_Public_ConvertContractCoin), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXUnitConvert>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXUnitConvert>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSupportCoins>> GetRubikSupportCoinAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<OKXSupportCoins>>(_baseClient.GetUri(Endpoints_V5_RubikStat_TradingDataSupportCoin), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXSupportCoins>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXSupportCoins>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTakerVolume>>> GetRubikTakerVolumeAsync(
        string asset,
        OKXInstrumentType instrumentType,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTakerVolume>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_TakerVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXTakerVolume>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXTakerVolume>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikMarginLendingRatioAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXRatio>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_MarginLoanRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXRatio>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXRatio>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikLongShortRatioAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXRatio>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_ContractsLongShortAccountRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXRatio>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXRatio>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikContractSummaryAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestVolume>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_ContractsOpenInterestVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInterestVolume>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInterestVolume>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikOptionsSummaryAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestVolume>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInterestVolume>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInterestVolume>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPutCallRatio>>> GetRubikPutCallRatioAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXPutCallRatio>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXPutCallRatio>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXPutCallRatio>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolumeExpiry>>> GetRubikInterestVolumeExpiryAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestVolumeExpiry>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeExpiry), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInterestVolumeExpiry>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInterestVolumeExpiry>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolumeStrike>>> GetRubikInterestVolumeStrikeAsync(
        string asset,
        string expiryTime,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "expTime", expiryTime},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestVolumeStrike>>>(_baseClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeStrike), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInterestVolumeStrike>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInterestVolumeStrike>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTakerFlow>> GetRubikTakerFlowAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<OKXTakerFlow>>(_baseClient.GetUri(Endpoints_V5_RubikStat_OptionTakerBlockVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXTakerFlow>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXTakerFlow>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }
}
