namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order request
/// </summary>
[SerializationModel]
public record OKXAlgoOrderRequest
{
    /// <summary>
    /// ["<c>algoId</c>"] Algo order id
    /// </summary>
    [JsonPropertyName("algoId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client algo order id
    /// </summary>
    [JsonPropertyName("algoClOrdId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ClientAlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
}
