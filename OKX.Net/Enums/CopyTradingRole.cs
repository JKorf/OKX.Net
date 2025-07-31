using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Copy trading role
/// </summary>
[JsonConverter(typeof(EnumConverter<CopyTradingRole>))]
public enum CopyTradingRole
{
    /// <summary>
    /// General user
    /// </summary>
    [Map("0")]
    GeneralUser,

    /// <summary>
    /// Leading trader
    /// </summary>
    [Map("1")]
    LeadingTrader,

    /// <summary>
    /// Copy trader
    /// </summary>
    [Map("2")]
    CopyTrader,
}
