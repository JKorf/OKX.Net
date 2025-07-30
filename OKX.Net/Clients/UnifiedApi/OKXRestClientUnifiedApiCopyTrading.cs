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
}
