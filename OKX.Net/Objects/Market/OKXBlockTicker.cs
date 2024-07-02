using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Block ticker
/// </summary>
public record OKXBlockTicker
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
    /// Quote Volume
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonProperty("volCcy24h")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
