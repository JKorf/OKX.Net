using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Borrow/repay entry
/// </summary>
[SerializationModel]
public record OKXBorrowRepayEntry
{
    /// <summary>
    /// ["<c>accBorrowed</c>"] Accumelated borrow quantity
    /// </summary>
    [JsonPropertyName("accBorrowed")]
    public decimal TotalBorrowQuantity { get; set; }
    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Borrow/repay type
    /// </summary>
    [JsonPropertyName("type")]
    public BorrowRepayType BorrowRepayType { get; set; }
}


