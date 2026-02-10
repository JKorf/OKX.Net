namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel request
/// </summary>
[SerializationModel]
public record OKXOrderCancelRequest
{
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
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ClientOrderId { get; set; }
}
