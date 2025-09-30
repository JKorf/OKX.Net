using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account bill info
/// </summary>
[SerializationModel]
public record OKXAccountBill
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Data timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType? InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Balance change
    /// </summary>
    [JsonPropertyName("balChg")]
    public decimal? BalanceChange { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// From account
    /// </summary>
    [JsonPropertyName("from")]
    public AccountType? FromAccount { get; set; }

    /// <summary>
    /// To account
    /// </summary>
    [JsonPropertyName("to")]
    public AccountType? ToAccount { get; set; }

    /// <summary>
    /// Notes
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Marker or take
    /// </summary>
    [JsonPropertyName("execType")]
    public string? ExecutionType { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// Position balance
    /// </summary>
    [JsonPropertyName("posBal")]
    public decimal? PositionBalance { get; set; }

    /// <summary>
    /// Position balance change
    /// </summary>
    [JsonPropertyName("posBalChg")]
    public decimal? PositionBalanceChange { get; set; }

    /// <summary>
    /// Sub type
    /// </summary>
    [JsonPropertyName("subType")]
    public AccountBillSubType? SubType { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; } = string.Empty;

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Forward price when filled, only for Options
    /// </summary>
    [JsonPropertyName("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }

    /// <summary>
    /// Index price at time of fill, only for Options
    /// </summary>
    [JsonPropertyName("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// Mark price at time of fill, only for Options
    /// </summary>
    [JsonPropertyName("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// Mark volatility when filled, only for Options
    /// </summary>
    [JsonPropertyName("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// Implied volatility when filled, only for Options
    /// </summary>
    [JsonPropertyName("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// Options price when filled, in the unit of USD
    /// </summary>
    [JsonPropertyName("fillPxUsd")]
    public decimal? FillOptionPriceUsd { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Last trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string? TradeId { get; set; }

    /// <summary>
    /// Last fill time
    /// </summary>
    [JsonPropertyName("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FillTime { get; set; }
    /// <summary>
    /// Earn quantity
    /// </summary>
    [JsonPropertyName("earnAmt")]
    public decimal? EarnQuantity { get; set; }
    /// <summary>
    /// Earn APR
    /// </summary>
    [JsonPropertyName("earnApr")]
    public decimal? EarnApr { get; set; }
}
