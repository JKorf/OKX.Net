namespace OKX.Net.Objects.Funding;

/// <summary>
/// Asset info
/// </summary>
[SerializationModel]
public record OKXAsset
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset name
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>name</c>"] Name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>chain</c>"] Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>canDep</c>"] Deposit allowed
    /// </summary>
    [JsonPropertyName("canDep")]
    public bool AllowDeposit { get; set; }

    /// <summary>
    /// ["<c>canWd</c>"] Withdrawal allowed
    /// </summary>
    [JsonPropertyName("canWd")]
    public bool AllowWithdrawal { get; set; }

    /// <summary>
    /// ["<c>canInternal</c>"] Can internal transfer
    /// </summary>
    [JsonPropertyName("canInternal")]
    public bool AllowInternalTransfer { get; set; }

    /// <summary>
    /// ["<c>minWd</c>"] Minimal withdrawal quantity
    /// </summary>
    [JsonPropertyName("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// ["<c>fee</c>"] Fixed withdrawal fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal FixedWithdrawalFee { get; set; }

    /// <summary>
    /// ["<c>minFee</c>"] DEPRECATED
    /// </summary>
    [JsonPropertyName("minFee")]
    public decimal MinimumWithdrawalFee { get; set; }

    /// <summary>
    /// ["<c>maxFee</c>"] DEPRECATED
    /// </summary>
    [JsonPropertyName("maxFee")]
    public decimal MaximumWithdrawalFee { get; set; }

    /// <summary>
    /// ["<c>depQuotaFixed</c>"] The fixed deposit limit, unit in USD
    /// </summary>
    [JsonPropertyName("depQuotaFixed")]
    public decimal? MaxDeposit { get; set; }

    /// <summary>
    /// ["<c>depQuoteDailyLayer2</c>"] The layer2 network daily deposit limit
    /// </summary>
    [JsonPropertyName("depQuoteDailyLayer2")]
    public decimal? MaxDepositLayer2 { get; set; }

    /// <summary>
    /// ["<c>logoLink</c>"] Logo link
    /// </summary>
    [JsonPropertyName("logoLink")]
    public string LogoLink { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>mainNet</c>"] Main net
    /// </summary>
    [JsonPropertyName("mainNet")]
    public bool IsMainNet { get; set; }

    /// <summary>
    /// ["<c>maxFeeForCtAddr</c>"] DEPRECATED
    /// </summary>
    [JsonPropertyName("maxFeeForCtAddr")]
    public decimal? MaxWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// ["<c>minFeeForCtAddr</c>"] DEPRECATED
    /// </summary>
    [JsonPropertyName("minFeeForCtAddr")]
    public decimal? MinWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// ["<c>maxWd</c>"] The maximum withdrawal amount in a single transaction
    /// </summary>
    [JsonPropertyName("maxWd")]
    public decimal? MaxWithdrawal { get; set; }

    /// <summary>
    /// ["<c>minDep</c>"] The minimum deposit amount in a single transaction
    /// </summary>
    [JsonPropertyName("minDep")]
    public decimal? MinDeposit { get; set; }

    /// <summary>
    /// ["<c>minDepArrivalConfirm</c>"] The minimum number of blockchain confirmations to acknowledge fund deposit.
    /// </summary>
    [JsonPropertyName("minDepArrivalConfirm")]
    public int? MinDepositConfirmations { get; set; }

    /// <summary>
    /// ["<c>minWdUnlockConfirm</c>"] The minimum number of blockchain confirmations required for withdrawal of a deposit
    /// </summary>
    [JsonPropertyName("minWdUnlockConfirm")]
    public int? MinDepositConfirmationsWithdraw { get; set; }

    /// <summary>
    /// ["<c>needTag</c>"] Whether tag/memo information is required for withdrawal
    /// </summary>
    [JsonPropertyName("needTag")]
    public bool NeedsTag { get; set; }

    /// <summary>
    /// ["<c>usedDepQuotaFixed</c>"] The used amount of fixed deposit quota, unit in USD
    /// </summary>
    [JsonPropertyName("usedDepQuotaFixed")]
    public decimal? UsedDepositQuota { get; set; }

    /// <summary>
    /// ["<c>usedWdQuota</c>"] The amount of currency withdrawal used in the past 24 hours, unit in USD
    /// </summary>
    [JsonPropertyName("usedWdQuota")]
    public decimal? UsedWithdrawalQuota { get; set; }

    /// <summary>
    /// ["<c>wdQuota</c>"] The withdrawal limit in the past 24 hours, unit in USD
    /// </summary>
    [JsonPropertyName("wdQuota")]
    public decimal? MaxWithdrawal24h { get; set; }

    /// <summary>
    /// ["<c>wdTickSz</c>"] The withdrawal precision, indicating the number of digits after the decimal point.
    /// </summary>
    [JsonPropertyName("wdTickSz")]
    public decimal? WithdrawalTickSize { get; set; }

    /// <summary>
    /// ["<c>burningFeeRate</c>"] Burning fee rate, e.g "0.05" represents "5%". Some currencies may charge combustion fees.The burning fee is deducted based on the withdrawal quantity
    /// </summary>
    [JsonPropertyName("burningFeeRate")]
    public decimal? BurningFeeRate { get; set; }

    /// <summary>
    /// ["<c>ctAddr</c>"] Contract address
    /// </summary>
    [JsonPropertyName("ctAddr")]
    public string? ContractAddress { get; set; }
    /// <summary>
    /// ["<c>depEstOpenTime</c>"] Estimated deposit open time
    /// </summary>
    [JsonPropertyName("depEstOpenTime")]
    public DateTime? EstimatedDepositOpenTime { get; set; }
    /// <summary>
    /// ["<c>wdEstOpenTime</c>"] Estimated withdrawal open time
    /// </summary>
    [JsonPropertyName("wdEstOpenTime")]
    public DateTime? EstimatedWithdrawalOpenTime { get; set; }
    /// <summary>
    /// ["<c>minInternal</c>"] Minimal internal tranfer quantity
    /// </summary>
    [JsonPropertyName("minInternal")]
    public decimal? MinInternalTransferQuantity { get; set; }
}
