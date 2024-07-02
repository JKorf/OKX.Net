using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Estimated price
/// </summary>
public record OKXEstimatedPrice
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Estimated price
    /// </summary>
    [JsonProperty("settlePx")]
    public decimal EstimatedPrice { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
