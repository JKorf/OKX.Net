using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Dust convert info
/// </summary>
[SerializationModel]
public record OKXDustConvertEntry
{
    /// <summary>
    /// Fill from quantity
    /// </summary>
    [JsonPropertyName("fillFromSz")]
    public decimal FillFromQuantity { get; set; }
    /// <summary>
    /// Fill to quantity
    /// </summary>
    [JsonPropertyName("fillToSz")]
    public decimal FillToQuantity { get; set; }
    /// <summary>
    /// From asset
    /// </summary>
    [JsonPropertyName("fromCcy")]
    public string FromAsset { get; set; } = string.Empty;
    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    public DustConvertStatus DustConvertStatus { get; set; }
    /// <summary>
    /// Account type
    /// </summary>
    [JsonPropertyName("acct")]
    public AccountType? AccountType { get; set; }
    /// <summary>
    /// To asset
    /// </summary>
    [JsonPropertyName("toCcy")]
    public string ToAsset { get; set; } = string.Empty;
    /// <summary>
    /// Update time
    /// </summary>
    [JsonPropertyName("uTime")]
    public DateTime UpdateTime { get; set; }
}


