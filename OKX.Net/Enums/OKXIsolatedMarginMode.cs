using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quick margin type
/// </summary>
public enum OKXIsolatedMarginMode
{
    /// <summary>
    /// Auto transfers
    /// </summary>
    [Map("automatic")]
    Automatic,
    /// <summary>
    /// Manual transfers (only supported in CONTRACTS)
    /// </summary>
    [Map("autonomy")]
    Autonomy,
    /// <summary>
    /// Quick Margin Mode (only supported in MARGIN)
    /// </summary>
    [Map("quick_margin")]
    QuickMargin
}
