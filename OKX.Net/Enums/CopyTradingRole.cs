using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Copy trading role
/// </summary>
[JsonConverter(typeof(EnumConverter<CopyTradingRole>))]
public enum CopyTradingRole
{
    /// <summary>
    /// ["<c>0</c>"] General user
    /// </summary>
    [Map("0")]
    GeneralUser,

    /// <summary>
    /// ["<c>1</c>"] Leading trader
    /// </summary>
    [Map("1")]
    LeadingTrader,

    /// <summary>
    /// ["<c>2</c>"] Copy trader
    /// </summary>
    [Map("2")]
    CopyTrader,
}
