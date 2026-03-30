namespace OKX.Net.Objects.Account;

/// <summary>
/// Estimated Leverage Info
/// </summary>
[SerializationModel]
public record OKXEstimatedLeverageInfo
{
    /// <summary>
    /// ["<c>estMgn</c>"] Estimated margin
    /// </summary>
    [JsonPropertyName("estMgn")]
    public decimal? EstimatedMargin { get; set; }

    /// <summary>
    /// ["<c>estMaxWd</c>"] Estimated max withdrawal
    /// </summary>
    [JsonPropertyName("estMaxWd")]
    public decimal? EstimatedMaxWithdrawal { get; set; }
}