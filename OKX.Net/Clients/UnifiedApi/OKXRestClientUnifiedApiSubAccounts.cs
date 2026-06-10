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
    public virtual async Task<HttpResult<OKXSubAccount[]>> GetSubAccountsAsync(
        bool? enable = null,
        string? subAccountName = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("enable", enable);
        parameters.Add("subAcct", subAccountName);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/users/subaccount/list", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountApiKey>> ResetSubAccountApiKeyAsync(
        string subAccountName,
        string apiKey,
        string? apiLabel = null,
        bool? readPermission = null,
        bool? tradePermission = null,
        string? ipAddresses = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            { "subAcct", subAccountName },
            { "apiKey", apiKey}
        };
        parameters.Add("label", apiLabel);
        parameters.Add("ip", ipAddresses);

        var permissions = new List<string>();
        if (readPermission.HasValue && readPermission.Value) permissions.Add("read_only");
        if (tradePermission.HasValue && tradePermission.Value) permissions.Add("trade");
        if (permissions.Count > 0)
            parameters.Add("perm", string.Join(",", permissions));

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/users/subaccount/modify-apikey", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountBalance>> GetSubAccountTradingBalancesAsync(
        string subAccountName,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/subaccount/balances", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountBalance>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountFundingBalance[]>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
        };

        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/subaccount/balances", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountFundingBalance[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountBill[]>> GetSubAccountBillsAsync(
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

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("subAcct", subAccountName);
        parameters.Add("ccy", asset);
        parameters.Add("type", type);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/subaccount/bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountTransfer>> TransferBetweenSubAccountsAsync(
        string asset,
        decimal amount,
        AccountType fromAccount,
        AccountType toAccount,
        string fromSubAccountName,
        string toSubAccountName,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"ccy", asset },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };
        parameters.Add("from", fromAccount);
        parameters.Add("to", toAccount);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/asset/subaccount/transfer", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccountTransfer>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountMaxWithdrawal[]>> GetSubAccountMaxWithdrawalsAsync(
        string subAccountName,
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
        };
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/subaccount/max-withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountMaxWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountBill[]>> GetManagedSubAccountBillsAsync(
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

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("subAcct", subAccountName);
        parameters.Add("ccy", asset);
        parameters.Add("type", type);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/subaccount/managed-subaccount-bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXEntrustSubAccount[]>> GetEntrustSubAccountsAsync(
        string? subAccountName = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("subAcct", subAccountName);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/users/entrust-subaccount-list", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXEntrustSubAccount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountApiKey[]>> GetSubAccountApiKeysAsync(
        string subAccountName,
        string? apiKey = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
        };
        parameters.Add("apiKey", apiKey);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/users/subaccount/apikey", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSubAccountApiKey[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountApiKey>> CreateSubAccountApiKeyAsync(
        string subAccountName,
        string apiLabel,
        string passphrase,
        bool readPermission = true,
        bool tradePermission = false,
        string? ipAddresses = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
            {"label", apiLabel },
            {"Passphrase", passphrase },
        };

        var permissions = new List<string>();
        if (readPermission) permissions.Add("read_only");
        if (tradePermission) permissions.Add("trade");
        if (permissions.Count > 0)
            parameters.AddParameter("perm", string.Join(",", permissions));

        parameters.Add("ip", ipAddresses);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/users/subaccount/apikey", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccountApiKey>> DeleteSubAccountApiKeyAsync(
        string subAccountName,
        string apiKey,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
            {"apiKey", apiKey },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/users/subaccount/delete-apikey", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccount>> SetSubAccountTransferOutAsync(
        string subAccountName,
        bool canTransferOut,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
            {"canTransOut", canTransferOut },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/users/subaccount/set-transfer-out", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccount>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSubAccount>> CreateSubAccountAsync(
        string subAccountName,
        string? label = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            {"subAcct", subAccountName },
        };
        parameters.Add("label", label);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/users/subaccount/create-subaccount", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSubAccount>(request, parameters, ct).ConfigureAwait(false);
    }
}
