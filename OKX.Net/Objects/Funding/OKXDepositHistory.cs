using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Deposit history
/// </summary>
[SerializationModel]
public record OKXDepositHistory
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>chain</c>"] Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>from</c>"] From
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>to</c>"] To
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>txId</c>"] Transaction id
    /// </summary>
    [JsonPropertyName("txId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>depId</c>"] Deposit id
    /// </summary>
    [JsonPropertyName("depId")]
    public string DepositId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>state</c>"] Deposit state
    /// </summary>
    [JsonPropertyName("state")]
    public DepositState State { get; set; }

    /// <summary>
    /// ["<c>actualDepBlkConfirm</c>"] Actual amount of blockchain confirm in a single deposit
    /// </summary>
    [JsonPropertyName("actualDepBlkConfirm")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// ["<c>areaCodeFrom</c>"] If from is a phone number, this parameter return area code of the phone number
    /// </summary>
    [JsonPropertyName("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>fromWdId</c>"] Internal transfer initiator's withdrawal ID
    /// </summary>
    [JsonPropertyName("fromWdId")]
    public string? FromWithdrawalId { get; set; } = string.Empty;
}
