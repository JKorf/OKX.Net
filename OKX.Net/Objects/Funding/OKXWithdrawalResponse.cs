namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal response
/// </summary>
public class OKXWithdrawalResponse
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Network
    /// </summary>
    [JsonProperty("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonProperty("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;

    /// <summary>
    /// Client id
    /// </summary>
    [JsonProperty("clientId")]
    public string? ClientId { get; set; }
}