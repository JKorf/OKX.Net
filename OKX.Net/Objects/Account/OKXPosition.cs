using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position info
/// </summary>
[SerializationModel]
public record OKXPosition
{
    /// <summary>
    /// ["<c>cTime</c>"] Create time
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// ["<c>uTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// ["<c>pTime</c>"] pTime
    /// </summary>
    [JsonPropertyName("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>posCcy</c>"] Position asset
    /// </summary>
    [JsonPropertyName("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>posId</c>"] Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// ["<c>tradeId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// ["<c>liab</c>"] Liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// ["<c>liabCcy</c>"] Liabilities asset
    /// </summary>
    [JsonPropertyName("liabCcy")]
    public string LiabilitiesAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>imr</c>"] Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>optVal</c>"] Option value
    /// </summary>
    [JsonPropertyName("optVal")]
    public decimal? OptionValue { get; set; }

    /// <summary>
    /// ["<c>upl</c>"] Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>adl</c>"] Auto deleveraging indicator
    /// </summary>
    [JsonPropertyName("adl")]
    public decimal? AutoDeleveragingIndicator { get; set; }

    /// <summary>
    /// ["<c>availPos</c>"] Available position
    /// </summary>
    [JsonPropertyName("availPos")]
    public decimal? AvailablePositions { get; set; }

    /// <summary>
    /// ["<c>interest</c>"] Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// ["<c>pos</c>"] Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal? PositionsQuantity { get; set; }

    /// <summary>
    /// ["<c>uplRatio</c>"] Unrealized profit and loss ratio
    /// </summary>
    [JsonPropertyName("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// ["<c>notionalUsd</c>"] Notional usd
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// ["<c>mmr</c>"] Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>mgnRatio</c>"] Margin ratio
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// ["<c>margin</c>"] Margin
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal? Margin { get; set; }

    /// <summary>
    /// ["<c>last</c>"] Last price
    /// </summary>
    [JsonPropertyName("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// ["<c>avgPx</c>"] Average price
    /// </summary>
    [JsonPropertyName("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// ["<c>liqPx</c>"] Liquidation price
    /// </summary>
    [JsonPropertyName("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// ["<c>deltaBS</c>"] Delta BS
    /// </summary>
    [JsonPropertyName("deltaBS")]
    public decimal? DeltaBS { get; set; }
    /// <summary>
    /// ["<c>deltaPA</c>"] Delta PA
    /// </summary>
    [JsonPropertyName("deltaPA")]
    public decimal? DeltaPA { get; set; }
    /// <summary>
    /// ["<c>gammaBS</c>"] Gamma BS
    /// </summary>
    [JsonPropertyName("gammaBS")]
    public decimal? GammaBS { get; set; }

    /// <summary>
    /// ["<c>gammaPA</c>"] Gamma PA
    /// </summary>
    [JsonPropertyName("gammaPA")]
    public decimal? GammaPA { get; set; }

    /// <summary>
    /// ["<c>thetaBS</c>"] Theta BS
    /// </summary>
    [JsonPropertyName("thetaBS")]
    public decimal? ThetaBS { get; set; }

    /// <summary>
    /// ["<c>thetaPA</c>"] Theta PA
    /// </summary>
    [JsonPropertyName("thetaPA")]
    public decimal? ThetaPA { get; set; }
    /// <summary>
    /// ["<c>vegaBS</c>"] Vega BS
    /// </summary>
    [JsonPropertyName("vegaBS")]
    public decimal? VegaBS { get; set; }
    /// <summary>
    /// ["<c>vegaPA</c>"] Vega PA
    /// </summary>
    [JsonPropertyName("vegaPA")]
    public decimal? VegaPA { get; set; }

    /// <summary>
    /// ["<c>idxPx</c>"] Latest underlying index price
    /// </summary>
    [JsonPropertyName("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// ["<c>bePx</c>"] Break-even price
    /// </summary>
    [JsonPropertyName("bePx")]
    public decimal? BreakEvenPrice { get; set; }

    /// <summary>
    /// ["<c>usdPx</c>"] Usd price
    /// </summary>
    [JsonPropertyName("usdPx")]
    public decimal? UsdPrice { get; set; }

    /// <summary>
    /// ["<c>markPx</c>"] Latest mark price
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// ["<c>baseBorrowed</c>"] DEPRECATED Base asset amount already borrowed
    /// </summary>
    [JsonPropertyName("baseBorrowed")]
    public decimal? BaseBorrowed { get; set; }

    /// <summary>
    /// ["<c>baseInterest</c>"] DEPRECATED Base Interest, undeducted interest that has been incurred
    /// </summary>
    [JsonPropertyName("baseInterest")]
    public decimal? BaseInterest { get; set; }

    /// <summary>
    /// ["<c>quoteBorrowed</c>"] DEPRECATED Quote asset amount already borrowed
    /// </summary>
    [JsonPropertyName("quoteBorrowed")]
    public decimal? QuoteBorrowed { get; set; }
    /// <summary>
    /// ["<c>quoteInterest</c>"] DEPRECATED Quote Interest, undeducted interest that has been incurred
    /// </summary>
    [JsonPropertyName("quoteInterest")]
    public decimal? QuoteInterest { get; set; }
    /// <summary>
    /// ["<c>spotInUseAmt</c>"] Spot in use amount
    /// </summary>
    [JsonPropertyName("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }
    /// <summary>
    /// ["<c>spotInUseCcy</c>"] Spot in use asset
    /// </summary>
    [JsonPropertyName("spotInUseCcy")]
    public string? SpotInUseAsset { get; set; }
    /// <summary>
    /// ["<c>bizRefId</c>"] External business id
    /// </summary>
    [JsonPropertyName("bizRefId")]
    public string? BusinessId { get; set; }
    /// <summary>
    /// ["<c>bizRefType</c>"] External business type
    /// </summary>
    [JsonPropertyName("bizRefType")]
    public string? BusinessType { get; set; }
    /// <summary>
    /// ["<c>uplLastPx</c>"] Unrealized profit and loss calculated by last price. Main usage is showing, actual value is upl.
    /// </summary>
    [JsonPropertyName("uplLastPx")]
    public decimal? UnrealizedPnl { get; set; }
    /// <summary>
    /// ["<c>uplRatioLastPx</c>"] Unrealized profit and loss ratio calculated by last price.
    /// </summary>
    [JsonPropertyName("uplRatioLastPx")]
    public decimal? UnrealizedPnlRatio { get; set; }
    /// <summary>
    /// ["<c>baseBal</c>"] DEPRECATED Base balance (Margin only)
    /// </summary>
    [JsonPropertyName("baseBal")]
    public decimal? BaseBalance { get; set; }
    /// <summary>
    /// ["<c>quoteBal</c>"] DEPRECATED Quote balance (Margin only)
    /// </summary>
    [JsonPropertyName("quoteBal")]
    public decimal? QuoteBalance { get; set; }
    /// <summary>
    /// ["<c>fee</c>"] Accumulated fee. Negative number represents the user transaction fee charged by the platform. Positive number represents rebate.
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }
    /// <summary>
    /// ["<c>fundingFee</c>"] Accumulated funding fee
    /// </summary>
    [JsonPropertyName("fundingFee")]
    public decimal? FundingFee { get; set; }
    /// <summary>
    /// ["<c>liqPenalty</c>"] Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonPropertyName("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }
    /// <summary>
    /// ["<c>realizedPnl</c>"] Realized profit and loss
    /// </summary>
    [JsonPropertyName("realizedPnl")]
    public decimal? RealizedPnl { get; set; }
    /// <summary>
    /// ["<c>pnl</c>"] Accumulated pnl of closing order(s)
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? Pnl { get; set; }
    /// <summary>
    /// ["<c>pendingCloseOrdLiabVal</c>"] The amount of close orders of isolated margin liability.
    /// </summary>
    [JsonPropertyName("pendingCloseOrdLiabVal")]
    public decimal? PendingCloseOrderLiability { get; set; }

    /// <summary>
    /// ["<c>clSpotInUseAmt</c>"] User-defined spot risk offset amount
    /// </summary>
    [JsonPropertyName("clSpotInUseAmt")]
    public decimal? ClSpotInUseAmount { get; set; }

    /// <summary>
    /// ["<c>maxSpotInUseAmt</c>"] Max possible spot risk offset amount
    /// </summary>
    [JsonPropertyName("maxSpotInUseAmt")]
    public decimal? MaxSpotInUseAmount { get; set; }
    /// <summary>
    /// ["<c>closeOrderAlgo</c>"] Close position algo orders attached to the position
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
    /// ["<c>algoId</c>"] Algo id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string AlgoId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>slTriggerPx</c>"] Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx")]
    public decimal StopLossTriggerPrice { get; set; }
    /// <summary>
    /// ["<c>slTriggerPxType</c>"] Stop loss trigger price type
    /// </summary>
    [JsonPropertyName("slTriggerPxType")]
    public TriggerPriceType StopLossTriggerType { get; set; }
    /// <summary>
    /// ["<c>tpTriggerPx</c>"] Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx")]
    public decimal TakeProfitTriggerPrice { get; set; }
    /// <summary>
    /// ["<c>tpTriggerPxType</c>"] Take profit trigger price type
    /// </summary>
    [JsonPropertyName("tpTriggerPxType")]
    public TriggerPriceType TakeProfitTriggerType { get; set; }
    /// <summary>
    /// ["<c>closeFraction</c>"] Fraction of position to be closed when the algo order is triggered.
    /// </summary>
    [JsonPropertyName("closeFraction")]
    public decimal? CloseFraction { get; set; }
    /// <summary>
    /// ["<c>nonSettleAvgPx</c>"] The non-settlement entry price only reflects the average price at which the position is opened or increased.
    /// </summary>
    [JsonPropertyName("nonSettleAvgPx")]
    public decimal? NonSettlementEntryPrice { get; set; }
    /// <summary>
    /// ["<c>settledPnl</c>"] Accumulated settled profit and loss (calculated by settlement price)
    /// </summary>
    [JsonPropertyName("settledPnl")]
    public decimal? SettledPnl { get; set; }
}
