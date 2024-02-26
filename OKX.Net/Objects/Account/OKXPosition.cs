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

    /// <summary>
    /// Latest underlying index price
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// Break-even price
    /// </summary>
    [JsonProperty("bePx")]
    public decimal? BreakEvenPrice { get; set; }

    /// <summary>
    /// Usd price
    /// </summary>
    [JsonProperty("usdPx")]
    public decimal? UsdPrice { get; set; }

    /// <summary>
    /// Latest mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Base asset amount already borrowed
    /// </summary>
    [JsonProperty("baseBorrowed")]
    public decimal? BaseBorrowed { get; set; }

    /// <summary>
    /// Base Interest, undeducted interest that has been incurred
    /// </summary>
    [JsonProperty("baseInterest")]
    public decimal? BaseInterest { get; set; }

    /// <summary>
    /// Quote asset amount already borrowed
    /// </summary>
    [JsonProperty("quoteBorrowed")]
    public decimal? QuoteBorrowed { get; set; }
    /// <summary>
    /// Quote Interest, undeducted interest that has been incurred
    /// </summary>
    [JsonProperty("quoteInterest")]
    public decimal? QuoteInterest { get; set; }
    /// <summary>
    /// Spot in use amount
    /// </summary>
    [JsonProperty("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }
    /// <summary>
    /// Spot in use asset
    /// </summary>
    [JsonProperty("spotInUseCcy")]
    public string? SpotInUseAsset { get; set; }
    /// <summary>
    /// External business id
    /// </summary>
    [JsonProperty("bizRefId")]
    public string? BusinessId { get; set; }
    /// <summary>
    /// External business type
    /// </summary>
    [JsonProperty("bizRefType")]
    public string? BusinessType { get; set; }
    /// <summary>
    /// Unrealized profit and loss calculated by last price. Main usage is showing, actual value is upl.
    /// </summary>
    [JsonProperty("uplLastPx")]
    public decimal? UnrealizedPnl { get; set; }
    /// <summary>
    /// Unrealized profit and loss ratio calculated by last price.
    /// </summary>
    [JsonProperty("uplRatioLastPx")]
    public decimal? UnrealizedPnlRatio { get; set; }
    /// <summary>
    /// Base balance (Margin only)
    /// </summary>
    [JsonProperty("baseBal")]
    public decimal? BaseBalance { get; set; }
    /// <summary>
    /// Quote balance (Margin only)
    /// </summary>
    [JsonProperty("quoteBal")]
    public decimal? QuoteBalance { get; set; }
    /// <summary>
    /// Accumulated fee. Negative number represents the user transaction fee charged by the platform. Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }
    /// <summary>
    /// Accumulated funding fee
    /// </summary>
    [JsonProperty("fundingFee")]
    public decimal? FundingFee { get; set; }
    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonProperty("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }
    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonProperty("realizedPnl")]
    public decimal? RealizedPnl { get; set; }
    /// <summary>
    /// Accumulated pnl of closing order(s)
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? Pnl { get; set; }
    /// <summary>
    /// Close position algo orders attached to the position
    /// </summary>
    [JsonProperty("closeOrderAlgo")]
    public IEnumerable<OKXPositionCloseOrder> CloseOrderAlgo { get; set; } = Array.Empty<OKXPositionCloseOrder>();
}

/// <summary>
/// Position close order info
/// </summary>
public class OKXPositionCloseOrder
{
    /// <summary>
    /// Algo id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoId { get; set; } = string.Empty;
    /// <summary>
    /// Stop losse trigger price
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal StopLossTriggerPrice { get; set; }
    /// <summary>
    /// Stop loss trigger price type
    /// </summary>
    [JsonProperty("slTriggerPxType"), JsonConverter(typeof(EnumConverter))]
    public OXKTriggerPriceType StopLossTriggerType { get; set; }
    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal TakeProfitTriggerPrice { get; set; }
    /// <summary>
    /// Take profit trigger price type
    /// </summary>
    [JsonProperty("tpTriggerPxType"), JsonConverter(typeof(EnumConverter))]
    public OXKTriggerPriceType TakeProfitTriggerType { get; set; }
    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered.
    /// </summary>
    [JsonProperty("closeFraction")]
    public decimal? CloseFraction { get; set; }
}
