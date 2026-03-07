namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Copy trading account info
/// </summary>
[SerializationModel]
public record OKXCopyTradingAccount
{
    /// <summary>
    /// ["<c>uniqueCode</c>"] User unique code
    /// </summary>
    [JsonPropertyName("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>nickName</c>"] Nickname
    /// </summary>
    [JsonPropertyName("nickName")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>portLink</c>"] Portrait link
    /// </summary>
    [JsonPropertyName("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>details</c>"] Details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXCopyTradingAccountDetails[] Details { get; set; } = [];
}
