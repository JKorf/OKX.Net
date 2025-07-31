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
}
