using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quick margin type
/// </summary>
[JsonConverter(typeof(EnumConverter<IsolatedMarginMode>))]
public enum IsolatedMarginMode
{
    /// <summary>
    /// ["<c>automatic</c>"] Auto transfers
    /// </summary>
    [Map("automatic")]
    Automatic,
    /// <summary>
    /// ["<c>autonomy</c>"] Manual transfers (only supported in CONTRACTS)
    /// </summary>
    [Map("autonomy")]
    Autonomy,
    /// <summary>
    /// ["<c>quick_margin</c>"] Quick Margin Mode (only supported in MARGIN)
    /// </summary>
    [Map("quick_margin")]
    QuickMargin
}
