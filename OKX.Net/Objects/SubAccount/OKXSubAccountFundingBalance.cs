namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Sub account funding balance
/// </summary>
[SerializationModel]
public record OKXSubAccountFundingBalance
{
    /// <summary>
    /// ["<c>availBal</c>"] Available balance
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// ["<c>bal</c>"] Balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>frozenBal</c>"] Frozen balance
    /// </summary>
    [JsonPropertyName("frozenBal")]
    public decimal? FrozenBalance { get; set; }
}
