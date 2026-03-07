namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order response
/// </summary>
[SerializationModel]
public record OKXAlgoOrderResponse
{
    /// <summary>
    /// ["<c>algoId</c>"] Algo order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Algo client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    public string? AgloClientOrderId { get; set; }

    /// <summary>
    /// ["<c>sCode</c>"] Code
    /// </summary>
    [JsonPropertyName("sCode")]
    public int Code { get; set; }

    /// <summary>
    /// ["<c>sMsg</c>"] Message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Whether order placement was successful
    /// </summary>
    public bool Success => Code == 0;
}
