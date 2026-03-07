using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Open interest
/// </summary>
[SerializationModel]
public record OKXOpenInterest
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
    /// ["<c>oi</c>"] Open interest
    /// </summary>
    [JsonPropertyName("oi")]
    public decimal? OpenInterest { get; set; }

    /// <summary>
    /// ["<c>oiCcy</c>"] Open interest asset
    /// </summary>
    [JsonPropertyName("oiCcy")]
    public decimal? OpenInterestAsset { get; set; }

    /// <summary>
    /// ["<c>oiUsd</c>"] Open interest in USD
    /// </summary>
    [JsonPropertyName("oiUsd")]
    public decimal? OpenInterestUsd { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
