﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Margin amount
/// </summary>
public record OKXMarginAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string? Asset { get; set; }

    /// <summary>
    /// Real leverage after the margin adjustment
    /// </summary>
    [JsonProperty("leverage")]
    public string? Leverage { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Margin add reduce
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
    public MarginAddReduce? MarginAddReduce { get; set; }
}
