using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account bill info
/// </summary>
public class OKXAccountBill
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
}
