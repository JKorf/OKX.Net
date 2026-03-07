using OKX.Net.Enums;
namespace OKX.Net.Objects.Account;

/// <summary>
/// Account balance
/// </summary>
[SerializationModel]
public record OKXAccountBalance
{
    /// <summary>
    /// ["<c>uTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// ["<c>totalEq</c>"] Total equity
    /// </summary>
    [JsonPropertyName("totalEq")]
    public decimal TotalEquity { get; set; }

    /// <summary>
    /// ["<c>availEq</c>"] Available equity
    /// </summary>
    [JsonPropertyName("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// ["<c>isoEq</c>"] Isolated margin equity
    /// </summary>
    [JsonPropertyName("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// ["<c>adjEq</c>"] Adjusted equity
    /// </summary>
    [JsonPropertyName("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// ["<c>borrowFroz</c>"] Frozen borrow quantity
    /// </summary>
    [JsonPropertyName("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// ["<c>ordFroz</c>"] Order frozen
    /// </summary>
    [JsonPropertyName("ordFroz")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// ["<c>imr</c>"] Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>mmr</c>"] Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>mgnRatio</c>"] Margin ratio
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// ["<c>notionalUsd</c>"] Notional usd
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// ["<c>upl</c>"] Cross-margin info of unrealized profit and loss at the account level in USD
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal? UnrealizedPnl { get; set; }

    /// <summary>
    /// ["<c>details</c>"] Details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXAccountBalanceDetail[] Details { get; set; } = Array.Empty<OKXAccountBalanceDetail>();
}

/// <summary>
/// Balance details
/// </summary>
[SerializationModel]
public record OKXAccountBalanceDetail
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>uTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// ["<c>eq</c>"] Equity
    /// </summary>
    [JsonPropertyName("eq")]
    public decimal? Equity { get; set; }

    /// <summary>
    /// ["<c>cashBal</c>"] Cash balance
    /// </summary>
    [JsonPropertyName("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// ["<c>isoEq</c>"] Isolated margin equity
    /// </summary>
    [JsonPropertyName("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// ["<c>availEq</c>"] Available equity
    /// </summary>
    [JsonPropertyName("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// ["<c>disEq</c>"] Discount equity
    /// </summary>
    [JsonPropertyName("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// ["<c>availBal</c>"] Available balance
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// ["<c>frozenBal</c>"] Frozen balance
    /// </summary>
    [JsonPropertyName("frozenBal")]
    public decimal? FrozenBalance { get; set; }

    /// <summary>
    /// ["<c>ordFrozen</c>"] Order frozen
    /// </summary>
    [JsonPropertyName("ordFrozen")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// ["<c>borrowFroz</c>"] Frozen borrow quantity
    /// </summary>
    [JsonPropertyName("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// ["<c>imr</c>"] Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>mmr</c>"] Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>rewardBal</c>"] Trial fund balance
    /// </summary>
    [JsonPropertyName("rewardBal")]
    public decimal? RewardBalance { get; set; }

    /// <summary>
    /// ["<c>liab</c>"] Liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// ["<c>upl</c>"] Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>uplLiab</c>"] Unrealized profit and loss liabilities
    /// </summary>
    [JsonPropertyName("uplLiab")]
    public decimal? UnrealizedProfitAndLossLiabilities { get; set; }

    /// <summary>
    /// ["<c>isoUpl</c>"] Isolated unrealized profit and loss
    /// </summary>
    [JsonPropertyName("isoUpl")]
    public decimal? IsolatedUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>crossLiab</c>"] Cross liabilities
    /// </summary>
    [JsonPropertyName("crossLiab")]
    public decimal? CrossLiabilities { get; set; }

    /// <summary>
    /// ["<c>isoLiab</c>"] Isolated liabilities
    /// </summary>
    [JsonPropertyName("isoLiab")]
    public decimal? IsolatedLiabilities { get; set; }

    /// <summary>
    /// ["<c>mgnRatio</c>"] Margin ratio
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// ["<c>interest</c>"] Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// ["<c>twap</c>"] Twap
    /// </summary>
    [JsonPropertyName("twap")]
    public decimal? Twap { get; set; }

    /// <summary>
    /// ["<c>maxLoan</c>"] Maximum loan
    /// </summary>
    [JsonPropertyName("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// ["<c>eqUsd</c>"] Usd equity
    /// </summary>
    [JsonPropertyName("eqUsd")]
    public decimal? UsdEquity { get; set; }

    /// <summary>
    /// ["<c>notionalLever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("notionalLever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// ["<c>stgyEq</c>"] Strategy equity
    /// </summary>
    [JsonPropertyName("stgyEq")]
    public decimal? StrategyEquity { get; set; }

    /// <summary>
    /// ["<c>fixedBal</c>"] Frozen balance
    /// </summary>
    [JsonPropertyName("fixedBal")]
    public decimal? FixedBalance { get; set; }

    /// <summary>
    /// ["<c>spotInUseAmt</c>"] Spot in use amount
    /// </summary>
    [JsonPropertyName("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }

    /// <summary>
    /// ["<c>clSpotInUseAmt</c>"] User-defined spot risk offset amount
    /// </summary>
    [JsonPropertyName("clSpotInUseAmt")]
    public decimal? ClSpotInUseAmount { get; set; }

    /// <summary>
    /// ["<c>maxSpotInUseAmt</c>"] Max possible spot risk offset amount
    /// </summary>
    [JsonPropertyName("maxSpotInUseAmt")]
    public decimal? MaxSpotInUseAmount { get; set; }

    /// <summary>
    /// ["<c>spotIsoBal</c>"] Spot isolated balance
    /// </summary>
    [JsonPropertyName("spotIsoBal")]
    public decimal? SpotIsolatedBalance { get; set; }

    /// <summary>
    /// ["<c>coinUsdPrice</c>"] Price index usd of the asset
    /// </summary>
    [JsonPropertyName("coinUsdPrice")]
    public decimal? AssetUsdPrice { get; set; }

    /// <summary>
    /// ["<c>spotBal</c>"] Spot balance. The unit is currency, e.g. BTC
    /// </summary>
    [JsonPropertyName("spotBal")]
    public decimal? SpotBalance { get; set; }

    /// <summary>
    /// ["<c>openAvgPx</c>"] Spot average cost price. The unit is USD
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal? SpotAverageOpenPrice { get; set; }

    /// <summary>
    /// ["<c>accAvgPx</c>"] Spot accumulated cost price. The unit is USD
    /// </summary>
    [JsonPropertyName("accAvgPx")]
    public decimal? SpotAccumulatedCostPrice { get; set; }

    /// <summary>
    /// ["<c>spotUpl</c>"] Spot unrealized profit and loss. The unit is USD
    /// </summary>
    [JsonPropertyName("spotUpl")]
    public decimal? SpotUnrealizedPnl { get; set; }

    /// <summary>
    /// ["<c>spotUplRatio</c>"] Spot unrealized profit and loss ratio
    /// </summary>
    [JsonPropertyName("spotUplRatio")]
    public decimal? SpotUnrealizedPnlRatio { get; set; }

    /// <summary>
    /// ["<c>totalPnl</c>"] Spot accumulated profit and loss. The unit is USD
    /// </summary>
    [JsonPropertyName("totalPnl")]
    public decimal? SpotPnl { get; set; }

    /// <summary>
    /// ["<c>totalPnlRatio</c>"] Spot accumulated profit and loss ratio
    /// </summary>
    [JsonPropertyName("totalPnlRatio")]
    public decimal? SpotPnlRatio { get; set; }

    /// <summary>
    /// ["<c>collateralEnabled</c>"] Collateral enabled for multi-currency margin
    /// </summary>
    [JsonPropertyName("collateralEnabled")]
    public bool CollateralEnabled { get; set; }
    /// <summary>
    /// ["<c>colRes</c>"] Collateral restriction status
    /// </summary>
    [JsonPropertyName("colRes")]
    public CollateralRestrictionStatus CollateralRestrictionStatus { get; set; }
}
