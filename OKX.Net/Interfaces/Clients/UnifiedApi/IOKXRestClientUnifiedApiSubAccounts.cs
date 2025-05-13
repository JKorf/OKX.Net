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
    /// <para><a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-history-of-sub-account-transfer" /></para>
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="type">Transfer type</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountBill[]>> GetSubAccountBillsAsync(string? subAccountName = null, string? asset = null, SubAccountTransferType? type = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get sub-account funding balance
    /// Query detailed balance info of Funding Account of a sub-account via the master account (applies to master accounts only)
    /// <para><a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-funding-balance" /></para>
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="asset">Single asset or multiple assets (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountFundingBalance[]>> GetSubAccountFundingBalancesAsync(string subAccountName, string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// applies to master accounts only
    /// <para><a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-list" /></para>
    /// </summary>
    /// <param name="enable">Sub-account status，true: Normal ; false: Frozen</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccount[]>> GetSubAccountsAsync(bool? enable = null, string? subAccountName = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
    /// <para><a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-trading-balance" /></para>
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBalance>> GetSubAccountTradingBalancesAsync(string subAccountName, CancellationToken ct = default);

    /// <summary>
    /// applies to master accounts only
    /// <para><a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-reset-the-api-key-of-a-sub-account" /></para>
    /// </summary>
    /// <param name="readPermission">Read permission</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="apiKey">APIKey note</param>
    /// <param name="apiLabel">APIKey note</param>
    /// <param name="tradePermission">Trade permission</param>
    /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountApiKey>> ResetSubAccountApiKeyAsync(string subAccountName, string apiKey, string? apiLabel = null, bool? readPermission = null, bool? tradePermission = null, string? ipAddresses = null, CancellationToken ct = default);

    /// <summary>
    /// applies to master accounts only
    /// <para><a href="https://www.okx.com/docs-v5/en/#sub-account-rest-api-master-accounts-manage-the-transfers-between-sub-accounts" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="amount">Amount</param>
    /// <param name="fromAccount">From account type</param>
    /// <param name="toAccount">To account type</param>
    /// <param name="fromSubAccountName">Sub-account name of the account that transfers funds out.</param>
    /// <param name="toSubAccountName">Sub-account name of the account that transfers funds in.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubAccountTransfer>> TransferBetweenSubAccountsAsync(string asset, decimal amount, AccountType fromAccount, AccountType toAccount, string fromSubAccountName, string toSubAccountName, CancellationToken ct = default);
}
