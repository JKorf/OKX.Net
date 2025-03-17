using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Loan type
/// </summary>
[JsonConverter(typeof(EnumConverter<LoanType>))]
public enum LoanType
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
