using CryptoExchange.Net.RateLimiting.Guards;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.SubAccount;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiSubAccounts : IOKXRestClientUnifiedApiSubAccounts
{
    private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
    private readonly OKXRestClientUnifiedApi _baseClient;

    internal OKXRestClientUnifiedApiSubAccounts(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccount[]>> GetSubAccountsAsync(
        bool? enable = null,
        string? subAccountName = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("enable", enable);
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/users/subaccount/list", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccountApiKey>> ResetSubAccountApiKeyAsync(
        string subAccountName,
        string apiKey,
        string? apiLabel = null,
        bool? readPermission = null,
        bool? tradePermission = null,
        string? ipAddresses = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "subAcct", subAccountName },
            { "apiKey", apiKey}
        };
        parameters.AddOptionalParameter("label", apiLabel);
        parameters.AddOptionalParameter("ip", ipAddresses);

        var permissions = new List<string>();
        if (readPermission.HasValue && readPermission.Value) permissions.Add("read_only");
        if (tradePermission.HasValue && tradePermission.Value) permissions.Add("trade");
        if (permissions.Count > 0)
            parameters.AddOptionalParameter("perm", string.Join(",", permissions));

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/users/subaccount/modify-apikey", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountBalance>> GetSubAccountTradingBalancesAsync(
        string subAccountName,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"subAcct", subAccountName },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/subaccount/balances", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountBalance>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccountFundingBalance[]>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/subaccount/balances", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountFundingBalance[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccountBill[]>> GetSubAccountBillsAsync(
        string? subAccountName = null,
        string? asset = null,
        SubAccountTransferType? type = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/subaccount/bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccountTransfer>> TransferBetweenSubAccountsAsync(
        string asset,
        decimal amount,
        AccountType fromAccount,
        AccountType toAccount,
        string fromSubAccountName,
        string toSubAccountName,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"ccy", asset },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };
        parameters.AddEnum("from", fromAccount);
        parameters.AddEnum("to", toAccount);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/subaccount/transfer", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccountTransfer>(request, parameters, ct).ConfigureAwait(false);
    }
}
