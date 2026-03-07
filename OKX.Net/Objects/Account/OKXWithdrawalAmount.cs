namespace OKX.Net.Objects.Account;

/// <summary>
/// Withdrawal info
/// </summary>
[SerializationModel]
public record OKXWithdrawalAmount
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>maxWd</c>"] Maximum withdrawal amount
    /// </summary>
    [JsonPropertyName("maxWd")]
    public decimal? MaximumWithdrawal { get; set; }

    /// <summary>
    /// ["<c>maxWdEx</c>"] Max withdrawal (including borrowed assets under Multi-currency margin/Portfolio margin)
    /// </summary>
    [JsonPropertyName("maxWdEx")]
    public decimal? MaximumWithdrawalIncl { get; set; }

    /// <summary>
    /// ["<c>spotOffsetMaxWd</c>"] Max withdrawal under Spot-Derivatives risk offset mode (excluding borrowed assets under Portfolio margin)
    /// </summary>
    [JsonPropertyName("spotOffsetMaxWd")]
    public decimal? MaximumWithdrawalSpotOffset { get; set; }

    /// <summary>
    ///	["<c>spotOffsetMaxWdEx</c>"] Max withdrawal under Spot-Derivatives risk offset mode (including borrowed assets under Portfolio margin)
    /// </summary>
    [JsonPropertyName("spotOffsetMaxWdEx")]
    public decimal? MaximumWithdrawalSpotOffsetIncl { get; set; }
}
