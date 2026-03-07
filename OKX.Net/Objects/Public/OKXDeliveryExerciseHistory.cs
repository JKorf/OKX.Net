using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Delivery exercise history
/// </summary>
[SerializationModel]
public record OKXDeliveryExerciseHistory
{
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>details</c>"] Details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXPublicDeliveryExerciseHistoryDetail[] Details { get; set; } = Array.Empty<OKXPublicDeliveryExerciseHistoryDetail>();
}

/// <summary>
/// Delivery exercise history details
/// </summary>
[SerializationModel]
public record OKXPublicDeliveryExerciseHistoryDetail
{
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public DeliveryExerciseHistoryType Type { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }
}
