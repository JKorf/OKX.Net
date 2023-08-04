using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position info
/// </summary>
public class OKXPosition
{
    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// pTime
    /// </summary>
    [JsonProperty("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Position asset
    /// </summary>
    [JsonProperty("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// Position id
    /// </summary>
    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Trade id
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide PositionSide { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OKXMarginMode MarginMode { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Liabilities asset
    /// </summary>
    [JsonProperty("liabCcy")]
    public string LiabilitiesAsset { get; set; } = string.Empty;

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Option value
    /// </summary>
    [JsonProperty("optVal")]
    public decimal? OptionValue { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Auto deleveraging indicator
    /// </summary>
    [JsonProperty("adl")]
    public decimal? AutoDeleveragingIndicator { get; set; }

    /// <summary>
    /// Available position
    /// </summary>
    [JsonProperty("availPos")]
    public decimal? AvailablePositions { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Position quantity
    /// </summary>
    [JsonProperty("pos")]
    public decimal? PositionsQuantity { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio
    /// </summary>
    [JsonProperty("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// Notional usd
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Margin
    /// </summary>
    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    /// <summary>
    /// Last price
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Delta BS
    /// </summary>
    [JsonProperty("deltaBS")]
    public decimal? DeltaBS { get; set; }
    /// <summary>
    /// Delta PA
    /// </summary>
    [JsonProperty("deltaPA")]
    public decimal? DeltaPA { get; set; }
    /// <summary>
    /// Gamma BS
    /// </summary>
    [JsonProperty("gammaBS")]
    public decimal? GammaBS { get; set; }

    /// <summary>
    /// Gamma PA
    /// </summary>
    [JsonProperty("gammaPA")]
    public decimal? GammaPA { get; set; }

    /// <summary>
    /// Theta BS
    /// </summary>
    [JsonProperty("thetaBS")]
    public decimal? ThetaBS { get; set; }

    /// <summary>
    /// Theta PA
    /// </summary>
    [JsonProperty("thetaPA")]
    public decimal? ThetaPA { get; set; }
    /// <summary>
    /// Vega BS
    /// </summary>
    [JsonProperty("vegaBS")]
    public decimal? VegaBS { get; set; }
    /// <summary>
    /// Vega PA
    /// </summary>
    [JsonProperty("vegaPA")]
    public decimal? VegaPA { get; set; }
}
