namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal response
/// </summary>
[SerializationModel]
public record OKXWithdrawalResponse
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>chain</c>"] Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>wdId</c>"] Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>clientId</c>"] Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}
