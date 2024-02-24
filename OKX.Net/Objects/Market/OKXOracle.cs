using OKX.Net.Attributes;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Oracle
/// </summary>
[JsonConverter(typeof(TypedDataConverter<OKXOracle>))]
public class OKXOracle
{
    /// <summary>
    /// Messages
    /// </summary>
    [JsonProperty("messages")]
    public IEnumerable<string> Messages { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Signatures
    /// </summary>
    [JsonProperty("signatures")]
    public IEnumerable<string> Signatures { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Prices
    /// </summary>
    [TypedData]
    // [JsonProperty("prices")]
    public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();
}
