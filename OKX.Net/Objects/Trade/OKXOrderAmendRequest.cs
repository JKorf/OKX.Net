namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend request
/// </summary>
[SerializationModel]
public record OKXOrderAmendRequest
{
    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>reqId</c>"] Request id
    /// </summary>
    [JsonPropertyName("reqId")]
    public string RequestId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>instIdCode</c>"] Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode")]
    public long SymbolCode { get; set; }

    /// <summary>
    /// ["<c>cxlOnFail</c>"] Cancel on fail
    /// </summary>
    [JsonPropertyName("cxlOnFail"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? CancelOnFail { get; set; }

    /// <summary>
    /// ["<c>newSz</c>"] New quantity
    /// </summary>
    [JsonPropertyName("newSz"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? NewQuantity { get; set; }

    /// <summary>
    /// ["<c>newPx</c>"] New price
    /// </summary>
    [JsonPropertyName("newPx"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? NewPrice { get; set; }
}
