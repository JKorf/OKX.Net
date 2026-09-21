namespace OKX.Net.Objects.Funding;

/// <summary>
/// Non-tradable funding asset and its withdrawal details
/// </summary>
[SerializationModel]
public record OKXNonTradableAsset
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset code
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>name</c>"] Currency name, empty when unavailable
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>logoLink</c>"] Currency logo link
    /// </summary>
    [JsonPropertyName("logoLink")]
    public string LogoLink { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>bal</c>"] Withdrawable balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// ["<c>canWd</c>"] Whether on-chain withdrawals are available
    /// </summary>
    [JsonPropertyName("canWd")]
    public bool CanWithdraw { get; set; }

    /// <summary>
    /// ["<c>chain</c>"] Withdrawal chain
    /// </summary>
    [JsonPropertyName("chain")]
    public string Chain { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>minWd</c>"] Minimum withdrawal amount
    /// </summary>
    [JsonPropertyName("minWd")]
    public decimal MinimumWithdrawal { get; set; }

    /// <summary>
    /// ["<c>wdAll</c>"] Whether the entire balance must be withdrawn at once
    /// </summary>
    [JsonPropertyName("wdAll")]
    public bool WithdrawAll { get; set; }

    /// <summary>
    /// ["<c>fee</c>"] Fixed withdrawal fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal WithdrawalFee { get; set; }

    /// <summary>
    /// ["<c>feeCcy</c>"] Fixed withdrawal fee currency
    /// </summary>
    [JsonPropertyName("feeCcy")]
    public string WithdrawalFeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>burningFeeRate</c>"] Burning fee rate, or null when not applicable
    /// </summary>
    [JsonPropertyName("burningFeeRate")]
    public decimal? BurningFeeRate { get; set; }

    /// <summary>
    /// ["<c>ctAddr</c>"] Last six characters of the contract address
    /// </summary>
    [JsonPropertyName("ctAddr")]
    public string ContractAddress { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>needTag</c>"] Whether withdrawals require a tag
    /// </summary>
    [JsonPropertyName("needTag")]
    public bool NeedsTag { get; set; }

    /// <summary>
    /// ["<c>wdTickSz</c>"] Number of decimal places supported for withdrawals
    /// </summary>
    [JsonPropertyName("wdTickSz")]
    public int WithdrawalPrecision { get; set; }
}
