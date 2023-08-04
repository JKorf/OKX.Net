namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend request
/// </summary>
public class OKXOrderAmendRequest
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
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
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Cancel on fail
    /// </summary>
    [JsonProperty("cxlOnFail", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CancelOnFail { get; set; }

    /// <summary>
    /// New quantity
    /// </summary>
    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewQuantity { get; set; }

    /// <summary>
    /// New price
    /// </summary>
    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewPrice { get; set; }
}