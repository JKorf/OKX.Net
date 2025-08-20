using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
namespace OKX.Net.Objects.Account;

/// <summary>
/// Account balance
/// </summary>
[SerializationModel]
public record OKXAccountBalance
{
    /// <summary>
    /// Update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Total equity
    /// </summary>
    [JsonPropertyName("totalEq")]
    public decimal TotalEquity { get; set; }

    /// <summary>
    /// Available equity
    /// </summary>
    [JsonPropertyName("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Isolated margin equity
    /// </summary>
    [JsonPropertyName("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Adjusted equity
    /// </summary>
    [JsonPropertyName("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Frozen borrow quantity
    /// </summary>
    [JsonPropertyName("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Order frozen
    /// </summary>
    [JsonPropertyName("ordFroz")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Notional usd
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Cross-margin info of unrealized profit and loss at the account level in USD
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal? UnrealizedPnl { get; set; }

    /// <summary>
    /// Details
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
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Equity
    /// </summary>
    [JsonPropertyName("eq")]
    public decimal? Equity { get; set; }

    /// <summary>
    /// Cash balance
    /// </summary>
    [JsonPropertyName("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// Isolated margin equity
    /// </summary>
    [JsonPropertyName("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Available equity
    /// </summary>
    [JsonPropertyName("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Discount equity
    /// </summary>
    [JsonPropertyName("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonPropertyName("frozenBal")]
    public decimal? FrozenBalance { get; set; }

    /// <summary>
    /// Order frozen
    /// </summary>
    [JsonPropertyName("ordFrozen")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Frozen borrow quantity
    /// </summary>
    [JsonPropertyName("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Trial fund balance
    /// </summary>
    [JsonPropertyName("rewardBal")]
    public decimal? RewardBalance { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Unrealized profit and loss liabilities
    /// </summary>
    [JsonPropertyName("uplLiab")]
    public decimal? UnrealizedProfitAndLossLiabilities { get; set; }

    /// <summary>
    /// Isolated unrealized profit and loss
    /// </summary>
    [JsonPropertyName("isoUpl")]
    public decimal? IsolatedUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Cross liabilities
    /// </summary>
    [JsonPropertyName("crossLiab")]
    public decimal? CrossLiabilities { get; set; }

    /// <summary>
    /// Isolated liabilities
    /// </summary>
    [JsonPropertyName("isoLiab")]
    public decimal? IsolatedLiabilities { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Twap
    /// </summary>
    [JsonPropertyName("twap")]
    public decimal? Twap { get; set; }

    /// <summary>
    /// Maximum loan
    /// </summary>
    [JsonPropertyName("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// Usd equity
    /// </summary>
    [JsonPropertyName("eqUsd")]
    public decimal? UsdEquity { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("notionalLever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Strategy equity
    /// </summary>
    [JsonPropertyName("stgyEq")]
    public decimal? StrategyEquity { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonPropertyName("fixedBal")]
    public decimal? FixedBalance { get; set; }

    /// <summary>
    /// Spot in use amount
    /// </summary>
    [JsonPropertyName("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }

    /// <summary>
    /// User-defined spot risk offset amount
    /// </summary>
    [JsonPropertyName("clSpotInUseAmt")]
    public decimal? ClSpotInUseAmount { get; set; }

    /// <summary>
    /// Max possible spot risk offset amount
    /// </summary>
    [JsonPropertyName("maxSpotInUseAmt")]
    public decimal? MaxSpotInUseAmount { get; set; }

    /// <summary>
    /// Spot isolated balance
    /// </summary>
    [JsonPropertyName("spotIsoBal")]
    public decimal? SpotIsolatedBalance { get; set; }

    /// <summary>
    /// Price index usd of the asset
    /// </summary>
    [JsonPropertyName("coinUsdPrice")]
    public decimal? AssetUsdPrice { get; set; }

    /// <summary>
    /// Spot balance. The unit is currency, e.g. BTC
    /// </summary>
    [JsonPropertyName("spotBal")]
    public decimal? SpotBalance { get; set; }

    /// <summary>
    /// Spot average cost price. The unit is USD
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal? SpotAverageOpenPrice { get; set; }

    /// <summary>
    /// Spot accumulated cost price. The unit is USD
    /// </summary>
    [JsonPropertyName("accAvgPx")]
    public decimal? SpotAccumulatedCostPrice { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss. The unit is USD
    /// </summary>
    [JsonPropertyName("spotUpl")]
    public decimal? SpotUnrealizedPnl { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss ratio
    /// </summary>
    [JsonPropertyName("spotUplRatio")]
    public decimal? SpotUnrealizedPnlRatio { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss. The unit is USD
    /// </summary>
    [JsonPropertyName("totalPnl")]
    public decimal? SpotPnl { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss ratio
    /// </summary>
    [JsonPropertyName("totalPnlRatio")]
    public decimal? SpotPnlRatio { get; set; }

    /// <summary>
    /// Collateral enabled for multi-currency margin
    /// </summary>
    [JsonPropertyName("collateralEnabled")]
    public bool CollateralEnabled { get; set; }
    /// <summary>
    /// Collateral restriction status
    /// </summary>
    [JsonPropertyName("colRes")]
    public CollateralRestrictionStatus CollateralRestrictionStatus { get; set; }
}
