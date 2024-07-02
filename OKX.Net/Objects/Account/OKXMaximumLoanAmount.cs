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
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// Margin asset
    /// </summary>
    [JsonProperty("mgnCcy")]
    public string MarginAsset { get; set; } = string.Empty;

    /// <summary>
    /// Maximum loan
    /// </summary>
    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide? OrderSide { get; set; }
}
