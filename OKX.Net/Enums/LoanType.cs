using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Loan type
/// </summary>
[JsonConverter(typeof(EnumConverter<LoanType>))]
public enum LoanType
{
    /// <summary>
    /// ["<c>1</c>"] VIP loans
    /// </summary>
    [Map("1")]
    VIPLoans,
    /// <summary>
    /// ["<c>2</c>"] Market loans
    /// </summary>
    [Map("2")]
    MarketLoans
}
