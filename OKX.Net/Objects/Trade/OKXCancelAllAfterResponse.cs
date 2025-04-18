using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel all after response
/// </summary>
[SerializationModel]
public record OKXCancelAllAfterResponse
{
    /// <summary>
    /// Trigger time
    /// </summary>
    [JsonPropertyName("triggerTime")]
    public DateTime? TriggerTime { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
}
