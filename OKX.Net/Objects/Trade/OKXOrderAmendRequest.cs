namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend request
/// </summary>
[SerializationModel]
public record OKXOrderAmendRequest
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Request id
    /// </summary>
    [JsonPropertyName("reqId")]
    public string RequestId { get; set; } = string.Empty;
    /// <summary>
    /// Deprecated, use SymbolCode parameter instead
    /// </summary>
    [JsonPropertyName("instId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Symbol { get; set; }
    /// <summary>
    /// Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long? SymbolCode { get; set; }

    /// <summary>
    /// Cancel on fail
    /// </summary>
    [JsonPropertyName("cxlOnFail"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? CancelOnFail { get; set; }

    /// <summary>
    /// New quantity
    /// </summary>
    [JsonPropertyName("newSz"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? NewQuantity { get; set; }

    /// <summary>
    /// New price
    /// </summary>
    [JsonPropertyName("newPx"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? NewPrice { get; set; }
}
