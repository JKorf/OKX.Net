using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Maximum loan amount
/// </summary>
[SerializationModel]
public record OKXMaximumLoanAmount
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// ["<c>mgnCcy</c>"] Margin asset
    /// </summary>
    [JsonPropertyName("mgnCcy")]
    public string MarginAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>maxLoan</c>"] Maximum loan
    /// </summary>
    [JsonPropertyName("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide? OrderSide { get; set; }
}
