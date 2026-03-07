namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal id
/// </summary>
[SerializationModel]
public record OKXWithdrawalId
{
    /// <summary>
    /// ["<c>wdId</c>"] Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;
}
