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

    /// <summary>
    /// The fixed deposit limit, unit in USD
    /// </summary>
    [JsonProperty("depQuotaFixed")]
    public decimal? MaxDeposit { get; set; }

    /// <summary>
    /// The layer2 network daily deposit limit
    /// </summary>
    [JsonProperty("depQuoteDailyLayer2")]
    public decimal? MaxDepositLayer2 { get; set; }

    /// <summary>
    /// Logo link
    /// </summary>
    [JsonProperty("logoLink")]
    public string LogoLink { get; set; } = string.Empty;

    /// <summary>
    /// Main net
    /// </summary>
    [JsonProperty("mainNet")]
    public bool IsMainNet { get; set; }

    /// <summary>
    /// The maximum withdrawal fee for contract address
    /// </summary>
    [JsonProperty("maxFeeForCtAddr")]
    public decimal? MaxWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// The minimum withdrawal fee for contract address
    /// </summary>
    [JsonProperty("minFeeForCtAddr")]
    public decimal? MinWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// The maximum withdrawal amount in a single transaction
    /// </summary>
    [JsonProperty("maxWd")]
    public decimal? MaxWithdrawal { get; set; }

    /// <summary>
    /// The minimum deposit amount in a single transaction
    /// </summary>
    [JsonProperty("minDep")]
    public decimal? MinDeposit { get; set; }

    /// <summary>
    /// The minimum number of blockchain confirmations to acknowledge fund deposit.
    /// </summary>
    [JsonProperty("minDepArrivalConfirm")]
    public int? MinDepositConfirmations { get; set; }

    /// <summary>
    /// The minimum number of blockchain confirmations required for withdrawal of a deposit
    /// </summary>
    [JsonProperty("minWdUnlockConfirm")]
    public int? MinDepositConfirmationsWithdraw { get; set; }

    /// <summary>
    /// Whether tag/memo information is required for withdrawal
    /// </summary>
    [JsonProperty("needTag")]
    public bool NeedsTag { get; set; }

    /// <summary>
    /// The used amount of fixed deposit quota, unit in USD
    /// </summary>
    [JsonProperty("usedDepQuotaFixed")]
    public decimal? UsedDepositQuota { get; set; }

    /// <summary>
    /// The amount of currency withdrawal used in the past 24 hours, unit in USD
    /// </summary>
    [JsonProperty("usedWdQuota")]
    public decimal? UsedWithdrawalQuota { get; set; }

    /// <summary>
    /// The withdrawal limit in the past 24 hours, unit in USD
    /// </summary>
    [JsonProperty("wdQuota")]
    public decimal? MaxWithdrawal24h { get; set; }

    /// <summary>
    /// The withdrawal precision, indicating the number of digits after the decimal point.
    /// </summary>
    [JsonProperty("wdTickSz")]
    public decimal? WithdrawalTickSize { get; set; }
}
