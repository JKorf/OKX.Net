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
    [JsonProperty("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Open interest
    /// </summary>
    [JsonProperty("oi")]
    public decimal? OpenInterest { get; set; }

    /// <summary>
    /// Open interest asset
    /// </summary>
    [JsonProperty("oiCcy")]
    public decimal? OpenInterestAsset { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
