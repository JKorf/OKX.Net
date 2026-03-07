namespace OKX.Net.Objects.Funding;

/// <summary>
/// Lightning withdrawal
/// </summary>
[SerializationModel]
public record OKXLightningWithdrawal
{
    /// <summary>
    /// ["<c>cTime</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>wdId</c>"] Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;
}
