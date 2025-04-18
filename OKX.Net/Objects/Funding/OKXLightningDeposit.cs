using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Funding;

/// <summary>
/// Lightning deposit
/// </summary>
[SerializationModel]
public record OKXLightningDeposit
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Invoice
    /// </summary>
    [JsonPropertyName("invoice")]
    public string Invoice { get; set; } = string.Empty;

    /// <summary>
    /// Verified name for recipient
    /// </summary>
    [JsonPropertyName("verifiedName")]
    public string RecepientName { get; set; } = string.Empty;
}
