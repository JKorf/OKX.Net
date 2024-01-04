namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
public class OKXAlgoOrderAmendResponse
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("algoId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonProperty("algoClOrdId")]
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
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string Message { get; set; } = string.Empty;
}
