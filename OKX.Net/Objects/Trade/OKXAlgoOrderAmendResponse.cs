namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
[SerializationModel]
public record OKXAlgoOrderAmendResponse
{
    /// <summary>
    /// ["<c>algoId</c>"] Order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
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
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>sMsg</c>"] Message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;
}
