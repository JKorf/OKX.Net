using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Saving action info
/// </summary>
[SerializationModel]
public record OKXSavingActionResponse
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>rate</c>"] Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? PurchaseRate { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public SavingActionSide Side { get; set; }
}
