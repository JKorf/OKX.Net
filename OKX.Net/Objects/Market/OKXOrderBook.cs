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
    /// ["<c>asks</c>"] List of asks
    /// </summary>
    [JsonPropertyName("asks")]
    public OKXOrderBookRow[] Asks { get; set; } = [];

    /// <summary>
    /// ["<c>bids</c>"] List of bids
    /// </summary>
    [JsonPropertyName("bids")]
    public OKXOrderBookRow[] Bids { get; set; } = [];

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>action</c>"] Action
    /// </summary>
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>checksum</c>"] Checksum
    /// </summary>
    [JsonPropertyName("checksum")]
    public long? Checksum { get; set; }

    /// <summary>
    /// ["<c>seqId</c>"] Update sequence id
    /// </summary>
    [JsonPropertyName("seqId")]
    public long? SequenceId { get; set; }

    /// <summary>
    /// ["<c>prevSeqId</c>"] Previous sequence id
    /// </summary>
    [JsonPropertyName("prevSeqId")]
    public long? PreviousSequenceId { get; set; }
}
