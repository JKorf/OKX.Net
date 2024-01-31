namespace OKX.Net.Objects.Market;

/// <summary>
/// Order book
/// </summary>
public class OKXOrderBook
{
    /// <summary>
    /// Symbol
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// List of asks
    /// </summary>
    [JsonProperty("asks")]
    public IEnumerable<OKXOrderBookRow> Asks { get; set; } = new List<OKXOrderBookRow>();

    /// <summary>
    /// List of bids
    /// </summary>
    [JsonProperty("bids")]
    public IEnumerable<OKXOrderBookRow> Bids { get; set; } = new List<OKXOrderBookRow>();

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Checksum
    /// </summary>
    [JsonProperty("checksum")]
    public long? Checksum { get; set; }

    /// <summary>
    /// Update sequence id
    /// </summary>
    [JsonProperty("seqId")]
    public long? SequenceId { get; set; }

    /// <summary>
    /// Previous sequence id
    /// </summary>
    [JsonProperty("prevSeqId")]
    public long? PreviousSequenceId { get; set; }
}
