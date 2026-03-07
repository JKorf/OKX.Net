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
    [JsonPropertyName("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
}
