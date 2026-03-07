namespace OKX.Net.Objects.Funding;

/// <summary>
/// Lightning deposit
/// </summary>
[SerializationModel]
public record OKXLightningDeposit
{
    /// <summary>
    /// ["<c>cTime</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>invoice</c>"] Invoice
    /// </summary>
    [JsonPropertyName("invoice")]
    public string Invoice { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>verifiedName</c>"] Verified name for recipient
    /// </summary>
    [JsonPropertyName("verifiedName")]
    public string RecepientName { get; set; } = string.Empty;
}
