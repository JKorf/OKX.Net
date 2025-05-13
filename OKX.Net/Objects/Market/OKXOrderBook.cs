using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Market;

/// <summary>
/// Order book
/// </summary>
[SerializationModel]
public record OKXOrderBook
{
    /// <summary>
    /// Symbol
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// List of asks
    /// </summary>
    [JsonPropertyName("asks")]
    public OKXOrderBookRow[] Asks { get; set; } = [];

    /// <summary>
    /// List of bids
    /// </summary>
    [JsonPropertyName("bids")]
    public OKXOrderBookRow[] Bids { get; set; } = [];

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Checksum
    /// </summary>
    [JsonPropertyName("checksum")]
    public long? Checksum { get; set; }

    /// <summary>
    /// Update sequence id
    /// </summary>
    [JsonPropertyName("seqId")]
    public long? SequenceId { get; set; }

    /// <summary>
    /// Previous sequence id
    /// </summary>
    [JsonPropertyName("prevSeqId")]
    public long? PreviousSequenceId { get; set; }
}
