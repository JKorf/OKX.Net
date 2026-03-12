using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Status of optional trading
/// </summary>
[JsonConverter(typeof(EnumConverter<OptionalTradingStatus>))]
public enum OptionalTradingStatus
{
    /// <summary>
    /// ["<c>0</c>"] Not activated
    /// </summary>
    [Map("0")]
    NotActivated,
    /// <summary>
    /// ["<c>1</c>"] Activated
    /// </summary>
    [Map("1")]
    Activated
}
