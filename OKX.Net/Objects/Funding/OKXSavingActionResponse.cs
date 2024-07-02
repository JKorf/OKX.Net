using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Saving action info
/// </summary>
public record OKXSavingActionResponse
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? PurchaseRate { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter))]
    public SavingActionSide Side { get; set; }
}