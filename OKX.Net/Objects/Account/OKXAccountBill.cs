using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account bill info
/// </summary>
public record OKXAccountBill
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Data timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType? InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OKXMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Bill id
    /// </summary>
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Balance change
    /// </summary>
    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// From account
    /// </summary>
    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OKXAccount? FromAccount { get; set; }

    /// <summary>
    /// To account
    /// </summary>
    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OKXAccount? ToAccount { get; set; }

    /// <summary>
    /// Notes
    /// </summary>
    [JsonProperty("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Marker or take
    /// </summary>
    [JsonProperty("execType")]
    public string? ExecutionType { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// Position balance
    /// </summary>
    [JsonProperty("posBal")]
    public decimal? PositionBalance { get; set; }

    /// <summary>
    /// Position balance change
    /// </summary>
    [JsonProperty("posBalChg")]
    public decimal? PositionBalanceChange { get; set; }

    /// <summary>
    /// Sub type
    /// </summary>
    [JsonProperty("subType")]
    public string? SubType { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; } = string.Empty;

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Forward price when filled, only for Options
    /// </summary>
    [JsonProperty("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }

    /// <summary>
    /// Index price at time of fill, only for Options
    /// </summary>
    [JsonProperty("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// Mark price at time of fill, only for Options
    /// </summary>
    [JsonProperty("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// Mark volatility when filled, only for Options
    /// </summary>
    [JsonProperty("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// Implied volatility when filled, only for Options
    /// </summary>
    [JsonProperty("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// Options price when filled, in the unit of USD
    /// </summary>
    [JsonProperty("fillPxUsd")]
    public decimal? FillOptionPriceUsd { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Last trade id
    /// </summary>
    [JsonProperty("tradeId")]
    public string? TradeId { get; set; }

    /// <summary>
    /// Last fill time
    /// </summary>
    [JsonProperty("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FillTime { get; set; }
}
