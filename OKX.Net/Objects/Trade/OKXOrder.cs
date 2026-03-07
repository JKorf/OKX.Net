using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order info
/// </summary>
[SerializationModel]
public record OKXOrder
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
    /// ["<c>fillTime</c>"] Fill time
    /// </summary>
    [JsonPropertyName("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FillTime { get; set; }

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
    /// ["<c>tradeId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long? TradeId { get; set; }

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
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position type
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// ["<c>ordType</c>"] Order type
    /// </summary>
    [JsonPropertyName("ordType")]
    public OrderType OrderType { get; set; }

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
    /// ["<c>state</c>"] Order state
    /// </summary>
    [JsonPropertyName("state")]
    public OrderStatus OrderState { get; set; }

    /// <summary>
    /// ["<c>tgtCcy</c>"] Quantity type
    /// </summary>
    [JsonPropertyName("tgtCcy")]
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// ["<c>avgPx</c>"] Average price
    /// </summary>
    [JsonPropertyName("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>pnl</c>"] Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>category</c>"] Category
    /// </summary>
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>accFillSz</c>"] Accumulated fill quantity
    /// </summary>
    [JsonPropertyName("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    /// <summary>
    /// ["<c>fillPx</c>"] Fill price
    /// </summary>
    [JsonPropertyName("fillPx")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// ["<c>fillSz</c>"] Fill quantity
    /// </summary>
    [JsonPropertyName("fillSz")]
    public decimal? QuantityFilled { get; set; }

    /// <summary>
    /// ["<c>tpTriggerPx</c>"] Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// ["<c>tpOrdPx</c>"] Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// ["<c>slTriggerPx</c>"] Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// ["<c>slOrdPx</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// ["<c>feeCcy</c>"] Fee asset
    /// </summary>
    [JsonPropertyName("feeCcy")]
    public string FeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// ["<c>rebateCcy</c>"] Rebate asset
    /// </summary>
    [JsonPropertyName("rebateCcy")]
    public string RebateAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>rebate</c>"] Rebate
    /// </summary>
    [JsonPropertyName("rebate")]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// ["<c>attachAlgoClOrdId</c>"] Client-supplied Algo ID when placing order attaching TP/SL.
    /// </summary>
    [JsonPropertyName("attachAlgoClOrdId")]
    public string? AttachAlgoCllientOrderId { get; set; }

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
    /// ["<c>stpId</c>"] Self trade prevention ID
    /// </summary>
    [JsonPropertyName("stpId")]
    public string? SelfTradePreventionId { get; set; }

    /// <summary>
    /// ["<c>stpMode</c>"] Self trade prevention mode
    /// </summary>
    [JsonPropertyName("stpMode")]

    public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// ["<c>source</c>"] Order source
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>cancelSource</c>"] Code of the cancellation source.
    /// </summary>
    [JsonPropertyName("cancelSource")]
    public string? CancelSource { get; set; }

    /// <summary>
    /// ["<c>cancelSourceReason</c>"] Reason of the cancellation source.
    /// </summary>
    [JsonPropertyName("cancelSourceReason")]
    public string? CancelSourceReason { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client algo order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// ["<c>algoId</c>"] Algo id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoId { get; set; }

    /// <summary>
    /// ["<c>quickMgnType</c>"] Quick Margin type
    /// </summary>
    [JsonPropertyName("quickMgnType")]

    public QuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// ["<c>isTpLimit</c>"] Is take profit limit order or not
    /// </summary>
    [JsonPropertyName("isTpLimit")]
    public bool? IsTakeProfitLimit { get; set; }

    /// <summary>
    /// ["<c>pxType</c>"] Price type for options
    /// </summary>
    [JsonPropertyName("pxType")]
    public string? PriceType { get; set; }

    /// <summary>
    /// ["<c>pxUsd</c>"] Usd price for options
    /// </summary>
    [JsonPropertyName("pxUsd")]
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// ["<c>pxVol</c>"] Implied volatility for options
    /// </summary>
    [JsonPropertyName("pxVol")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// ["<c>lastPx</c>"] Last price
    /// </summary>
    [JsonPropertyName("lastPx")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// ["<c>tradeQuoteCcy</c>"] Trade quote asset
    /// </summary>
    [JsonPropertyName("tradeQuoteCcy")]
    public string TradeQuoteAsset { get; set; } = string.Empty;

}
