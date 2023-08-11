using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Status of optional trading
/// </summary>
public enum OKXOptionalTradingStatus
{
    /// <summary>
    /// Not activated
    /// </summary>
    [Map("0")]
    NotActivated,
    /// <summary>
    /// Activated
    /// </summary>
    [Map("1")]
    Activated
}
