using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal history
/// </summary>
[SerializationModel]
public record OKXWithdrawalHistory
{
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>wdId</c>"] Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public long WithdrawalId { get; set; }

    /// <summary>
    /// ["<c>state</c>"] State
    /// </summary>
    [JsonPropertyName("state")]
    public WithdrawalState State { get; set; }

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
    /// ["<c>txId</c>"] Transaction id
    /// </summary>
    [JsonPropertyName("txId")]
    public string TransactionId { get; set; } = string.Empty;

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
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>clientId</c>"] Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// ["<c>areaCodeFrom</c>"] Area code for the phone number
    /// </summary>
    [JsonPropertyName("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; }

    /// <summary>
    /// ["<c>areaCodeTo</c>"] Area code for the phone number
    /// </summary>
    [JsonPropertyName("areaCodeTo")]
    public string? AreaCodeTo { get; set; }
}
