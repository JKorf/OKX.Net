namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Copy trading account info
/// </summary>
[SerializationModel]
public record OKXCopyTradingAccount
{
    /// <summary>
    /// User unique code
    /// </summary>
    [JsonPropertyName("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// Nickname
    /// </summary>
    [JsonPropertyName("nickName")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonPropertyName("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXCopyTradingAccountDetails[] Details { get; set; } = [];
}
