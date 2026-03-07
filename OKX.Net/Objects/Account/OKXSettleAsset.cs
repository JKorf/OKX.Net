namespace OKX.Net.Objects.Account;

/// <summary>
/// Settlement asset
/// </summary>
public record OKXSettleAsset
{
    /// <summary>
    /// ["<c>settleCcy</c>"] Settlement asset
    /// </summary>
    [JsonPropertyName("settleCcy")]
    public string SettleAsset { get; set; } = string.Empty;
}
