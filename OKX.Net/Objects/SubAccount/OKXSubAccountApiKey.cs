using OKX.Net.Converters;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount api key info
/// </summary>
public class OKXSubAccountApiKey
{
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
    /// Key
    /// </summary>
    [JsonProperty("apiKey")]
    public string apiKey { get; set; } = string.Empty;

    /*
    [JsonProperty("secretKey")]
    public string secretKey { get; set; }

    [JsonProperty("Passphrase")]
    public string Passphrase { get; set; }

    [JsonProperty("perm"), JsonConverter(typeof(ApiPermissionConverter))]
    public OkexApiPermission Permission { get; set; }
    */

    /// <summary>
    /// Permissions
    /// </summary>
    [JsonProperty("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// IP addresses
    /// </summary>
    [JsonProperty("ip")]
    public string IpAddresses { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
