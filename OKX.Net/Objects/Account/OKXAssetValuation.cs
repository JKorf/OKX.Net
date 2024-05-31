namespace OKX.Net.Objects.Account;

/// <summary>
/// Account asset valuation
/// </summary>
public record OKXAssetValuation
{
    /// <summary>
    /// Valuation details
    /// </summary>
    [JsonProperty("details")]
    public OKXAssetValuationDetails Valuations { get; set; } = null!;
    /// <summary>
    /// Total balance
    /// </summary>
    [JsonProperty("totalBal")]
    public decimal TotalBalance { get; set; }
    /// <summary>
    /// Data timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Valuation details
/// </summary>
public record OKXAssetValuationDetails
{
    /// <summary>
    /// Earn balance
    /// </summary>
    [JsonProperty("earn")]
    public decimal Earn { get; set; }
    /// <summary>
    /// Funding balance
    /// </summary>
    [JsonProperty("funding")]
    public decimal Funding { get; set; }
    /// <summary>
    /// Trading balance
    /// </summary>
    [JsonProperty("trading")]
    public decimal Trading { get; set; }
}
