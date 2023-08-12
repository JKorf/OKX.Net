using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount info
/// </summary>
public class OKXSubAccount
{
    /// <summary>
    /// Enabled
    /// </summary>
    [JsonProperty("enable")]
    public bool Enable { get; set; }

    /// <summary>
    /// Google auth enabled
    /// </summary>
    [JsonProperty("gAuth")]
    public bool GoogleAuth { get; set; }

    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Label
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Mobile
    /// </summary>
    [JsonProperty("mobile")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// Can transfer out
    /// </summary>
    [JsonProperty("canTransOut")]
    public bool CanTransOut { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(SubAccountTypeConverter))]
    public OKXSubAccountType Type { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("uid")]
    public string Uid { get; set; } = string.Empty;
}
