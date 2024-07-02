using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Liquidation info
/// </summary>
public record OKXLiquidationInfo
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Total loss
    /// </summary>
    [JsonPropertyName("totalLoss")]
    public decimal? TotalLoss { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    [JsonPropertyName("details")]
    public IEnumerable<OKXPublicLiquidationInfoDetail> Details { get; set; } = Array.Empty<OKXPublicLiquidationInfoDetail>();
}

/// <summary>
/// Liquidation info details
/// </summary>
public record OKXPublicLiquidationInfoDetail
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Bankruptcy price
    /// </summary>
    [JsonPropertyName("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    /// <summary>
    /// Number of liquidations
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    /// <summary>
    /// Number of losses
    /// </summary>
    [JsonPropertyName("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}