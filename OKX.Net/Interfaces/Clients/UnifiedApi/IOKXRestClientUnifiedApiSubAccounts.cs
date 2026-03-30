using OKX.Net.Enums;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.SubAccount;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API subaccount endpoints
/// </summary>
public interface IOKXRestClientUnifiedApiSubAccounts
{
    /// <summary>
    /// applies to master accounts only
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-history-of-sub-account-transfer" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/subaccount/bills
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="type">["<c>type</c>"] Transfer type</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountBill[]>> GetSubAccountBillsAsync(string? subAccountName = null, string? asset = null, SubAccountTransferType? type = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get sub-account funding balance
    /// Query detailed balance info of Funding Account of a sub-account via the master account (applies to master accounts only)
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-funding-balance" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/subaccount/balances
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="asset">["<c>ccy</c>"] Single asset or multiple assets (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountFundingBalance[]>> GetSubAccountFundingBalancesAsync(string subAccountName, string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// applies to master accounts only
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-list" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/users/subaccount/list
    /// </para>
    /// </summary>
    /// <param name="enable">["<c>enable</c>"] Sub-account status，true: Normal ; false: Frozen</param>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccount[]>> GetSubAccountsAsync(bool? enable = null, string? subAccountName = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-trading-balance" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/subaccount/balances
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBalance>> GetSubAccountTradingBalancesAsync(string subAccountName, CancellationToken ct = default);

    /// <summary>
    /// applies to master accounts only
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-reset-the-api-key-of-a-sub-account" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/users/subaccount/modify-apikey
    /// </para>
    /// </summary>
    /// <param name="readPermission">["<c>perm</c>"] Read permission</param>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="apiKey">["<c>apiKey</c>"] APIKey note</param>
    /// <param name="apiLabel">["<c>label</c>"] APIKey note</param>
    /// <param name="tradePermission">["<c>perm</c>"] Trade permission</param>
    /// <param name="ipAddresses">["<c>ip</c>"] Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountApiKey>> ResetSubAccountApiKeyAsync(string subAccountName, string apiKey, string? apiLabel = null, bool? readPermission = null, bool? tradePermission = null, string? ipAddresses = null, CancellationToken ct = default);

    /// <summary>
    /// applies to master accounts only
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-master-accounts-manage-the-transfers-between-sub-accounts" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/asset/subaccount/transfer
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="amount">["<c>amt</c>"] Amount</param>
    /// <param name="fromAccount">["<c>from</c>"] From account type</param>
    /// <param name="toAccount">["<c>to</c>"] To account type</param>
    /// <param name="fromSubAccountName">["<c>fromSubAccount</c>"] Sub-account name of the account that transfers funds out.</param>
    /// <param name="toSubAccountName">["<c>toSubAccount</c>"] Sub-account name of the account that transfers funds in.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountTransfer>> TransferBetweenSubAccountsAsync(string asset, decimal amount, AccountType fromAccount, AccountType toAccount, string fromSubAccountName, string toSubAccountName, CancellationToken ct = default);

    /// <summary>
    /// Find the maximum withdrawal amount for a sub-account.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-maximum-withdrawals" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/subaccount/max-withdrawal
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountMaxWithdrawal[]>> GetSubAccountMaxWithdrawalsAsync(string subAccountName, string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the transfer history of managed sub-accounts.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-history-of-managed-sub-account-transfer" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/subaccount/managed-subaccount-bills
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="type">["<c>type</c>"] Transfer type</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountBill[]>> GetManagedSubAccountBillsAsync(string? subAccountName = null, string? asset = null, SubAccountTransferType? type = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get entrust sub-account list
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-custody-trading-sub-account-list" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/users/entrust-subaccount-list
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXEntrustSubAccount[]>> GetEntrustSubAccountsAsync(string? subAccountName = null, CancellationToken ct = default);

    /// <summary>
    /// Get sub-account API keys
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-query-the-api-key-of-a-sub-account" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/users/subaccount/apikey
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="apiKey">["<c>apiKey</c>"] API key to query</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountApiKey[]>> GetSubAccountApiKeysAsync(string subAccountName, string? apiKey = null, CancellationToken ct = default);

    /// <summary>
    /// Create an API Key for a sub-account
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-create-an-api-key-for-a-sub-account" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/users/subaccount/apikey
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="apiLabel">["<c>label</c>"] API Key Label</param>
    /// <param name="passphrase">["<c>Passphrase</c>"] Passphrase</param>
    /// <param name="readPermission">["<c>perm</c>"] Read permission</param>
    /// <param name="tradePermission">["<c>perm</c>"] Trade permission</param>
    /// <param name="ipAddresses">["<c>ip</c>"] IP Addresses</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountApiKey>> CreateSubAccountApiKeyAsync(string subAccountName, string apiLabel, string passphrase, bool readPermission = true, bool tradePermission = false, string? ipAddresses = null, CancellationToken ct = default);

    /// <summary>
    /// Delete a sub-account API Key
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-delete-the-api-key-of-sub-accounts" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/users/subaccount/delete-apikey
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="apiKey">["<c>apiKey</c>"] API Key</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountApiKey>> DeleteSubAccountApiKeyAsync(string subAccountName, string apiKey, CancellationToken ct = default);

    /// <summary>
    /// Set sub-account transfer out permissions
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-set-permission-of-transfer-out" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/users/subaccount/set-transfer-out
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="canTransferOut">["<c>canTransOut</c>"] Can transfer out</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccount>> SetSubAccountTransferOutAsync(string subAccountName, bool canTransferOut, CancellationToken ct = default);

    /// <summary>
    /// Create a new sub-account
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-create-sub-account" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/users/subaccount/create-subaccount
    /// </para>
    /// </summary>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="label">["<c>label</c>"] Label</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccount>> CreateSubAccountAsync(string subAccountName, string? label = null, CancellationToken ct = default);
}

