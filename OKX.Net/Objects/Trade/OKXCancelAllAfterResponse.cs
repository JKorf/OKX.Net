namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel all after response
/// </summary>
[SerializationModel]
public record OKXCancelAllAfterResponse
{
    /// <summary>
    /// ["<c>triggerTime</c>"] Trigger time
    /// </summary>
    [JsonPropertyName("triggerTime")]
    public DateTime? TriggerTime { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
}
