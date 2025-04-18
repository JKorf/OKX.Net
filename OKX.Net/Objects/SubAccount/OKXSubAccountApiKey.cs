using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount api key info
/// </summary>
[SerializationModel]
public record OKXSubAccountApiKey
{
    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Label
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Key
    /// </summary>
    [JsonPropertyName("apiKey")]
    public string apiKey { get; set; } = string.Empty;

    /*
    [JsonPropertyName("secretKey")]
    public string secretKey { get; set; }

    [JsonPropertyName("Passphrase")]
    public string Passphrase { get; set; }

    [JsonPropertyName("perm"), JsonConverter(typeof(ApiPermissionConverter))]
    public OkexApiPermission Permission { get; set; }
    */

    /// <summary>
    /// Permissions
    /// </summary>
    [JsonPropertyName("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// IP addresses
    /// </summary>
    [JsonPropertyName("ip")]
    public string IpAddresses { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
