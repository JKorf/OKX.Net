using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount bill
/// </summary>
public class OKXSubAccountBill
{
    /// <summary>
    /// Bill id
    /// </summary>
    [JsonProperty("billId")]
    public long BillId { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(SubAccountTransferTypeConverter))]
    public OKXSubAccountTransferType Type { get; set; }

    /// <summary>
    /// asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Quantity { get; set; }
}
