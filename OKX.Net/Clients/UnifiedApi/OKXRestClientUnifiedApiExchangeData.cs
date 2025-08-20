using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.RateLimiting.Guards;
using OKX.Net.Enums;
using OKX.Net.ExtensionMethods;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Trading;

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
    public virtual async Task<WebCallResult<OKXTicker[]>> GetTickersAsync(InstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/tickers", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXTicker[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/ticker", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXTicker[]>(request, parameters, ct).ConfigureAwait(false);
        return result.As<OKXTicker>(result.Data?.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXIndexTicker[]>> GetIndexTickersAsync(string? quoteAsset = null, string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("quoteCcy", quoteAsset);
        parameters.AddOptionalParameter("instId", symbol);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/index-tickers", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXIndexTicker[]>(request, parameters, ct).ConfigureAwait(false);
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/books", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXOrderBook[]>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result.As<OKXOrderBook>(default);

        if (!result.Data.Any())
            return result.AsError<OKXOrderBook>(new ServerError(new ErrorInfo(ErrorType.Unknown, "No data")));

        var orderbook = result.Data.First();
        orderbook.Symbol = symbol;
        return result.As(orderbook);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXKline[]>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 300)
            throw new ArgumentException("Limit can be between 1-300.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };
        parameters.AddEnum("bar", klineInterval);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/candles", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXKline[]>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data)
            candle.Symbol = symbol;
        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 300)
            throw new ArgumentException("Limit can be between 1-300.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol }
        };
        parameters.AddEnum("bar", klineInterval);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/history-candles", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXKline[]>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data)
            candle.Symbol = symbol;

        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXKline[]>> GetIndexKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol }
        };
        parameters.AddEnum("bar", klineInterval);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/index-candles", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXKline[]>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data)
            candle.Symbol = symbol;

        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval klineInterval,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol }
        };
        parameters.AddEnum("bar", klineInterval);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/mark-price-candles", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXKline[]>(request, parameters, ct).ConfigureAwait(false);
        if (!result)
            return result;

        foreach (var candle in result.Data)
            candle.Symbol = symbol;

        return result;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTrade[]>> GetRecentTradesAsync(string symbol, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 500)
            throw new ArgumentException("Limit can be between 1-500.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/trades", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXTrade[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTrade[]>> GetTradeHistoryAsync(string symbol, TradeHistoryPaginationType type = TradeHistoryPaginationType.TradeId,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        parameters.AddEnum("type", type);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/history-trades", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXTrade[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/platform-24-volume", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKX24HourVolume>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "index", index },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/index-components", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXIndexComponents>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXBlockTicker[]>> GetBlockTickersAsync(InstrumentType instrumentType, string? underlying = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/block-tickers", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXBlockTicker[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/market/block-ticker", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKXBlockTicker>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXBlockTrade[]>> GetBlockTradesAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/block-trades", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXBlockTrade[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInstrument[]>> GetSymbolsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/instruments", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXInstrument[]>(request, parameters, ct, rateLimitKeySuffix: instrumentType.ToString()).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXDeliveryExerciseHistory[]>> GetDeliveryExerciseHistoryAsync(
        InstrumentType instrumentType,
        string? underlying = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(InstrumentType.Futures, InstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Futures or Option.");

        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/delivery-exercise-history", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXDeliveryExerciseHistory[]>(request, parameters, ct, rateLimitKeySuffix: instrumentType.ToString() + underlying).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOpenInterest[]>> GetOpenInterestsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(InstrumentType.Futures, InstrumentType.Option, InstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/open-interest", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXOpenInterest[]>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXFundingRate[]>> GetFundingRatesAsync(string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol ?? "ANY" },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/funding-rate", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXFundingRate[]>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol,
        DateTime? startTime = null,
        DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 400)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/funding-rate-history", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXFundingRateHistory[]>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXLimitPrice>> GetPriceLimitsAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/price-limit", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKXLimitPrice>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXOptionSummary[]>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "uly", underlying },
        };
        parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/opt-summary", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXOptionSummary[]>(request, parameters, ct, rateLimitKeySuffix: underlying).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", symbol },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/estimated-price", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKXEstimatedPrice>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXDiscountInfo[]>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default)
    {

        if (discountLevel.HasValue && (discountLevel < 1 || discountLevel > 5))
            throw new ArgumentException("Limit can be between 1-5.");

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("discountLv", discountLevel?.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/discount-rate-interest-free-quota", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXDiscountInfo[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/time", OKXExchange.RateLimiter.EndpointGate, 1, false, preventCaching: true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendGetSingleAsync<OKXTime>(request, null, ct).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXMarkPrice[]>> GetMarkPricesAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(InstrumentType.Margin, InstrumentType.Futures, InstrumentType.Option, InstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/mark-price", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXMarkPrice[]>(request, parameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXPositionTier[]>> GetPositionTiersAsync(
        InstrumentType instrumentType,
        MarginMode marginMode,
        string underlying,
        string? symbol = null,
        string? tier = null,
        string? asset = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(InstrumentType.Margin, InstrumentType.Futures, InstrumentType.Option, InstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddEnum("tdMode", marginMode);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("tier", tier);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/position-tiers", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXPositionTier[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/interest-rate-loan-quota", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKXInterestRate>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXVipInterestRate[]>> GetVIPInterestRatesAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/vip-interest-rate-loan-quota", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXVipInterestRate[]>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<string[]>> GetUnderlyingAsync(InstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(InstrumentType.Futures, InstrumentType.Option, InstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/underlying", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<string[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInsuranceFund>> GetInsuranceFundAsync(
        InstrumentType instrumentType,
        InsuranceType type = InsuranceType.All,
        string? underlying = null,
        string? asset = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(InstrumentType.Margin, InstrumentType.Swap, InstrumentType.Futures, InstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Margin, Swap, Futures or Option.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);

        if (type != InsuranceType.All)
            parameters.AddEnum("type", type);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/insurance-fund", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKXInsuranceFund>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXUnitConvert>> UnitConvertAsync(
        ConvertType type,
        string symbol,
        decimal quantity,
        ConvertUnit? unit = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("type", type);
        parameters.AddOptionalEnum("unit", unit);
        if (!string.IsNullOrEmpty(symbol)) parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("sz", quantity.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/public/convert-contract-coin", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendGetSingleAsync<OKXUnitConvert>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSupportCoins>> GetTradeStatsSupportedAssetsAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/trading-data/support-coin", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXSupportCoins>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTakerVolume[]>> GetTradeStatsTakerVolumeAsync(
        string asset,
        InstrumentType instrumentType,
        KlineInterval period = KlineInterval.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset}
        };
        parameters.AddEnum("instType", instrumentType);
        parameters.AddEnum("period", period);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/taker-volume", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXTakerVolume[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXRatio[]>> GetTradeStatsMarginLendingRatioAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/margin/loan-ratio", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXRatio[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXRatio[]>> GetTradeStatsLongShortRatioAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/contracts/long-short-account-ratio", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXRatio[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestVolume[]>> GetTradeStatsContractSummaryAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/contracts/open-interest-volume", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXInterestVolume[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestVolume[]>> GetTradeStatsOptionsSummaryAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXInterestVolume[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXPutCallRatio[]>> GetTradeStatsPutCallRatioAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume-ratio", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXPutCallRatio[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestVolumeExpiry[]>> GetTradeStatsInterestVolumeExpiryAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume-expiry", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXInterestVolumeExpiry[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInterestVolumeStrike[]>> GetTradeStatsInterestVolumeStrikeAsync(
        string asset,
        string expiryTime,
        KlineInterval period = KlineInterval.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
            { "expTime", expiryTime},
        };
        parameters.AddEnum("period", period);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/open-interest-volume-strike", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXInterestVolumeStrike[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTakerFlow>> GetTradeStatsTakerFlowAsync(
        string asset,
        KlineInterval period = KlineInterval.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy", asset},
        };
        parameters.AddEnum("period", period);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/rubik/stat/option/taker-block-volume", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        return await _baseClient.SendAsync<OKXTakerFlow>(request, parameters, ct).ConfigureAwait(false);
    }

    #region Get Announcements

    /// <inheritdoc />
    public async Task<WebCallResult<OKXAnnouncementsPage>> GetAnnouncementsAsync(string? announcementType = null, int? page = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("annType", announcementType);
        parameters.AddOptional("page", page);
        var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v5/support/announcements", OKXExchange.RateLimiter.EndpointGate, 1, _baseClient.AuthenticationProvider != null,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendGetSingleAsync<OKXAnnouncementsPage>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Get Announcement Types

    /// <inheritdoc />
    public async Task<WebCallResult<OKXAnnouncementType[]>> GetAnnouncementTypesAsync(CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v5/support/announcement-types", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXAnnouncementType[]>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Get Estimated Futures Settlement Price

    /// <inheritdoc />
    public async Task<WebCallResult<OKXSettlementPrice>> GetEstimatedFuturesSettlementPriceAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("instId", symbol);
        var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v5/public/estimated-settlement-info", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendGetSingleAsync<OKXSettlementPrice>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Get Estimated Futures Settlement Price

    /// <inheritdoc />
    public async Task<WebCallResult<OKXSettlementInfo[]>> GetSettlementHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("instId", symbol);
        parameters.AddOptionalMillisecondsString("after", startTime);
        parameters.AddOptionalMillisecondsString("before", endTime);
        parameters.AddOptional("limit", limit);
        var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v5/public/settlement-history", OKXExchange.RateLimiter.EndpointGate, 1, false,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
        var result = await _baseClient.SendAsync<OKXSettlementInfo[]>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

}
