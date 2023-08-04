using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Close position response
/// </summary>
public class OKXClosePositionResponse
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide PositionSide { get; set; }
}
