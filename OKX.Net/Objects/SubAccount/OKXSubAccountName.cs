namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Account name
/// </summary>
public class OKXSubAccountName
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;
}
