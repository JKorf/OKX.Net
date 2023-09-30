using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order info
/// </summary>
public class OKXAlgoOrder
{
    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trigger time
    /// </summary>
    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TriggerTime { get; set; }

    /// <summary>
    /// Algo order id
    /// </summary>
    [JsonProperty("algoId")]
    public string? AlgoId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Order id list
    /// </summary>
    [JsonProperty("ordIdList")]
    public IEnumerable<long>? OrderIdList { get; set; }

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
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OKXOrderSide OrderSide { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
    public OKXTradeMode TradeMode { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(AlgoOrderTypeConverter))]
    public OKXAlgoOrderType OrderType { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Actual price
    /// </summary>
    [JsonProperty("actualPx")]
    public decimal? ActualOrderPrice { get; set; }

    /// <summary>
    /// Actual quantity
    /// </summary>
    [JsonProperty("actualSz")]
    public decimal? ActualOrderQuantity { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("ordPx")]
    public decimal? OrderPrice { get; set; }

    /// <summary>
    /// Price limit
    /// </summary>
    [JsonProperty("pxLimit")]
    public decimal? PriceLimit { get; set; }

    /// <summary>
    /// Price ratio
    /// </summary>
    [JsonProperty("pxSpread")]
    public decimal? PriceRatio { get; set; }

    /// <summary>
    /// Price variance
    /// </summary>
    [JsonProperty("pxVar")]
    public decimal? PriceVariance { get; set; }

    /// <summary>
    /// Stop loss order price
    /// </summary>
    [JsonProperty("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Take profit order price
    /// </summary>
    [JsonProperty("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Trigger rpice
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Average quantity
    /// </summary>
    [JsonProperty("szLimit")]
    public decimal? AverageQuantity { get; set; }

    /// <summary>
    /// Time interval
    /// </summary>
    [JsonProperty("timeInterval")]
    public long? TimeInterval { get; set; }

    /// <summary>
    /// Quantity type
    /// </summary>
    [JsonProperty("tgtCcy"), JsonConverter(typeof(EnumConverter))]
    public OKXQuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(AlgoOrderStateConverter))]
    public OKXAlgoOrderState State { get; set; }

    /// <summary>
    /// Actual side
    /// </summary>
    [JsonProperty("actualSide"), JsonConverter(typeof(AlgoActualSideConverter))]
    public OKXAlgoActualSide? ActualSide { get; set; }

    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered
    /// </summary>
    [JsonProperty("closeFraction")]
    public decimal? CloseFraction { get; set; }

    /// <summary>
    /// Take profit trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonProperty("tpTriggerPxType")]
    public OXKTriggerPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop loss trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonProperty("slTriggerPxType")]
    public OXKTriggerPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonProperty("triggerPxType")]
    public OXKTriggerPriceType? TriggerPriceType { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Callback price ratio
    /// </summary>
    [JsonProperty("callbackRatio")]
    public decimal? CallbackRatio { get; set; }

    /// <summary>
    /// Callback price variance
    /// </summary>
    [JsonProperty("callbackSpread")]
    public decimal? CallbackSpread { get; set; }

    /// <summary>
    /// Active price
    /// </summary>
    [JsonProperty("activePx")]
    public decimal? ActivePrice { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("moveTriggerPx")]
    public decimal? MoveTriggerPrice { get; set; }

    /// <summary>
    /// Reduce only
    /// </summary>
    [JsonProperty("reduceOnly")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// Last filled price while placing
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Failure code of the trigger order
    /// </summary>
    [JsonProperty("failCode")]
    public string? FailCode { get; set; }

    /// <summary>
    /// Client algo order id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// Quick Margin type
    /// </summary>
    [JsonProperty("quickMgnType")]
    [JsonConverter(typeof(EnumConverter))]
    public OKXQuickMarginType? QuickMarginType { get; set; }
}
