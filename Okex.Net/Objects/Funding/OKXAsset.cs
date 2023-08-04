namespace OKX.Net.Objects.Funding;

/// <summary>
/// Asset info
/// </summary>
public class OKXAsset
{
    /// <summary>
    /// Asset name
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Network
    /// </summary>
    [JsonProperty("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// Deposit allowed
    /// </summary>
    [JsonProperty("canDep")]
    public bool AllowDeposit { get; set; }

    /// <summary>
    /// Withdrawal allowed
    /// </summary>
    [JsonProperty("canWd")]
    public bool AllowWithdrawal { get; set; }

    /// <summary>
    /// Can internal transfer
    /// </summary>
    [JsonProperty("canInternal")]
    public bool AllowInternalTransfer { get; set; }

    /// <summary>
    /// Minimal withdrawal quantity
    /// </summary>
    [JsonProperty("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// Minimum withdrawal fee
    /// </summary>
    [JsonProperty("minFee")]
    public decimal MinimumWithdrawalFee { get; set; }

    /// <summary>
    /// Maximum withdrawal fee
    /// </summary>
    [JsonProperty("maxFee")]
    public decimal MaximumWithdrawalFee { get; set; }
}
