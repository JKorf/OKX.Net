namespace OKX.Net.Objects.Account;

/// <summary>
/// Account asset valuation
/// </summary>
[SerializationModel]
public record OKXAssetValuation
{
    /// <summary>
    /// ["<c>details</c>"] Valuation details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXAssetValuationDetails Valuations { get; set; } = null!;
    /// <summary>
    /// ["<c>totalBal</c>"] Total balance
    /// </summary>
    [JsonPropertyName("totalBal")]
    public decimal TotalBalance { get; set; }
    /// <summary>
    /// ["<c>ts</c>"] Data timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Valuation details
/// </summary>
[SerializationModel]
public record OKXAssetValuationDetails
{
    /// <summary>
    /// ["<c>earn</c>"] Earn balance
    /// </summary>
    [JsonPropertyName("earn")]
    public decimal Earn { get; set; }
    /// <summary>
    /// ["<c>funding</c>"] Funding balance
    /// </summary>
    [JsonPropertyName("funding")]
    public decimal Funding { get; set; }
    /// <summary>
    /// ["<c>trading</c>"] Trading balance
    /// </summary>
    [JsonPropertyName("trading")]
    public decimal Trading { get; set; }
}
