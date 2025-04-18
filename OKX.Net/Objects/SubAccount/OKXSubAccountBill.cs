using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount bill
/// </summary>
[SerializationModel]
public record OKXSubAccountBill
{
    /// <summary>
    /// Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long BillId { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonPropertyName("type")]
    public SubAccountTransferType Type { get; set; }

    /// <summary>
    /// asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }
}
