namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Copy trading action response
/// </summary>
[SerializationModel]
public record OKXCopyTradingActionResponse
{
    /// <summary>
    /// ["<c>subPosId</c>"] Lead position ID
    /// </summary>
    [JsonPropertyName("subPosId")]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>tag</c>"] Order tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;
}