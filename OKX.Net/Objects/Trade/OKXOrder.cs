using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order info
/// </summary>
public class OKXOrder
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
    /// Fill time
    /// </summary>
    [JsonProperty("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FillTime { get; set; }

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
    /// Trade id
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

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
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Position type
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide? PositionSide { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(OrderTypeConverter))]
    public OKXOrderType OrderType { get; set; }

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
    /// Order state
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter))]
    public OKXOrderState OrderState { get; set; }

    /// <summary>
    /// Quantity type
    /// </summary>
    [JsonProperty("tgtCcy"), JsonConverter(typeof(EnumConverter))]
    public OKXQuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Category
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    /// <summary>
    /// Fill price
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Fill quantity
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal? QuantityFilled { get; set; }

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take profit order price
    /// </summary>
    [JsonProperty("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop loss order price
    /// </summary>
    [JsonProperty("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// Fee asset
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Rebate asset
    /// </summary>
    [JsonProperty("rebateCcy")]
    public string RebateAsset { get; set; } = string.Empty;

    /// <summary>
    /// Rebate
    /// </summary>
    [JsonProperty("rebate")]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// Reduce only
    /// </summary>
    [JsonProperty("reduceOnly")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Client-supplied Algo ID when plaing order attaching TP/SL.
    /// </summary>
    [JsonProperty("attachAlgoClOrdId")]
    public string? AttachAlgoCllientOrderId { get; set; }

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
    /// Self trade prevention ID
    /// </summary>
    [JsonProperty("stpId")]
    public string? SelfTradePreventionId { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonProperty("stpMode")]
    [JsonConverter(typeof(EnumConverter))]
    public OKXSelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// Order source
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Code of the cancellation source.
    /// </summary>
    [JsonProperty("cancelSource")]
    public string? CancelSource { get; set; }

    /// <summary>
    /// Reason of the cancellation source.
    /// </summary>
    [JsonProperty("cancelSourceReason")]
    public string? CancelSourceReason { get; set; }

    /// <summary>
    /// Client algo order id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// Algo id
    /// </summary>
    [JsonProperty("algoId")]
    public string? AlgoId { get; set; }

    /// <summary>
    /// Quick Margin type
    /// </summary>
    [JsonProperty("quickMgnType")]
    [JsonConverter(typeof(EnumConverter))]
    public OKXQuickMarginType? QuickMarginType { get; set; }

}