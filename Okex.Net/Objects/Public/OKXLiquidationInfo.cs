using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Liquidation info
/// </summary>
public class OKXLiquidationInfo
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Total loss
    /// </summary>
    [JsonProperty("totalLoss")]
    public decimal? TotalLoss { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public IEnumerable<OKXPublicLiquidationInfoDetail> Details { get; set; } = Array.Empty<OKXPublicLiquidationInfoDetail>();
}

/// <summary>
/// Liquidation info details
/// </summary>
public class OKXPublicLiquidationInfoDetail
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OKXOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide PositionSide { get; set; }

    /// <summary>
    /// Bankruptcy price
    /// </summary>
    [JsonProperty("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    /// <summary>
    /// Number of liquidations
    /// </summary>
    [JsonProperty("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    /// <summary>
    /// Number of losses
    /// </summary>
    [JsonProperty("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}