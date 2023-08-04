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
    public long? AlgoId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

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
    [JsonProperty("tgtCcy"), JsonConverter(typeof(QuantityTypeConverter))]
    public OKXQuantityType? QuantityType { get; set; }

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
}
