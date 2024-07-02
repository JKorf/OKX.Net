using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Margin amount
/// </summary>
public record OKXMarginAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string? Asset { get; set; }

    /// <summary>
    /// Real leverage after the margin adjustment
    /// </summary>
    [JsonPropertyName("leverage")]
    public string? Leverage { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Margin add reduce
    /// </summary>
    [JsonPropertyName("type"), JsonConverter(typeof(EnumConverter))]
    public MarginAddReduce? MarginAddReduce { get; set; }
}
