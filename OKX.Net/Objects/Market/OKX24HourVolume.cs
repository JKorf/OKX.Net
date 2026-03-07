namespace OKX.Net.Objects.Market;

/// <summary>
/// 24 hour volume
/// </summary>
[SerializationModel]
public record OKX24HourVolume
{
    /// <summary>
    /// ["<c>volUsd</c>"] Usd volume
    /// </summary>
    [JsonPropertyName("volUsd")]
    public decimal VolumeUsd { get; set; }

    /// <summary>
    /// ["<c>volCny</c>"] Volume
    /// </summary>
    [JsonPropertyName("volCny")]
    public decimal VolumeCny { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Data timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
