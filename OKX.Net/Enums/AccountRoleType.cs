using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Role type
/// </summary>
[JsonConverter(typeof(EnumConverter<AccountRoleType>))]
public enum AccountRoleType
{
    /// <summary>
    /// ["<c>0</c>"] Normal user
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
    CopyTrader
}
