﻿namespace OKX.Net.Objects.Funding;

/// <summary>
/// Asset info
/// </summary>
public record OKXAsset
{
    /// <summary>
    /// Asset name
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// Deposit allowed
    /// </summary>
    [JsonPropertyName("canDep")]
    public bool AllowDeposit { get; set; }

    /// <summary>
    /// Withdrawal allowed
    /// </summary>
    [JsonPropertyName("canWd")]
    public bool AllowWithdrawal { get; set; }

    /// <summary>
    /// Can internal transfer
    /// </summary>
    [JsonPropertyName("canInternal")]
    public bool AllowInternalTransfer { get; set; }

    /// <summary>
    /// Minimal withdrawal quantity
    /// </summary>
    [JsonPropertyName("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// Minimum withdrawal fee
    /// </summary>
    [JsonPropertyName("minFee")]
    public decimal MinimumWithdrawalFee { get; set; }

    /// <summary>
    /// Maximum withdrawal fee
    /// </summary>
    [JsonPropertyName("maxFee")]
    public decimal MaximumWithdrawalFee { get; set; }

    /// <summary>
    /// The fixed deposit limit, unit in USD
    /// </summary>
    [JsonPropertyName("depQuotaFixed")]
    public decimal? MaxDeposit { get; set; }

    /// <summary>
    /// The layer2 network daily deposit limit
    /// </summary>
    [JsonPropertyName("depQuoteDailyLayer2")]
    public decimal? MaxDepositLayer2 { get; set; }

    /// <summary>
    /// Logo link
    /// </summary>
    [JsonPropertyName("logoLink")]
    public string LogoLink { get; set; } = string.Empty;

    /// <summary>
    /// Main net
    /// </summary>
    [JsonPropertyName("mainNet")]
    public bool IsMainNet { get; set; }

    /// <summary>
    /// The maximum withdrawal fee for contract address
    /// </summary>
    [JsonPropertyName("maxFeeForCtAddr")]
    public decimal? MaxWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// The minimum withdrawal fee for contract address
    /// </summary>
    [JsonPropertyName("minFeeForCtAddr")]
    public decimal? MinWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// The maximum withdrawal amount in a single transaction
    /// </summary>
    [JsonPropertyName("maxWd")]
    public decimal? MaxWithdrawal { get; set; }

    /// <summary>
    /// The minimum deposit amount in a single transaction
    /// </summary>
    [JsonPropertyName("minDep")]
    public decimal? MinDeposit { get; set; }

    /// <summary>
    /// The minimum number of blockchain confirmations to acknowledge fund deposit.
    /// </summary>
    [JsonPropertyName("minDepArrivalConfirm")]
    public int? MinDepositConfirmations { get; set; }

    /// <summary>
    /// The minimum number of blockchain confirmations required for withdrawal of a deposit
    /// </summary>
    [JsonPropertyName("minWdUnlockConfirm")]
    public int? MinDepositConfirmationsWithdraw { get; set; }

    /// <summary>
    /// Whether tag/memo information is required for withdrawal
    /// </summary>
    [JsonPropertyName("needTag")]
    public bool NeedsTag { get; set; }

    /// <summary>
    /// The used amount of fixed deposit quota, unit in USD
    /// </summary>
    [JsonPropertyName("usedDepQuotaFixed")]
    public decimal? UsedDepositQuota { get; set; }

    /// <summary>
    /// The amount of currency withdrawal used in the past 24 hours, unit in USD
    /// </summary>
    [JsonPropertyName("usedWdQuota")]
    public decimal? UsedWithdrawalQuota { get; set; }

    /// <summary>
    /// The withdrawal limit in the past 24 hours, unit in USD
    /// </summary>
    [JsonPropertyName("wdQuota")]
    public decimal? MaxWithdrawal24h { get; set; }

    /// <summary>
    /// The withdrawal precision, indicating the number of digits after the decimal point.
    /// </summary>
    [JsonPropertyName("wdTickSz")]
    public decimal? WithdrawalTickSize { get; set; }
}
