namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Account name
/// </summary>
public record OKXSubAccountName
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;
}
