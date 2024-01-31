namespace OKX.Net.Objects.Account;

/// <summary>
/// Account balance
/// </summary>
public class OKXAccountBalance
{
    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Total equity
    /// </summary>
    [JsonProperty("totalEq")]
    public decimal TotalEquity { get; set; }

    /// <summary>
    /// Isolated margin equity
    /// </summary>
    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Adjusted equity
    /// </summary>
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Order frozen
    /// </summary>
    [JsonProperty("ordFroz")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Notional usd
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public IEnumerable<OKXAccountBalanceDetail> Details { get; set; } = Array.Empty<OKXAccountBalanceDetail>();
}

/// <summary>
/// Balance details
/// </summary>
public class OKXAccountBalanceDetail
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Equity
    /// </summary>
    [JsonProperty("eq")]
    public decimal? Equity { get; set; }

    /// <summary>
    /// Cash balance
    /// </summary>
    [JsonProperty("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// Isolated margin equity
    /// </summary>
    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Available equity
    /// </summary>
    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Discount equity
    /// </summary>
    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// Avilable balance
    /// </summary>
    [JsonProperty("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal? FrozenBalance { get; set; }

    /// <summary>
    /// Order frozen
    /// </summary>
    [JsonProperty("ordFrozen")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Unrealized profit and loss liabilities
    /// </summary>
    [JsonProperty("uplLiab")]
    public decimal? UnrealizedProfitAndLossLiabilities { get; set; }

    /// <summary>
    /// Isolated unrealized profit and loss
    /// </summary>
    [JsonProperty("isoUpl")]
    public decimal? IsolatedUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Cross liabilities
    /// </summary>
    [JsonProperty("crossLiab")]
    public decimal? CrossLiabilities { get; set; }

    /// <summary>
    /// Isolated liabilities
    /// </summary>
    [JsonProperty("isoLiab")]
    public decimal? IsolatedLiabilities { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Twap
    /// </summary>
    [JsonProperty("twap")]
    public decimal? Twap { get; set; }

    /// <summary>
    /// Maximum loan
    /// </summary>
    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// Usd equity
    /// </summary>
    [JsonProperty("eqUsd")]
    public decimal? UsdEquity { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("notionalLever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Strategy equity
    /// </summary>
    [JsonProperty("stgyEq")]
    public decimal? StrategyEquity { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonProperty("fixedBal")]
    public decimal? FixedBalance { get; set; }

    /// <summary>
    /// Spot in use amount
    /// </summary>
    [JsonProperty("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }

    /// <summary>
    /// Price index usd of the asset
    /// </summary>
    [JsonProperty("coinUsdPrice")]
    public decimal? AssetUsdPrice { get; set; }
}
