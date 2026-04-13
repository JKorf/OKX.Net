namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel request
/// </summary>
[SerializationModel]
public record OKXOrderCancelSocketRequest
{
    /// <summary>
    /// ["<c>instIdCode</c>"] Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode")]
    public long SymbolCode { get; set; }

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
