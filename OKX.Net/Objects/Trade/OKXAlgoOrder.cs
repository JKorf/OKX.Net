using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order info
/// </summary>
[SerializationModel]
public record OKXAlgoOrder
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
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// ["<c>triggerTime</c>"] Trigger time
    /// </summary>
    [JsonPropertyName("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TriggerTime { get; set; }

    /// <summary>
    /// ["<c>algoId</c>"] Algo order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoId { get; set; }

    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>ordIdList</c>"] Order id list
    /// </summary>
    [JsonPropertyName("ordIdList")]
    public long[]? OrderIdList { get; set; }

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
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// ["<c>tdMode</c>"] Trade mode
    /// </summary>
    [JsonPropertyName("tdMode")]
    public Enums.TradeMode TradeMode { get; set; }

    /// <summary>
    /// ["<c>ordType</c>"] Order type
    /// </summary>
    [JsonPropertyName("ordType")]
    public AlgoOrderType OrderType { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>actualPx</c>"] Actual price
    /// </summary>
    [JsonPropertyName("actualPx")]
    public decimal? ActualOrderPrice { get; set; }

    /// <summary>
    /// ["<c>actualSz</c>"] Actual quantity
    /// </summary>
    [JsonPropertyName("actualSz")]
    public decimal? ActualOrderQuantity { get; set; }

    /// <summary>
    /// ["<c>ordPx</c>"] Order price
    /// </summary>
    [JsonPropertyName("ordPx")]
    public decimal? OrderPrice { get; set; }

    /// <summary>
    /// ["<c>pxLimit</c>"] Price limit
    /// </summary>
    [JsonPropertyName("pxLimit")]
    public decimal? PriceLimit { get; set; }

    /// <summary>
    /// ["<c>pxSpread</c>"] Price ratio
    /// </summary>
    [JsonPropertyName("pxSpread")]
    public decimal? PriceRatio { get; set; }

    /// <summary>
    /// ["<c>pxVar</c>"] Price variance
    /// </summary>
    [JsonPropertyName("pxVar")]
    public decimal? PriceVariance { get; set; }

    /// <summary>
    /// ["<c>slOrdPx</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// ["<c>slTriggerPx</c>"] Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// ["<c>tpOrdPx</c>"] Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// ["<c>tpTriggerPx</c>"] Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// ["<c>triggerPx</c>"] Trigger rpice
    /// </summary>
    [JsonPropertyName("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// ["<c>szLimit</c>"] Average quantity
    /// </summary>
    [JsonPropertyName("szLimit")]
    public decimal? AverageQuantity { get; set; }

    /// <summary>
    /// ["<c>timeInterval</c>"] Time interval
    /// </summary>
    [JsonPropertyName("timeInterval")]
    public long? TimeInterval { get; set; }

    /// <summary>
    /// ["<c>tgtCcy</c>"] Quantity type
    /// </summary>
    [JsonPropertyName("tgtCcy")]
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// ["<c>state</c>"] State
    /// </summary>
    [JsonPropertyName("state")]
    public AlgoOrderState State { get; set; }

    /// <summary>
    /// ["<c>actualSide</c>"] Actual side
    /// </summary>
    [JsonPropertyName("actualSide")]
    public AlgoActualSide? ActualSide { get; set; }

    /// <summary>
    /// ["<c>closeFraction</c>"] Fraction of position to be closed when the algo order is triggered
    /// </summary>
    [JsonPropertyName("closeFraction")]
    public decimal? CloseFraction { get; set; }

    /// <summary>
    /// ["<c>tpTriggerPxType</c>"] Take profit trigger price type
    /// </summary>

    [JsonPropertyName("tpTriggerPxType")]
    public TriggerPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// ["<c>slTriggerPxType</c>"] Stop loss trigger price type
    /// </summary>

    [JsonPropertyName("slTriggerPxType")]
    public TriggerPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// ["<c>triggerPxType</c>"] Trigger price type
    /// </summary>

    [JsonPropertyName("triggerPxType")]
    public TriggerPriceType? TriggerPriceType { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// ["<c>callbackRatio</c>"] Callback price ratio
    /// </summary>
    [JsonPropertyName("callbackRatio")]
    public decimal? CallbackRatio { get; set; }

    /// <summary>
    /// ["<c>callbackSpread</c>"] Callback price variance
    /// </summary>
    [JsonPropertyName("callbackSpread")]
    public decimal? CallbackSpread { get; set; }

    /// <summary>
    /// ["<c>activePx</c>"] Active price
    /// </summary>
    [JsonPropertyName("activePx")]
    public decimal? ActivePrice { get; set; }

    /// <summary>
    /// ["<c>moveTriggerPx</c>"] Trigger price
    /// </summary>
    [JsonPropertyName("moveTriggerPx")]
    public decimal? MoveTriggerPrice { get; set; }

    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// ["<c>last</c>"] Last filled price while placing
    /// </summary>
    [JsonPropertyName("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// ["<c>failCode</c>"] Failure code of the trigger order
    /// </summary>
    [JsonPropertyName("failCode")]
    public string? FailCode { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client algo order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// ["<c>quickMgnType</c>"] Quick Margin type
    /// </summary>
    [JsonPropertyName("quickMgnType")]

    public QuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// ["<c>amendPxOnTriggerType</c>"] Whether to enable Cost-price SL. Only applicable to SL order of split TPs. false: disable, the default value, true: Enable “Cost-price SL”
    /// </summary>
    [JsonPropertyName("amendPxOnTriggerType")]
    [JsonConverter(typeof(BoolConverter))]
    public bool CostPriceSlEnabled { get; set; }

    /// <summary>
    /// ["<c>isTradeBorrowMode</c>"] Whether borrowing asset automatically
    /// </summary>
    [JsonPropertyName("isTradeBorrowMode")]
    [JsonConverter(typeof(BoolConverter))]
    public bool? IsTradeBorrowMode { get; set; }

    /// <summary>
    /// ["<c>chaseType</c>"] Chase order value type
    /// </summary>
    [JsonPropertyName("chaseType")]
    public ChaseType? ChaseType { get; set; }
    /// <summary>
    /// ["<c>chaseVal</c>"] Chase value
    /// </summary>
    [JsonPropertyName("chaseVal")]
    public decimal? ChaseValue { get; set; }
    /// <summary>
    /// ["<c>maxChaseType</c>"] Max chase order value type
    /// </summary>
    [JsonPropertyName("maxChaseType")]
    public ChaseType? MaxChaseType { get; set; }
    /// <summary>
    /// ["<c>maxChaseVal</c>"] Max chase value
    /// </summary>
    [JsonPropertyName("maxChaseVal")]
    public decimal? MaxChaseValue { get; set; }

    /// <summary>
    /// ["<c>tradeQuoteCcy</c>"] Trade quote asset
    /// </summary>
    [JsonPropertyName("tradeQuoteCcy")]
    public string TradeQuoteAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>advanceOrdType</c>"] Advanced trigger order type
    /// </summary>
    [JsonPropertyName("advanceOrdType")]
    public AdvancedOrderType? AdvancedOrderType { get; set; }
}
