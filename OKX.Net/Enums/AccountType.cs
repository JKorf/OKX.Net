using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Account type
/// </summary>
[JsonConverter(typeof(EnumConverter<AccountType>))]
public enum AccountType
{
    /// <summary>
    /// ["<c>6</c>"] Funding account
    /// </summary>
    [Map("6")]
    Funding,
    /// <summary>
    /// ["<c>18</c>"] Trading account
    /// </summary>
    [Map("18")]
    Trading,
}
