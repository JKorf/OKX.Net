using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Maximum loan amount
/// </summary>
public record OKXMaximumLoanAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// Margin asset
    /// </summary>
    [JsonPropertyName("mgnCcy")]
    public string MarginAsset { get; set; } = string.Empty;

    /// <summary>
    /// Maximum loan
    /// </summary>
    [JsonPropertyName("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Order side
    /// </summary>
    [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide? OrderSide { get; set; }
}
