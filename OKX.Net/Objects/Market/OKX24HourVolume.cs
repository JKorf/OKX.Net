using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Market;

/// <summary>
/// 24 hour volume
/// </summary>
[SerializationModel]
public record OKX24HourVolume
{
    /// <summary>
    /// Usd volume
    /// </summary>
    [JsonPropertyName("volUsd")]
    public decimal VolumeUsd { get; set; }

    /// <summary>
    /// Volume
    /// </summary>
    [JsonPropertyName("volCny")]
    public decimal VolumeCny { get; set; }

    /// <summary>
    /// Data timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
