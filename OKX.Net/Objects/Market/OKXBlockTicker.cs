using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Block ticker
/// </summary>
[SerializationModel]
public record OKXBlockTicker
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Quote Volume
    /// </summary>
    [JsonPropertyName("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonPropertyName("volCcy24h")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
