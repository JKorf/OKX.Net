using OKX.Net.Enums;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount bill
/// </summary>
[SerializationModel]
public record OKXSubAccountBill
{
    /// <summary>
    /// ["<c>billId</c>"] Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long BillId { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public SubAccountTransferType Type { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>subAcct</c>"] Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }
}
