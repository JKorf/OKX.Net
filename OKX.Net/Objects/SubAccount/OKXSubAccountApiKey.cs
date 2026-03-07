namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount api key info
/// </summary>
[SerializationModel]
public record OKXSubAccountApiKey
{
    /// <summary>
    /// ["<c>subAcct</c>"] Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>label</c>"] Label
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>apiKey</c>"] Key
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
    /// ["<c>perm</c>"] Permissions
    /// </summary>
    [JsonPropertyName("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ip</c>"] IP addresses
    /// </summary>
    [JsonPropertyName("ip")]
    public string IpAddresses { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
