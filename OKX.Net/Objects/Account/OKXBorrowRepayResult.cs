using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;
/// <summary>
/// Manual borrow/repay result
/// </summary>
[SerializationModel]
public record OKXBorrowRepayResult
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public BorrowRepaySide BorrowRepaySide { get; set; }
    /// <summary>
    /// ["<c>amt</c>"] Actual quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }
}

