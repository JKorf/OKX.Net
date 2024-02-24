namespace OKX.Net.Objects.Funding;

/// <summary>
/// Lightning deposit
/// </summary>
public class OKXLightningDeposit
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Invoice
    /// </summary>
    [JsonProperty("invoice")]
    public string Invoice { get; set; } = string.Empty;
}