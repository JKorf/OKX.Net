using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Borrow/repay entry
/// </summary>
[SerializationModel]
public record OKXBorrowRepayEntry
{
    /// <summary>
    /// Accumelated borrow quantity
    /// </summary>
    [JsonPropertyName("accBorrowed")]
    public decimal TotalBorrowQuantity { get; set; }
    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// Borrow/repay type
    /// </summary>
    [JsonPropertyName("type")]
    public BorrowRepayType BorrowRepayType { get; set; }
}


