namespace OKX.Net.Objects.Funding;

/// <summary>
/// Funding balance
/// </summary>
[SerializationModel]
public record OKXFundingBalance
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>availBal</c>"] Available balance
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal Available { get; set; }

    /// <summary>
    /// ["<c>bal</c>"] Balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// ["<c>frozenBal</c>"] Frozen balance
    /// </summary>
    [JsonPropertyName("frozenBal")]
    public decimal Frozen { get; set; }
}
