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
    [JsonProperty("tgtCcy"), JsonConverter(typeof(QuantityTypeConverter))]
    public OKXQuantityType? QuantityType { get; set; }

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
}