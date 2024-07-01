using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Account type
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Funding account
    /// </summary>
    [Map("6")]
    Funding,
    /// <summary>
    /// Trading account
    /// </summary>
    [Map("18")]
    Trading,
}