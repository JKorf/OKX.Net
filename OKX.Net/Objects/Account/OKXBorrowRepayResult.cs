using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;
/// <summary>
/// Manual borrow/repay result
/// </summary>
[SerializationModel]
public record OKXBorrowRepayResult
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Side
    /// </summary>
    [JsonPropertyName("side")]
    public BorrowRepaySide BorrowRepaySide { get; set; }
    /// <summary>
    /// Actual quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }
}

