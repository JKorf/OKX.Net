using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quick margin type
/// </summary>
[JsonConverter(typeof(EnumConverter<QuickMarginType>))]
public enum QuickMarginType
{
    /// <summary>
    /// ["<c>manual</c>"] Manual
    /// </summary>
    [Map("manual")]
    Manual,
    /// <summary>
    /// ["<c>auto_borrow</c>"] Auto borrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow,
    /// <summary>
    /// ["<c>auto_repay</c>"] Auto repay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay
}
