using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Unmatched info type
/// </summary>
[JsonConverter(typeof(EnumConverter<UnmatchedInfoType>))]
public enum UnmatchedInfoType
{
    /// <summary>
    /// Asset validation
    /// </summary>
    [Map("asset_validation")]
    AssetValidation,
    /// <summary>
    /// Order book pending orders
    /// </summary>
    [Map("pending_orders")]
    PendingOrders,
    /// <summary>
    /// Pending algo orders and trading bots, such as iceberg, recurring buy and twap
    /// </summary>
    [Map("pending_algos")]
    PendingAlgos,
    /// <summary>
    /// Isolated margin (quick margin and manual transfers
    /// </summary>
    [Map("isolated_margin")]
    IsolatedMargin,
    /// <summary>
    /// Isolated contract (manual transfers
    /// </summary>
    [Map("isolated_contract")]
    IsolatedContract,
    /// <summary>
    /// Contract positions in hedge mode
    /// </summary>
    [Map("contract_long_short")]
    ContractLongShort,
    /// <summary>
    /// Cross margin positions
    /// </summary>
    [Map("cross_margin")]
    CrossMargin,
    /// <summary>
    /// Cross options buyer
    /// </summary>
    [Map("cross_option_buyer")]
    CrossOptionBuyer,
    /// <summary>
    /// Isolated options (only applicable to spot mode
    /// </summary>
    [Map("isolated_option")]
    IsolatedOption,
    /// <summary>
    /// Positions with trial funds
    /// </summary>
    [Map("growth_fund")]
    GrowthFund,
    /// <summary>
    /// All positions
    /// </summary>
    [Map("all_positions")]
    AllPositions,
    /// <summary>
    /// Copy trader and customize lead trader can only use spot mode or spot and futures mode
    /// </summary>
    [Map("spot_lead_copy_only_simple_single")]
    SpotLeadCopyOnlySimpleSingle,
    /// <summary>
    /// Spot customize copy trading
    /// </summary>
    [Map("stop_spot_custom")]
    StopSpotCustom,
    /// <summary>
    /// Contract customize copy trading
    /// </summary>
    [Map("stop_futures_custom")]
    StopFuturesCustom,
    /// <summary>
    /// Lead trader can not switch to portfolio margin mode
    /// </summary>
    [Map("lead_portfolio")]
    LeadPortfolio,
    /// <summary>
    /// You can not switch to spot mode when having smart contract sync
    /// </summary>
    [Map("futures_smart_sync")]
    FuturesSmartSync,
    /// <summary>
    /// Vip loan
    /// </summary>
    [Map("vip_fixed_loan")]
    VipFixedLoan,
    /// <summary>
    /// Borrowings
    /// </summary>
    [Map("repay_borrowings")]
    RepayBorrowings,
    /// <summary>
    /// Due to compliance restrictions, margin trading services are unavailable
    /// </summary>
    [Map("compliance_restriction")]
    ComplianceRestriction,
    /// <summary>
    /// Due to compliance restrictions, margin trading services are unavailable. if you are not a resident of this region, please complete kyc2 identity verification.
    /// </summary>
    [Map("compliance_kyc2")]
    ComplianceKyc2,
}

