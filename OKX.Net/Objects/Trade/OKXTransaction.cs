using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Transaction info
/// </summary>
[SerializationModel]
public record OKXTransaction
{
    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

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
    /// ["<c>billId</c>"] Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

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
    /// ["<c>fillIdxPx</c>"] Index price at the moment of trade execution
    /// </summary>
    [JsonPropertyName("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// ["<c>fillPnl</c>"] Last filled profit and loss, applicable to orders which have a trade and aim to close position
    /// </summary>
    [JsonPropertyName("fillPnl")]
    public decimal? FillProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>execType</c>"] Order flow type
    /// </summary>
    [JsonPropertyName("execType")]
    public OrderFlowType OrderFlowType { get; set; }

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
    /// ["<c>fillTime</c>"] Trade time
    /// </summary>
    [JsonPropertyName("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FillTime { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Data time
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>fillPxVol</c>"] Implied volitality for options
    /// </summary>
    [JsonPropertyName("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// ["<c>fillPxUsd</c>"] Usd fill price for options
    /// </summary>
    [JsonPropertyName("fillPxUsd")]
    public decimal? FillUsdPrice { get; set; }

    /// <summary>
    /// ["<c>fillMarkVol</c>"] Mark volatility when filled for options
    /// </summary>
    [JsonPropertyName("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// ["<c>fillFwdPx</c>"] Forward price when filled for options
    /// </summary>
    [JsonPropertyName("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }

    /// <summary>
    /// ["<c>fillMarkPx</c>"] Mark price when filled
    /// </summary>
    [JsonPropertyName("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// ["<c>subType</c>"] Transaction type
    /// </summary>
    [JsonPropertyName("subType")]
    public string TransactionType { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>tradeQuoteCcy</c>"] Quote asset
    /// </summary>
    [JsonPropertyName("tradeQuoteCcy")]
    public string TradeQuoteAsset { get; set; } = string.Empty;
}
