using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Core;
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
    public virtual async Task<WebCallResult<IEnumerable<OKXSubAccount>>> GetSubAccountsAsync(
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/users/subaccount/list", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXSubAccount>>(request, parameters, ct).ConfigureAwait(false);
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

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/users/subaccount/modify-apikey", OKXExchange.RateLimiter.Public, 1, true);
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/subaccount/balances", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountBalance>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXSubAccountFundingBalance>>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/subaccount/balances", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXSubAccountFundingBalance>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXSubAccountBill>>> GetSubAccountBillsAsync(
        string? subAccountName = null,
        string? asset = null,
        OKXSubAccountTransferType? type = null,
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
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SubAccountTransferTypeConverter(false)));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/subaccount/bills", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXSubAccountBill>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccountTransfer>> TransferBetweenSubAccountsAsync(
        string asset,
        decimal amount,
        OKXAccount fromAccount,
        OKXAccount toAccount,
        string fromSubAccountName,
        string toSubAccountName,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"ccy", asset },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
            {"from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            {"to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/subaccount/transfer", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXSubAccountTransfer>(request, parameters, ct).ConfigureAwait(false);
    }
}
