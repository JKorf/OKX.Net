using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order info
/// </summary>
[SerializationModel]
public record OKXOrder
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
    /// Fill time
    /// </summary>
    [JsonPropertyName("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FillTime { get; set; }

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
    /// Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Position type
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonPropertyName("ordType")]
    public OrderType OrderType { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonPropertyName("tdMode")]
    public Enums.TradeMode TradeMode { get; set; }

    /// <summary>
    /// Order state
    /// </summary>
    [JsonPropertyName("state")]
    public OrderStatus OrderState { get; set; }

    /// <summary>
    /// Quantity type
    /// </summary>
    [JsonPropertyName("tgtCcy")]
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonPropertyName("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Category
    /// </summary>
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Accumulated fill quantity
    /// </summary>
    [JsonPropertyName("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    /// <summary>
    /// Fill price
    /// </summary>
    [JsonPropertyName("fillPx")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Fill quantity
    /// </summary>
    [JsonPropertyName("fillSz")]
    public decimal? QuantityFilled { get; set; }

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// Fee asset
    /// </summary>
    [JsonPropertyName("feeCcy")]
    public string FeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Rebate asset
    /// </summary>
    [JsonPropertyName("rebateCcy")]
    public string RebateAsset { get; set; } = string.Empty;

    /// <summary>
    /// Rebate
    /// </summary>
    [JsonPropertyName("rebate")]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Client-supplied Algo ID when placing order attaching TP/SL.
    /// </summary>
    [JsonPropertyName("attachAlgoClOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? AttachAlgoCllientOrderId { get; set; }

    /// <summary>
    /// Take profit trigger price type
    /// </summary>

    [JsonPropertyName("tpTriggerPxType")]
    public TriggerPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop loss trigger price type
    /// </summary>

    [JsonPropertyName("slTriggerPxType")]
    public TriggerPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Self trade prevention ID
    /// </summary>
    [JsonPropertyName("stpId")]
    public string? SelfTradePreventionId { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonPropertyName("stpMode")]

    public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// Order source
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Code of the cancellation source.
    /// </summary>
    [JsonPropertyName("cancelSource")]
    public string? CancelSource { get; set; }

    /// <summary>
    /// Reason of the cancellation source.
    /// </summary>
    [JsonPropertyName("cancelSourceReason")]
    public string? CancelSourceReason { get; set; }

    /// <summary>
    /// Client algo order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// Algo id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoId { get; set; }

    /// <summary>
    /// Quick Margin type
    /// </summary>
    [JsonPropertyName("quickMgnType")]

    public QuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// Is take profit limit order or not
    /// </summary>
    [JsonPropertyName("isTpLimit")]
    public bool? IsTakeProfitLimit { get; set; }

    /// <summary>
    /// Price type for options
    /// </summary>
    [JsonPropertyName("pxType")]
    public string? PriceType { get; set; }

    /// <summary>
    /// Usd price for options
    /// </summary>
    [JsonPropertyName("pxUsd")]
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// Implied volatility for options
    /// </summary>
    [JsonPropertyName("pxVol")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// Last price
    /// </summary>
    [JsonPropertyName("lastPx")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Trade quote asset
    /// </summary>
    [JsonPropertyName("tradeQuoteCcy")]
    public string TradeQuoteAsset { get; set; } = string.Empty;

}
