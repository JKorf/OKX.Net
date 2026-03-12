using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Unmatched info type
/// </summary>
[JsonConverter(typeof(EnumConverter<UnmatchedInfoType>))]
public enum UnmatchedInfoType
{
    /// <summary>
    /// ["<c>asset_validation</c>"] Asset validation
    /// </summary>
    [Map("asset_validation")]
    AssetValidation,
    /// <summary>
    /// ["<c>pending_orders</c>"] Order book pending orders
    /// </summary>
    [Map("pending_orders")]
    PendingOrders,
    /// <summary>
    /// ["<c>pending_algos</c>"] Pending algo orders and trading bots, such as iceberg, recurring buy and twap
    /// </summary>
    [Map("pending_algos")]
    PendingAlgos,
    /// <summary>
    /// ["<c>isolated_margin</c>"] Isolated margin (quick margin and manual transfers
    /// </summary>
    [Map("isolated_margin")]
    IsolatedMargin,
    /// <summary>
    /// ["<c>isolated_contract</c>"] Isolated contract (manual transfers
    /// </summary>
    [Map("isolated_contract")]
    IsolatedContract,
    /// <summary>
    /// ["<c>contract_long_short</c>"] Contract positions in hedge mode
    /// </summary>
    [Map("contract_long_short")]
    ContractLongShort,
    /// <summary>
    /// ["<c>cross_margin</c>"] Cross margin positions
    /// </summary>
    [Map("cross_margin")]
    CrossMargin,
    /// <summary>
    /// ["<c>cross_option_buyer</c>"] Cross options buyer
    /// </summary>
    [Map("cross_option_buyer")]
    CrossOptionBuyer,
    /// <summary>
    /// ["<c>isolated_option</c>"] Isolated options (only applicable to spot mode
    /// </summary>
    [Map("isolated_option")]
    IsolatedOption,
    /// <summary>
    /// ["<c>growth_fund</c>"] Positions with trial funds
    /// </summary>
    [Map("growth_fund")]
    GrowthFund,
    /// <summary>
    /// ["<c>all_positions</c>"] All positions
    /// </summary>
    [Map("all_positions")]
    AllPositions,
    /// <summary>
    /// ["<c>spot_lead_copy_only_simple_single</c>"] Copy trader and customize lead trader can only use spot mode or spot and futures mode
    /// </summary>
    [Map("spot_lead_copy_only_simple_single")]
    SpotLeadCopyOnlySimpleSingle,
    /// <summary>
    /// ["<c>stop_spot_custom</c>"] Spot customize copy trading
    /// </summary>
    [Map("stop_spot_custom")]
    StopSpotCustom,
    /// <summary>
    /// ["<c>stop_futures_custom</c>"] Contract customize copy trading
    /// </summary>
    [Map("stop_futures_custom")]
    StopFuturesCustom,
    /// <summary>
    /// ["<c>lead_portfolio</c>"] Lead trader can not switch to portfolio margin mode
    /// </summary>
    [Map("lead_portfolio")]
    LeadPortfolio,
    /// <summary>
    /// ["<c>futures_smart_sync</c>"] You can not switch to spot mode when having smart contract sync
    /// </summary>
    [Map("futures_smart_sync")]
    FuturesSmartSync,
    /// <summary>
    /// ["<c>vip_fixed_loan</c>"] Vip loan
    /// </summary>
    [Map("vip_fixed_loan")]
    VipFixedLoan,
    /// <summary>
    /// ["<c>repay_borrowings</c>"] Borrowings
    /// </summary>
    [Map("repay_borrowings")]
    RepayBorrowings,
    /// <summary>
    /// ["<c>compliance_restriction</c>"] Due to compliance restrictions, margin trading services are unavailable
    /// </summary>
    [Map("compliance_restriction")]
    ComplianceRestriction,
    /// <summary>
    /// ["<c>compliance_kyc2</c>"] Due to compliance restrictions, margin trading services are unavailable. if you are not a resident of this region, please complete kyc2 identity verification.
    /// </summary>
    [Map("compliance_kyc2")]
    ComplianceKyc2,
}

