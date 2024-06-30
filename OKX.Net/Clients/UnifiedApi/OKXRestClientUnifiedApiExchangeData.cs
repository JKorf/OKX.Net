using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.ExtensionMethods;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Trading;
using System.Security.Cryptography;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiExchangeData : IOKXRestClientUnifiedApiExchangeData
{
    private readonly OKXRestClientUnifiedApi _baseClient;
    private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

    internal OKXRestClientUnifiedApiExchangeData(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTicker>>> GetTickersAsync(OKXInstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/tickers", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXTicker>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/ticker", OKXExchange.RateLimiter.Public, 1);
        var result = await _baseClient.SendAsync<IEnumerable<OKXTicker>>(request, parameters, ct).ConfigureAwait(false);
        return result.As<OKXTicker>(result.Data?.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXIndexTicker>>> GetIndexTickersAsync(string? quoteAsset = null, string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("quoteCcy", quoteAsset);
        parameters.AddOptionalParameter("instId", symbol);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/index-tickers", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXIndexTicker>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOrderBook>> GetOrderBookAsync(string symbol, int depth = 1, CancellationToken ct = default)
    {
        if (depth < 1 || depth > 400)
            throw new ArgumentException("Depth can be between 1-400.");

        var parameters = new ParameterCollection
        {
            {"instId", symbol},
            {"sz", depth},
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/books", OKXExchange.RateLimiter.Public, 1);
        var result = await _baseClient.SendAsync<IEnumerable<OKXOrderBook>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result.As<OKXOrderBook>(default);

        if (result.Data.Count() == 0)
            return result.AsError<OKXOrderBook>(new OKXRestApiError(null, "No data", null));

        var orderbook = result.Data.FirstOrDefault();
        orderbook.Symbol = symbol;
        return result.As(orderbook);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 300)
            throw new ArgumentException("Limit can be between 1-300.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/candles", OKXExchange.RateLimiter.Public, 1);
        var result = await _baseClient.SendAsync<IEnumerable<OKXCandlestick>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data) 
            candle.Symbol = symbol;
        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlineHistoryAsync(string symbol, OKXPeriod period, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/history-candles", OKXExchange.RateLimiter.Public, 1);
        var result = await _baseClient.SendAsync<IEnumerable<OKXCandlestick>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data) 
            candle.Symbol = symbol;

        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetIndexKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/index-candles", OKXExchange.RateLimiter.Public, 1);
        var result = await _baseClient.SendAsync<IEnumerable<OKXCandlestick>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data)
            candle.Symbol = symbol;

        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetMarkPriceKlinesAsync(string symbol, OKXPeriod period,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/mark-price-candles", OKXExchange.RateLimiter.Public, 1);
        var result = await _baseClient.SendAsync<IEnumerable<OKXCandlestick>>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data)
            candle.Symbol = symbol;

        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTrade>>> GetRecentTradesAsync(string symbol, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 500)
            throw new ArgumentException("Limit can be between 1-500.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/trades", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXTrade>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXTrade>>> GetTradeHistoryAsync(string symbol, OKXTradeHistoryPaginationType type = OKXTradeHistoryPaginationType.TradeId,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new TradeHistoryPaginationTypeConverter(false)));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/history-trades", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXTrade>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/platform-24-volume", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKX24HourVolume>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOracle>> GetOracleAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/open-oracle", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXOracle>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "index", index },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/index-components", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<OKXIndexComponents>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXBlockTicker>>> GetBlockTickersAsync(OKXInstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/block-tickers", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXBlockTicker>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/block-ticker", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXBlockTicker>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXBlockTrade>>> GetBlockTradesAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/block-trades", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXBlockTrade>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInstrument>>> GetSymbolsAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/instruments", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXInstrument>>(request, parameters, ct).ConfigureAwait(false);
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

        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) }
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/delivery-exercise-history", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXDeliveryExerciseHistory>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOpenInterest>>> GetOpenInterestsAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        if (instrumentType == OKXInstrumentType.Swap && string.IsNullOrEmpty(underlying))
            throw new ArgumentException("Underlying is required for Option.");

        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/open-interest", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXOpenInterest>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingRate>>> GetFundingRatesAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/funding-rate", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXFundingRate>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/funding-rate-history", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXFundingRateHistory>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXLimitPrice>> GetLimitPriceAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/price-limit", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXLimitPrice>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXOptionSummary>>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "uly", underlying },
        };
        parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/opt-summary", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXOptionSummary>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/estimated-price", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXEstimatedPrice>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default)
    {

        if (discountLevel.HasValue && (discountLevel < 1 || discountLevel > 5))
            throw new ArgumentException("Limit can be between 1-5.");

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("discountLv", discountLevel?.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/discount-rate-interest-free-quota", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXDiscountInfo>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/time", OKXExchange.RateLimiter.Public, 1);
        var result =  await _baseClient.SendGetSingleAsync<OKXTime>(request, null, ct).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMarkPrice>>> GetMarkPricesAsync(OKXInstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Margin, OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/mark-price", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXMarkPrice>>(request, parameters, ct).ConfigureAwait(false);
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

        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "tdMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("tier", tier);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/position-tiers", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXPositionTier>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/interest-rate-loan-quota", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXInterestRate>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/vip-interest-rate-loan-quota", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXVipInterestRate>>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<string>>> GetUnderlyingAsync(OKXInstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OKXInstrumentType.Futures, OKXInstrumentType.Option, OKXInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new ParameterCollection
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/underlying", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<IEnumerable<string>>(request, parameters, ct).ConfigureAwait(false);
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

        var parameters = new ParameterCollection
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/insurance-fund", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXInsuranceFund>(request, parameters, ct).ConfigureAwait(false);
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
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ConvertTypeConverter(false)));
        if (unit != null) parameters.AddOptionalParameter("unit", JsonConvert.SerializeObject(type, new ConvertUnitConverter(false)));
        if (!string.IsNullOrEmpty(symbol)) parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("sz", quantity.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/convert-contract-coin", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendGetSingleAsync<OKXUnitConvert>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSupportCoins>> GetRubikSupportCoinAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/trading-data/support-coin", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<OKXSupportCoins>(request, null, ct).ConfigureAwait(false);
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
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/taker-volume", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXTakerVolume>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikMarginLendingRatioAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/margin/loan-ratio", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXRatio>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikLongShortRatioAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/contracts/long-short-account-ratio", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXRatio>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikContractSummaryAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/contracts/open-interest-volume", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXInterestVolume>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikOptionsSummaryAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXInterestVolume>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPutCallRatio>>> GetRubikPutCallRatioAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume-ratio", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXPutCallRatio>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolumeExpiry>>> GetRubikInterestVolumeExpiryAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume-expiry", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXInterestVolumeExpiry>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestVolumeStrike>>> GetRubikInterestVolumeStrikeAsync(
        string asset,
        string expiryTime,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "expTime", expiryTime},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume-strike", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<IEnumerable<OKXInterestVolumeStrike>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTakerFlow>> GetRubikTakerFlowAsync(
        string asset,
        OKXPeriod period = OKXPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/taker-block-volume", OKXExchange.RateLimiter.Public, 1);
        return await _baseClient.SendAsync<OKXTakerFlow>(request, parameters, ct).ConfigureAwait(false);
    }
}
