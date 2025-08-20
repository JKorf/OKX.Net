using CryptoExchange.Net.RateLimiting.Guards;
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
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/copytrading/config", OKXExchange.RateLimiter.EndpointGate, 1, true,
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/copytrading/public-current-subpositions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.Default));
        return await _baseClient.SendAsync<OKXCurrentSubposition[]>(request, parameters, ct).ConfigureAwait(false);
    }
}
