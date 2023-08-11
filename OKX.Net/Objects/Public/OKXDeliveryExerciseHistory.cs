using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Delivery exercise history
/// </summary>
public class OKXDeliveryExerciseHistory
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public IEnumerable<OKXPublicDeliveryExerciseHistoryDetail> Details { get; set; } = Array.Empty<OKXPublicDeliveryExerciseHistoryDetail>();
}

/// <summary>
/// Delivery exercise history details
/// </summary>
public class OKXPublicDeliveryExerciseHistoryDetail
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(DeliveryExerciseHistoryTypeConverter))]
    public OKXDeliveryExerciseHistoryType Type { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }
}
