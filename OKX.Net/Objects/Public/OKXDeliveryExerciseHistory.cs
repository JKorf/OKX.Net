using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Delivery exercise history
/// </summary>
[SerializationModel]
public record OKXDeliveryExerciseHistory
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Details
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
    /// Type
    /// </summary>
    [JsonPropertyName("type")]
    public DeliveryExerciseHistoryType Type { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }
}
