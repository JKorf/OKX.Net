using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Funding bill
/// </summary>
[SerializationModel]
public record OKXFundingBill
{
    /// <summary>
    /// ["<c>billId</c>"] Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

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
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public FundingBillType Type { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>clientId</c>"] Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// ["<c>notes</c>"] Notes
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}
