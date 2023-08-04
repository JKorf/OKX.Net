using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quick margin type
/// </summary>
public enum OKXQuickMarginType
{
    /// <summary>
    /// Manual
    /// </summary>
    [Map("manual")]
    Manual,
    /// <summary>
    /// Auto borrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow,
    /// <summary>
    /// Auto repay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay
}
