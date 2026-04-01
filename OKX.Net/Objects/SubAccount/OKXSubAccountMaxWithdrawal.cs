namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount max withdrawal info
/// </summary>
[SerializationModel]
public record OKXSubAccountMaxWithdrawal
{
    /// <summary>
    /// ["<c>ccy</c>"] Currency
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>maxWd</c>"] Max withdrawal
    /// </summary>
    [JsonPropertyName("maxWd")]
    public decimal MaxWithdrawal { get; set; }

    /// <summary>
    /// ["<c>maxWdEx</c>"] Max withdrawal
    /// </summary>
    [JsonPropertyName("maxWdEx")]
    public decimal MaxWithdrawalEx { get; set; }

    /// <summary>
    /// ["<c>spotOffsetMaxWd</c>"] Spot offset max withdrawal
    /// </summary>
    [JsonPropertyName("spotOffsetMaxWd")]
    public decimal SpotOffsetMaxWithdrawal { get; set; }

    /// <summary>
    /// ["<c>spotOffsetMaxWdEx</c>"] Spot offset max withdrawal
    /// </summary>
    [JsonPropertyName("spotOffsetMaxWdEx")]
    public decimal SpotOffsetMaxWithdrawalEx { get; set; }
}