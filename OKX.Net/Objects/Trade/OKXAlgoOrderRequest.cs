namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order request
/// </summary>
public class OKXAlgoOrderRequest
{
    /// <summary>
    /// Algo order id
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;
}
