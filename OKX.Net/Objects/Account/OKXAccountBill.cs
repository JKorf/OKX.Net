using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account bill info
/// </summary>
[SerializationModel]
public record OKXAccountBill
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Data timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType? InstrumentType { get; set; }

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// ["<c>billId</c>"] Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>bal</c>"] Balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// ["<c>balChg</c>"] Balance change
    /// </summary>
    [JsonPropertyName("balChg")]
    public decimal? BalanceChange { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// ["<c>from</c>"] From account
    /// </summary>
    [JsonPropertyName("from")]
    public AccountType? FromAccount { get; set; }

    /// <summary>
    /// ["<c>to</c>"] To account
    /// </summary>
    [JsonPropertyName("to")]
    public AccountType? ToAccount { get; set; }

    /// <summary>
    /// ["<c>notes</c>"] Notes
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// ["<c>execType</c>"] Marker or take
    /// </summary>
    [JsonPropertyName("execType")]
    public string? ExecutionType { get; set; }

    /// <summary>
    /// ["<c>pnl</c>"] Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>posBal</c>"] Position balance
    /// </summary>
    [JsonPropertyName("posBal")]
    public decimal? PositionBalance { get; set; }

    /// <summary>
    /// ["<c>posBalChg</c>"] Position balance change
    /// </summary>
    [JsonPropertyName("posBalChg")]
    public decimal? PositionBalanceChange { get; set; }

    /// <summary>
    /// ["<c>subType</c>"] Sub type
    /// </summary>
    [JsonPropertyName("subType")]
    public AccountBillSubType? SubType { get; set; }

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>fillFwdPx</c>"] Forward price when filled, only for Options
    /// </summary>
    [JsonPropertyName("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }

    /// <summary>
    /// ["<c>fillIdxPx</c>"] Index price at time of fill, only for Options
    /// </summary>
    [JsonPropertyName("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// ["<c>fillMarkPx</c>"] Mark price at time of fill, only for Options
    /// </summary>
    [JsonPropertyName("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// ["<c>fillMarkVol</c>"] Mark volatility when filled, only for Options
    /// </summary>
    [JsonPropertyName("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// ["<c>fillPxVol</c>"] Implied volatility when filled, only for Options
    /// </summary>
    [JsonPropertyName("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// ["<c>fillPxUsd</c>"] Options price when filled, in the unit of USD
    /// </summary>
    [JsonPropertyName("fillPxUsd")]
    public decimal? FillOptionPriceUsd { get; set; }

    /// <summary>
    /// ["<c>interest</c>"] Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// ["<c>tradeId</c>"] Last trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string? TradeId { get; set; }

    /// <summary>
    /// ["<c>fillTime</c>"] Last fill time
    /// </summary>
    [JsonPropertyName("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FillTime { get; set; }
    /// <summary>
    /// ["<c>earnAmt</c>"] Earn quantity
    /// </summary>
    [JsonPropertyName("earnAmt")]
    public decimal? EarnQuantity { get; set; }
    /// <summary>
    /// ["<c>earnApr</c>"] Earn APR
    /// </summary>
    [JsonPropertyName("earnApr")]
    public decimal? EarnApr { get; set; }
}
