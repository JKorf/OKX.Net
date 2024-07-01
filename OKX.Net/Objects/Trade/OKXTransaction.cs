using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Transaction info
/// </summary>
public record OKXTransaction
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

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
    /// Bill id
    /// </summary>
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

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
    /// Index price at the moment of trade execution
    /// </summary>
    [JsonProperty("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// Last filled profit and loss, applicable to orders which have a trade and aim to close position
    /// </summary>
    [JsonProperty("fillPnl")]
    public decimal? FillProfitAndLoss { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OKXOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide PositionSide { get; set; }

    /// <summary>
    /// Order flow type
    /// </summary>
    [JsonProperty("execType"), JsonConverter(typeof(OrderFlowTypeConverter))]
    public OKXOrderFlowType OrderFlowType { get; set; }

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
    /// Trade time
    /// </summary>
    [JsonProperty("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FillTime { get; set; }

    /// <summary>
    /// Data time
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Implied volitality for options
    /// </summary>
    [JsonProperty("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// Usd fill price for options
    /// </summary>
    [JsonProperty("fillPxUsd")]
    public decimal? FillUsdPrice { get; set; }

    /// <summary>
    /// Mark volatility when filled for options
    /// </summary>
    [JsonProperty("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// Forward price when filled for options
    /// </summary>
    [JsonProperty("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }

    /// <summary>
    /// Mark price when filled
    /// </summary>
    [JsonProperty("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonProperty("subType")]
    public string TransactionType { get; set; } = string.Empty;
}