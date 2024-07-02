using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Funding bill
/// </summary>
public record OKXFundingBill
{
    /// <summary>
    /// Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

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
    /// Type
    /// </summary>
    [JsonPropertyName("type"), JsonConverter(typeof(EnumConverter))]
    public FundingBillType Type { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}