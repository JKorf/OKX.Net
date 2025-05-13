using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quick margin type
/// </summary>
[JsonConverter(typeof(EnumConverter<IsolatedMarginMode>))]
public enum IsolatedMarginMode
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
