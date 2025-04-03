using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position info
/// </summary>
[SerializationModel]
public record OKXPosition
{
    /// <summary>
    /// Create time
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// pTime
    /// </summary>
    [JsonPropertyName("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Position asset
    /// </summary>
    [JsonPropertyName("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Liabilities asset
    /// </summary>
    [JsonPropertyName("liabCcy")]
    public string LiabilitiesAsset { get; set; } = string.Empty;

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Option value
    /// </summary>
    [JsonPropertyName("optVal")]
    public decimal? OptionValue { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Auto deleveraging indicator
    /// </summary>
    [JsonPropertyName("adl")]
    public decimal? AutoDeleveragingIndicator { get; set; }

    /// <summary>
    /// Available position
    /// </summary>
    [JsonPropertyName("availPos")]
    public decimal? AvailablePositions { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal? PositionsQuantity { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio
    /// </summary>
    [JsonPropertyName("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// Notional usd
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Margin
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal? Margin { get; set; }

    /// <summary>
    /// Last price
    /// </summary>
    [JsonPropertyName("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonPropertyName("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonPropertyName("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Delta BS
    /// </summary>
    [JsonPropertyName("deltaBS")]
    public decimal? DeltaBS { get; set; }
    /// <summary>
    /// Delta PA
    /// </summary>
    [JsonPropertyName("deltaPA")]
    public decimal? DeltaPA { get; set; }
    /// <summary>
    /// Gamma BS
    /// </summary>
    [JsonPropertyName("gammaBS")]
    public decimal? GammaBS { get; set; }

    /// <summary>
    /// Gamma PA
    /// </summary>
    [JsonPropertyName("gammaPA")]
    public decimal? GammaPA { get; set; }

    /// <summary>
    /// Theta BS
    /// </summary>
    [JsonPropertyName("thetaBS")]
    public decimal? ThetaBS { get; set; }

    /// <summary>
    /// Theta PA
    /// </summary>
    [JsonPropertyName("thetaPA")]
    public decimal? ThetaPA { get; set; }
    /// <summary>
    /// Vega BS
    /// </summary>
    [JsonPropertyName("vegaBS")]
    public decimal? VegaBS { get; set; }
    /// <summary>
    /// Vega PA
    /// </summary>
    [JsonPropertyName("vegaPA")]
    public decimal? VegaPA { get; set; }

    /// <summary>
    /// Latest underlying index price
    /// </summary>
    [JsonPropertyName("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// Break-even price
    /// </summary>
    [JsonPropertyName("bePx")]
    public decimal? BreakEvenPrice { get; set; }

    /// <summary>
    /// Usd price
    /// </summary>
    [JsonPropertyName("usdPx")]
    public decimal? UsdPrice { get; set; }

    /// <summary>
    /// Latest mark price
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// DEPRECATED Base asset amount already borrowed
    /// </summary>
    [JsonPropertyName("baseBorrowed")]
    public decimal? BaseBorrowed { get; set; }

    /// <summary>
    /// DEPRECATED Base Interest, undeducted interest that has been incurred
    /// </summary>
    [JsonPropertyName("baseInterest")]
    public decimal? BaseInterest { get; set; }

    /// <summary>
    /// DEPRECATED Quote asset amount already borrowed
    /// </summary>
    [JsonPropertyName("quoteBorrowed")]
    public decimal? QuoteBorrowed { get; set; }
    /// <summary>
    /// DEPRECATED Quote Interest, undeducted interest that has been incurred
    /// </summary>
    [JsonPropertyName("quoteInterest")]
    public decimal? QuoteInterest { get; set; }
    /// <summary>
    /// Spot in use amount
    /// </summary>
    [JsonPropertyName("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }
    /// <summary>
    /// Spot in use asset
    /// </summary>
    [JsonPropertyName("spotInUseCcy")]
    public string? SpotInUseAsset { get; set; }
    /// <summary>
    /// External business id
    /// </summary>
    [JsonPropertyName("bizRefId")]
    public string? BusinessId { get; set; }
    /// <summary>
    /// External business type
    /// </summary>
    [JsonPropertyName("bizRefType")]
    public string? BusinessType { get; set; }
    /// <summary>
    /// Unrealized profit and loss calculated by last price. Main usage is showing, actual value is upl.
    /// </summary>
    [JsonPropertyName("uplLastPx")]
    public decimal? UnrealizedPnl { get; set; }
    /// <summary>
    /// Unrealized profit and loss ratio calculated by last price.
    /// </summary>
    [JsonPropertyName("uplRatioLastPx")]
    public decimal? UnrealizedPnlRatio { get; set; }
    /// <summary>
    /// DEPRECATED Base balance (Margin only)
    /// </summary>
    [JsonPropertyName("baseBal")]
    public decimal? BaseBalance { get; set; }
    /// <summary>
    /// DEPRECATED Quote balance (Margin only)
    /// </summary>
    [JsonPropertyName("quoteBal")]
    public decimal? QuoteBalance { get; set; }
    /// <summary>
    /// Accumulated fee. Negative number represents the user transaction fee charged by the platform. Positive number represents rebate.
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }
    /// <summary>
    /// Accumulated funding fee
    /// </summary>
    [JsonPropertyName("fundingFee")]
    public decimal? FundingFee { get; set; }
    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonPropertyName("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }
    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonPropertyName("realizedPnl")]
    public decimal? RealizedPnl { get; set; }
    /// <summary>
    /// Accumulated pnl of closing order(s)
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? Pnl { get; set; }
    /// <summary>
    /// The amount of close orders of isolated margin liability.
    /// </summary>
    [JsonPropertyName("pendingCloseOrdLiabVal")]
    public decimal? PendingCloseOrderLiability { get; set; }

    /// <summary>
    /// User-defined spot risk offset amount
    /// </summary>
    [JsonPropertyName("clSpotInUseAmt")]
    public decimal? ClSpotInUseAmount { get; set; }

    /// <summary>
    /// Max possible spot risk offset amount
    /// </summary>
    [JsonPropertyName("maxSpotInUseAmt")]
    public decimal? MaxSpotInUseAmount { get; set; }
    /// <summary>
    /// Close position algo orders attached to the position
    /// </summary>
    [JsonPropertyName("closeOrderAlgo")]
    public OKXPositionCloseOrder[] CloseOrderAlgo { get; set; } = Array.Empty<OKXPositionCloseOrder>();
}

/// <summary>
/// Position close order info
/// </summary>
[SerializationModel]
public record OKXPositionCloseOrder
{
    /// <summary>
    /// Algo id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string AlgoId { get; set; } = string.Empty;
    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx")]
    public decimal StopLossTriggerPrice { get; set; }
    /// <summary>
    /// Stop loss trigger price type
    /// </summary>
    [JsonPropertyName("slTriggerPxType")]
    public TriggerPriceType StopLossTriggerType { get; set; }
    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx")]
    public decimal TakeProfitTriggerPrice { get; set; }
    /// <summary>
    /// Take profit trigger price type
    /// </summary>
    [JsonPropertyName("tpTriggerPxType")]
    public TriggerPriceType TakeProfitTriggerType { get; set; }
    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered.
    /// </summary>
    [JsonPropertyName("closeFraction")]
    public decimal? CloseFraction { get; set; }
    /// <summary>
    /// The non-settlement entry price only reflects the average price at which the position is opened or increased.
    /// </summary>
    [JsonPropertyName("nonSettleAvgPx")]
    public decimal? NonSettlementEntryPrice { get; set; }
    /// <summary>
    /// Accumulated settled profit and loss (calculated by settlement price)
    /// </summary>
    [JsonPropertyName("settledPnl")]
    public decimal? SettledPnl { get; set; }
}
