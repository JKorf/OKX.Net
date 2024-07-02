namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order request
/// </summary>
public record OKXAlgoOrderRequest
{
    /// <summary>
    /// Algo order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
}
