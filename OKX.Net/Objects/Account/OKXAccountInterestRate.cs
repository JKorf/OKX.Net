namespace OKX.Net.Objects.Account;

/// <summary>
/// Interest rate
/// </summary>
[SerializationModel]
public record OKXAccountInterestRate
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>interestRate</c>"] Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }
}
