using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Block ticker
/// </summary>
[SerializationModel]
public record OKXBlockTicker
{
    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>vol24h</c>"] Quote Volume
    /// </summary>
    [JsonPropertyName("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// ["<c>volCcy24h</c>"] Base Volume
    /// </summary>
    [JsonPropertyName("volCcy24h")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
