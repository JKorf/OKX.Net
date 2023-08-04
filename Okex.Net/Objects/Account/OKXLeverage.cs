using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Leverage info
/// </summary>
public class OKXLeverage
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OKXMarginMode MarginMode { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide PositionSide { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}
