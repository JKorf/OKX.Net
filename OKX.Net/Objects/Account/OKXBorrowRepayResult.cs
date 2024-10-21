using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;
/// <summary>
/// Manual borrow/repay result
/// </summary>
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

