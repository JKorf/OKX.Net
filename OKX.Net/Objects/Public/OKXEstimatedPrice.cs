using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Estimated price
/// </summary>
[SerializationModel]
public record OKXEstimatedPrice
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>settlePx</c>"] Estimated price
    /// </summary>
    [JsonPropertyName("settlePx")]
    public decimal EstimatedPrice { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
