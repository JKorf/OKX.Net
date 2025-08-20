using OKX.Net.Objects.CopyTrading;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Copy trading endpoints
/// </summary>
public interface IOKXRestClientUnifiedApiCopyTrading
{
    /// <summary>
    /// Retrieve current account configuration related to copy/lead tradingt.
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-get-account-configuration" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCopyTradingAccount>> GetAccountConfigurationAsync(CancellationToken ct = default);

    /// <summary>
    /// Get current leading positions of lead trader.
    /// <para><a href="https://www.okx.com/docs-v5/zh/#order-book-trading-copy-trading-get-lead-trader-current-lead-positions" /></para>
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters</param>
    /// <param name="instType">Instrument type. SWAP, the default value.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested PositionId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested PositionId.</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCurrentSubposition[]>> GetLeadTraderCurrentLeadPositionsAsync(string uniqueCode, string instType = "SWAP", string? after = null, string? before = null, int limit = 100,  CancellationToken ct = default);
}
