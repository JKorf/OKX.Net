using CryptoExchange.Net.RateLimiting.Guards;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.CopyTrading;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiCopyTrading : IOKXRestClientUnifiedApiCopyTrading
{
    private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
    private readonly OKXRestClientUnifiedApi _baseClient;

    internal OKXRestClientUnifiedApiCopyTrading(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXCopyTradingAccount>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v5/copytrading/config", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCopyTradingAccount>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXCurrentSubposition[]>> GetLeadTraderCurrentLeadPositionsAsync(string uniqueCode, string instType = "SWAP", string? after = null, string? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (uniqueCode.Length != 16)
            throw new ArgumentException("uniqueCode all numbers and the length is 16 characters");
        if (limit < 1)
            limit = 1;
        if (limit > 100)
            limit = 100;

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("instType", instType);
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("after", after);
        parameters.AddOptionalParameter("before", before);
        parameters.AddOptionalParameter("limit", limit);

        var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v5/copytrading/public-current-subpositions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.Default));
        return await _baseClient.SendAsync<OKXCurrentSubposition[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXCurrentSubposition[]>> GetLeadPositionsAsync(string? symbol = null, InstrumentType? instrumentType = null, string? after = null, string? before = null, int limit = 500, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("after", after);
        parameters.AddOptionalParameter("before", before);
        parameters.AddOptionalParameter("limit", limit);

        var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v5/copytrading/current-subpositions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXCurrentSubposition[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXSubpositionHistory[]>> GetLeadPositionHistoryAsync(string? symbol = null, InstrumentType? instrumentType = null, string? after = null, string? before = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("after", after);
        parameters.AddOptionalParameter("before", before);
        parameters.AddOptionalParameter("limit", limit);

        var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v5/copytrading/subpositions-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubpositionHistory[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXCopyTradingActionResponse>> PlaceLeadStopOrderAsync(string subPositionId, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("subPosId", subPositionId);
        parameters.AddOptionalParameter("tpTriggerPx", takeProfitTriggerPrice);
        parameters.AddOptionalParameter("tpOrdPx", takeProfitOrderPrice);
        parameters.AddOptionalParameter("slTriggerPx", stopLossTriggerPrice);
        parameters.AddOptionalParameter("slOrdPx", stopLossOrderPrice);

        var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v5/copytrading/algo-order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCopyTradingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXCopyTradingActionResponse>> CloseLeadPositionAsync(string subPositionId, InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("subPosId", subPositionId);
        parameters.AddOptionalEnum("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v5/copytrading/close-subposition", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCopyTradingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXLeadingInstrument[]>> GetLeadingInstrumentsAsync(InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v5/copytrading/instruments", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXLeadingInstrument[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXLeadingInstrument[]>> AmendLeadingInstrumentsAsync(IEnumerable<string> symbols, InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("instId", string.Join(",", symbols));
        parameters.AddOptionalEnum("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v5/copytrading/set-instruments", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXLeadingInstrument[]>(request, parameters, ct).ConfigureAwait(false);
    }
}
