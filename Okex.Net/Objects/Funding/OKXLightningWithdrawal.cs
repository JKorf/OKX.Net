using OKX.Net.Converters;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Lightning withdrawal
/// </summary>
public class OKXLightningWithdrawal
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonProperty("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;
}