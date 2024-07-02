namespace OKX.Net.Objects.Market;

/// <summary>
/// Oracle
/// </summary>
public record OKXOracle
{
    /// <summary>
    /// Messages
    /// </summary>
    [JsonPropertyName("messages")]
    public IEnumerable<string> Messages { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Signatures
    /// </summary>
    [JsonPropertyName("signatures")]
    public IEnumerable<string> Signatures { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Prices
    /// </summary>
    [JsonPropertyName("prices")]
    public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();
}
