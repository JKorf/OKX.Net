namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel request
/// </summary>
[SerializationModel]
public record OKXOrderCancelRequest
{
    /// <summary>
    /// ["<c>instId</c>"] Deprecated, use SymbolCode parameter instead
    /// </summary>
    [JsonPropertyName("instId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Symbol { get; set; }
    /// <summary>
    /// ["<c>instIdCode</c>"] Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long? SymbolCode { get; set; }

    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ClientOrderId { get; set; }
}
