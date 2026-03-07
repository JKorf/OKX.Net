namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Account name
/// </summary>
[SerializationModel]
public record OKXSubAccountName
{
    /// <summary>
    /// ["<c>subAcct</c>"] Name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;
}
