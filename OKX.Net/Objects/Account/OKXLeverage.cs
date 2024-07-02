using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Leverage info
/// </summary>
public record OKXLeverage
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}
