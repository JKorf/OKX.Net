namespace OKX.Net.Objects.Market;

/// <summary>
/// 24 hour volume
/// </summary>
public class OKX24HourVolume
{
    /// <summary>
    /// Usd volume
    /// </summary>
    [JsonProperty("volUsd")]
    public decimal VolumeUsd { get; set; }

    /// <summary>
    /// Volume
    /// </summary>
    [JsonProperty("volCny")]
    public decimal VolumeCny { get; set; }

    /// <summary>
    /// Data timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
