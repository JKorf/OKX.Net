namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Entrust subaccount info
/// </summary>
[SerializationModel]
public record OKXEntrustSubAccount
{
    /// <summary>
    /// ["<c>subAcct</c>"] Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;
}