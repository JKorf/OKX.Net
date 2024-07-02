﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order info
/// </summary>
public record OKXAlgoOrder
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
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Trigger time
    /// </summary>
    [JsonPropertyName("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TriggerTime { get; set; }

    /// <summary>
    /// Algo order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Order id list
    /// </summary>
    [JsonPropertyName("ordIdList")]
    public IEnumerable<long>? OrderIdList { get; set; }

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
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonPropertyName("tdMode"), JsonConverter(typeof(EnumConverter))]
    public TradeMode TradeMode { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonPropertyName("ordType"), JsonConverter(typeof(EnumConverter))]
    public AlgoOrderType OrderType { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Actual price
    /// </summary>
    [JsonPropertyName("actualPx")]
    public decimal? ActualOrderPrice { get; set; }

    /// <summary>
    /// Actual quantity
    /// </summary>
    [JsonPropertyName("actualSz")]
    public decimal? ActualOrderQuantity { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonPropertyName("ordPx")]
    public decimal? OrderPrice { get; set; }

    /// <summary>
    /// Price limit
    /// </summary>
    [JsonPropertyName("pxLimit")]
    public decimal? PriceLimit { get; set; }

    /// <summary>
    /// Price ratio
    /// </summary>
    [JsonPropertyName("pxSpread")]
    public decimal? PriceRatio { get; set; }

    /// <summary>
    /// Price variance
    /// </summary>
    [JsonPropertyName("pxVar")]
    public decimal? PriceVariance { get; set; }

    /// <summary>
    /// Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Trigger rpice
    /// </summary>
    [JsonPropertyName("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Average quantity
    /// </summary>
    [JsonPropertyName("szLimit")]
    public decimal? AverageQuantity { get; set; }

    /// <summary>
    /// Time interval
    /// </summary>
    [JsonPropertyName("timeInterval")]
    public long? TimeInterval { get; set; }

    /// <summary>
    /// Quantity type
    /// </summary>
    [JsonPropertyName("tgtCcy"), JsonConverter(typeof(EnumConverter))]
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonPropertyName("state"), JsonConverter(typeof(EnumConverter))]
    public AlgoOrderState State { get; set; }

    /// <summary>
    /// Actual side
    /// </summary>
    [JsonPropertyName("actualSide"), JsonConverter(typeof(EnumConverter))]
    public AlgoActualSide? ActualSide { get; set; }

    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered
    /// </summary>
    [JsonPropertyName("closeFraction")]
    public decimal? CloseFraction { get; set; }

    /// <summary>
    /// Take profit trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonPropertyName("tpTriggerPxType")]
    public TriggerPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop loss trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonPropertyName("slTriggerPxType")]
    public TriggerPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonPropertyName("triggerPxType")]
    public TriggerPriceType? TriggerPriceType { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Callback price ratio
    /// </summary>
    [JsonPropertyName("callbackRatio")]
    public decimal? CallbackRatio { get; set; }

    /// <summary>
    /// Callback price variance
    /// </summary>
    [JsonPropertyName("callbackSpread")]
    public decimal? CallbackSpread { get; set; }

    /// <summary>
    /// Active price
    /// </summary>
    [JsonPropertyName("activePx")]
    public decimal? ActivePrice { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonPropertyName("moveTriggerPx")]
    public decimal? MoveTriggerPrice { get; set; }

    /// <summary>
    /// Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// Last filled price while placing
    /// </summary>
    [JsonPropertyName("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Failure code of the trigger order
    /// </summary>
    [JsonPropertyName("failCode")]
    public string? FailCode { get; set; }

    /// <summary>
    /// Client algo order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// Quick Margin type
    /// </summary>
    [JsonPropertyName("quickMgnType")]
    [JsonConverter(typeof(EnumConverter))]
    public QuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs. false: disable, the default value, true: Enable “Cost-price SL”
    /// </summary>
    [JsonPropertyName("amendPxOnTriggerType")]
    [JsonConverter(typeof(BoolConverter))]
    public bool CostPriceSlEnabled { get; set; }
}
