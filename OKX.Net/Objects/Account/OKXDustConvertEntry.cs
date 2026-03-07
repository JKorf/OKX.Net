using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Dust convert info
/// </summary>
[SerializationModel]
public record OKXDustConvertEntry
{
    /// <summary>
    /// ["<c>fillFromSz</c>"] Fill from quantity
    /// </summary>
    [JsonPropertyName("fillFromSz")]
    public decimal FillFromQuantity { get; set; }
    /// <summary>
    /// ["<c>fillToSz</c>"] Fill to quantity
    /// </summary>
    [JsonPropertyName("fillToSz")]
    public decimal FillToQuantity { get; set; }
    /// <summary>
    /// ["<c>fromCcy</c>"] From asset
    /// </summary>
    [JsonPropertyName("fromCcy")]
    public string FromAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>status</c>"] Status
    /// </summary>
    [JsonPropertyName("status")]
    public DustConvertStatus DustConvertStatus { get; set; }
    /// <summary>
    /// ["<c>acct</c>"] Account type
    /// </summary>
    [JsonPropertyName("acct")]
    public AccountType? AccountType { get; set; }
    /// <summary>
    /// ["<c>toCcy</c>"] To asset
    /// </summary>
    [JsonPropertyName("toCcy")]
    public string ToAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>uTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("uTime")]
    public DateTime UpdateTime { get; set; }
}


