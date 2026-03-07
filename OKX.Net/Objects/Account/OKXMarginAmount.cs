using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Margin amount
/// </summary>
[SerializationModel]
public record OKXMarginAmount
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string? Asset { get; set; }

    /// <summary>
    /// ["<c>leverage</c>"] Real leverage after the margin adjustment
    /// </summary>
    [JsonPropertyName("leverage")]
    public string? Leverage { get; set; }

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Margin add reduce
    /// </summary>
    [JsonPropertyName("type")]
    public MarginAddReduce? MarginAddReduce { get; set; }
}
