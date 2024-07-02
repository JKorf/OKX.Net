using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Open interest
/// </summary>
public record OKXOpenInterest
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Open interest
    /// </summary>
    [JsonPropertyName("oi")]
    public decimal? OpenInterest { get; set; }

    /// <summary>
    /// Open interest asset
    /// </summary>
    [JsonPropertyName("oiCcy")]
    public decimal? OpenInterestAsset { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
