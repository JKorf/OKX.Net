namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
[SerializationModel]
public record OKXOrderAmendResponse
{
    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>reqId</c>"] Request id
    /// </summary>
    [JsonPropertyName("reqId")]
    public string RequestId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>sCode</c>"] Code
    /// </summary>
    [JsonPropertyName("sCode")]
    public int Code { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// ["<c>sMsg</c>"] Message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Whether order edit was successful
    /// </summary>
    public bool Success => Code == 0;
}
