using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.SubAccount;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiSubAccounts : IOKXRestClientUnifiedApiSubAccounts
{
    private readonly OKXRestClientUnifiedApi _baseClient;

    #region Sub-Account Endpoints
    private const string Endpoints_V5_SubAccount_List = "api/v5/users/subaccount/list";
    private const string Endpoints_V5_SubAccount_ResetApiKey = "api/v5/users/subaccount/modify-apikey";
    private const string Endpoints_V5_SubAccount_TradingBalances = "api/v5/account/subaccount/balances";
    private const string Endpoints_V5_SubAccount_FundingBalances = "api/v5/asset/subaccount/balances";
    private const string Endpoints_V5_SubAccount_Bills = "api/v5/asset/subaccount/bills";
    private const string Endpoints_V5_SubAccount_Transfer = "api/v5/asset/subaccount/transfer";
    #endregion

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

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("enable", enable);
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSubAccount>>>(_baseClient.GetUri(Endpoints_V5_SubAccount_List), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXSubAccount>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXSubAccount>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
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
        var parameters = new Dictionary<string, object>
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

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSubAccountApiKey>>>(_baseClient.GetUri(Endpoints_V5_SubAccount_ResetApiKey), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXSubAccountApiKey>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXSubAccountApiKey>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSubAccountTradingBalance>> GetSubAccountTradingBalancesAsync(
        string subAccountName,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct", subAccountName },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSubAccountTradingBalance>>>(_baseClient.GetUri(Endpoints_V5_SubAccount_TradingBalances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXSubAccountTradingBalance>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXSubAccountTradingBalance>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXSubAccountFundingBalance>>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", asset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSubAccountFundingBalance>>>(_baseClient.GetUri(Endpoints_V5_SubAccount_FundingBalances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXSubAccountFundingBalance>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXSubAccountFundingBalance>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
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

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SubAccountTransferTypeConverter(false)));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));


        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSubAccountBill>>>(_baseClient.GetUri(Endpoints_V5_SubAccount_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXSubAccountBill>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXSubAccountBill>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
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
        var parameters = new Dictionary<string, object>
        {
            {"ccy", asset },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
            {"from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            {"to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSubAccountTransfer>>>(_baseClient.GetUri(Endpoints_V5_SubAccount_Transfer), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXSubAccountTransfer>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXSubAccountTransfer>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }
}
