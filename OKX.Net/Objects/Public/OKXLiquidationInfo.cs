using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Liquidation info
/// </summary>
[SerializationModel]
public record OKXLiquidationInfo
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>totalLoss</c>"] Total loss
    /// </summary>
    [JsonPropertyName("totalLoss")]
    public decimal? TotalLoss { get; set; }

    /// <summary>
    /// ["<c>uly</c>"] Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>details</c>"] Details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXPublicLiquidationInfoDetail[] Details { get; set; } = Array.Empty<OKXPublicLiquidationInfoDetail>();
}

/// <summary>
/// Liquidation info details
/// </summary>
[SerializationModel]
public record OKXPublicLiquidationInfoDetail
{
    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>bkPx</c>"] Bankruptcy price
    /// </summary>
    [JsonPropertyName("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Number of liquidations
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    /// <summary>
    /// ["<c>bkLoss</c>"] Number of losses
    /// </summary>
    [JsonPropertyName("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
