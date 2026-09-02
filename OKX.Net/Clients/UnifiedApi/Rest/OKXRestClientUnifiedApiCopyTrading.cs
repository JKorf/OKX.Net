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
    public async Task<HttpResult<OKXCopyTradingAccount>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/copytrading/config", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCopyTradingAccount>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXCurrentSubposition[]>> GetLeadTraderCurrentLeadPositionsAsync(string uniqueCode, string instType = "SWAP", string? after = null, string? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (uniqueCode.Length != 16)
            throw new ArgumentException("uniqueCode all numbers and the length is 16 characters");
        if (limit < 1)
            limit = 1;
        if (limit > 100)
            limit = 100;

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instType);
        parameters.Add("uniqueCode", uniqueCode);
        parameters.Add("after", after);
        parameters.Add("before", before);
        parameters.Add("limit", limit);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/copytrading/public-current-subpositions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.Default));
        return await _baseClient.SendAsync<OKXCurrentSubposition[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXCurrentSubposition[]>> GetLeadPositionsAsync(string? symbol = null, InstrumentType? instrumentType = null, string? after = null, string? before = null, int limit = 500, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("after", after);
        parameters.Add("before", before);
        parameters.Add("limit", limit);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/copytrading/current-subpositions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXCurrentSubposition[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXSubpositionHistory[]>> GetLeadPositionHistoryAsync(string? symbol = null, InstrumentType? instrumentType = null, string? after = null, string? before = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("after", after);
        parameters.Add("before", before);
        parameters.Add("limit", limit);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/copytrading/subpositions-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubpositionHistory[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXCopyTradingActionResponse>> PlaceLeadStopOrderAsync(string subPositionId, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.AddParameter("subPosId", subPositionId);
        parameters.Add("tpTriggerPx", takeProfitTriggerPrice);
        parameters.Add("tpOrdPx", takeProfitOrderPrice);
        parameters.Add("slTriggerPx", stopLossTriggerPrice);
        parameters.Add("slOrdPx", stopLossOrderPrice);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/copytrading/algo-order", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCopyTradingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXCopyTradingActionResponse>> CloseLeadPositionAsync(string subPositionId, InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.AddParameter("subPosId", subPositionId);
        parameters.Add("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/copytrading/close-subposition", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXCopyTradingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXLeadingInstrument[]>> GetLeadingInstrumentsAsync(InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/copytrading/instruments", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXLeadingInstrument[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXLeadingInstrument[]>> AmendLeadingInstrumentsAsync(IEnumerable<string> symbols, InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.AddParameter("instId", string.Join(",", symbols));
        parameters.Add("instType", instrumentType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/copytrading/set-instruments", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXLeadingInstrument[]>(request, parameters, ct).ConfigureAwait(false);
    }
}
