namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
public record OKXOrderAmendResponse
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Request id
    /// </summary>
    [JsonProperty("reqId")]
    public string RequestId { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("sCode")]
    public int Code { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Whether order edit was successful
    /// </summary>
    public bool Success => Code == 0;
}
