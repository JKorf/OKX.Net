using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account config
/// </summary>
[SerializationModel]
public record OKXAccountConfiguration
{
    /// <summary>
    /// User id
    /// </summary>
    [JsonPropertyName("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Account level
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel AccountLevel { get; set; }

    /// <summary>
    /// Position mode
    /// </summary>
    [JsonPropertyName("posMode")]
    public PositionMode PositionMode { get; set; }

    /// <summary>
    /// Auto loan
    /// </summary>
    [JsonPropertyName("autoLoan")]
    public bool AutoLoan { get; set; }

    /// <summary>
    /// Greeks type
    /// </summary>
    [JsonPropertyName("greeksType")]
    public GreeksType GreeksType { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Level temporary
    /// </summary>
    [JsonPropertyName("levelTmp")]
    public string LevelTemporary { get; set; } = string.Empty;

    /// <summary>
    /// Account self trade prevention mode
    /// </summary>
    [JsonPropertyName("acctStpMode")]
    public SelfTradePreventionMode StpMode { get; set; }

    /// <summary>
    /// Contract isolated margin trading mode
    /// </summary>
    [JsonPropertyName("ctIsoMode")]
    public MarginTransferMode ContractIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Margin isolated trading mode
    /// </summary>

    [JsonPropertyName("mgnIsoMode")]
    public MarginTransferMode MarginIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Liquidation gear
    /// </summary>
    [JsonPropertyName("liquidationGear")]
    public string LiquidationGear { get; set; } = string.Empty;

    /// <summary>
    /// Spot offset type
    /// </summary>
    [JsonPropertyName("spotOffsetType")]
    public string SpotOffsetType { get; set; } = string.Empty;

    /// <summary>
    /// API key note
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Account role
    /// </summary>
    [JsonPropertyName("roleType")]

    public AccountRoleType RoleType { get; set; }

    /// <summary>
    /// Spot account role
    /// </summary>
    [JsonPropertyName("spotRoleType")]

    public AccountRoleType SpotRoleType { get; set; }

    /// <summary>
    /// Optional trading activation status
    /// </summary>
    [JsonPropertyName("opAuth")]
    public OptionalTradingStatus OpAuth { get; set; }

    /// <summary>
    /// KYC level
    /// </summary>
    [JsonPropertyName("kycLv")]
    public string KycLevel { get; set; } = string.Empty;

    /// <summary>
    /// IP addresses linked to the current API key
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// Permissions of the current API key
    /// </summary>
    [JsonPropertyName("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// Main user id
    /// </summary>
    [JsonPropertyName("mainUid")]
    public string MainUserId { get; set; } = string.Empty;

    /// <summary>
    /// Whether borrow is allowed or not in Spot mode
    /// </summary>
    [JsonPropertyName("enableSpotBorrow")]
    public bool SpotBorrowEnabled { get; set; }

    /// <summary>
    /// Whether auto-repay is allowed or not in Spot mode
    /// </summary>
    [JsonPropertyName("spotBorrowAutoRepay")]
    public bool SpotBorrowAutoRepay { get; set; }

    /// <summary>
    /// Account type
    /// </summary>
    [JsonPropertyName("type")]
    public UserAccountType AccountType { get; set; }

    /// <summary>
    /// Spot fee charging type
    /// </summary>
    [JsonPropertyName("feeType")]
    public FeeType FeeType { get; set; }

    /// <summary>
    /// The settlement asset for USD contracts
    /// </summary>
    [JsonPropertyName("settleCcy")]
    public string SettleAsset { get; set; } = string.Empty;

    /// <summary>
    /// Available settlement assets for USD contracts
    /// </summary>
    [JsonPropertyName("settleCcyList")]
    public string[] SettleAssetList { get; set; } = [];
}
