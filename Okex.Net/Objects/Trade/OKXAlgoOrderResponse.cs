namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order response
/// </summary>
public class OKXAlgoOrderResponse
{
    /// <summary>
    /// Algo order id
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("sCode")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string Message { get; set; } = string.Empty;
}
