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
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? PurchaseRate { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(EnumConverter))]
    public SavingActionSide Side { get; set; }
}