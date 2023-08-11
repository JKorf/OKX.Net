using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Loan type
/// </summary>
public enum OKXLoanType
{
    /// <summary>
    /// VIP loans
    /// </summary>
    [Map("1")]
    VIPLoans,
    /// <summary>
    /// Market loans
    /// </summary>
    [Map("2")]
    MarketLoans
}
