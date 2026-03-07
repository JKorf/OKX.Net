using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account config
/// </summary>
[SerializationModel]
public record OKXAccountConfiguration
{
    /// <summary>
    /// ["<c>uid</c>"] User id
    /// </summary>
    [JsonPropertyName("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// ["<c>acctLv</c>"] Account level
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel AccountLevel { get; set; }

    /// <summary>
    /// ["<c>posMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("posMode")]
    public PositionMode PositionMode { get; set; }

    /// <summary>
    /// ["<c>autoLoan</c>"] Auto loan
    /// </summary>
    [JsonPropertyName("autoLoan")]
    public bool AutoLoan { get; set; }

    /// <summary>
    /// ["<c>greeksType</c>"] Greeks type
    /// </summary>
    [JsonPropertyName("greeksType")]
    public GreeksType GreeksType { get; set; }

    /// <summary>
    /// ["<c>level</c>"] Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>levelTmp</c>"] Level temporary
    /// </summary>
    [JsonPropertyName("levelTmp")]
    public string LevelTemporary { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>acctStpMode</c>"] Account self trade prevention mode
    /// </summary>
    [JsonPropertyName("acctStpMode")]
    public SelfTradePreventionMode StpMode { get; set; }

    /// <summary>
    /// ["<c>ctIsoMode</c>"] Contract isolated margin trading mode
    /// </summary>
    [JsonPropertyName("ctIsoMode")]
    public MarginTransferMode ContractIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// ["<c>mgnIsoMode</c>"] Margin isolated trading mode
    /// </summary>

    [JsonPropertyName("mgnIsoMode")]
    public MarginTransferMode MarginIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// ["<c>liquidationGear</c>"] Liquidation gear
    /// </summary>
    [JsonPropertyName("liquidationGear")]
    public string LiquidationGear { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>spotOffsetType</c>"] Spot offset type
    /// </summary>
    [JsonPropertyName("spotOffsetType")]
    public string SpotOffsetType { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>label</c>"] API key note
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>roleType</c>"] Account role
    /// </summary>
    [JsonPropertyName("roleType")]

    public AccountRoleType RoleType { get; set; }

    /// <summary>
    /// ["<c>spotRoleType</c>"] Spot account role
    /// </summary>
    [JsonPropertyName("spotRoleType")]

    public AccountRoleType SpotRoleType { get; set; }

    /// <summary>
    /// ["<c>opAuth</c>"] Optional trading activation status
    /// </summary>
    [JsonPropertyName("opAuth")]
    public OptionalTradingStatus OpAuth { get; set; }

    /// <summary>
    /// ["<c>kycLv</c>"] KYC level
    /// </summary>
    [JsonPropertyName("kycLv")]
    public string KycLevel { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ip</c>"] IP addresses linked to the current API key
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>perm</c>"] Permissions of the current API key
    /// </summary>
    [JsonPropertyName("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>mainUid</c>"] Main user id
    /// </summary>
    [JsonPropertyName("mainUid")]
    public string MainUserId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>enableSpotBorrow</c>"] Whether borrow is allowed or not in Spot mode
    /// </summary>
    [JsonPropertyName("enableSpotBorrow")]
    public bool SpotBorrowEnabled { get; set; }

    /// <summary>
    /// ["<c>spotBorrowAutoRepay</c>"] Whether auto-repay is allowed or not in Spot mode
    /// </summary>
    [JsonPropertyName("spotBorrowAutoRepay")]
    public bool SpotBorrowAutoRepay { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Account type
    /// </summary>
    [JsonPropertyName("type")]
    public UserAccountType AccountType { get; set; }

    /// <summary>
    /// ["<c>feeType</c>"] Spot fee charging type
    /// </summary>
    [JsonPropertyName("feeType")]
    public FeeType FeeType { get; set; }

    /// <summary>
    /// ["<c>settleCcy</c>"] The settlement asset for USD contracts
    /// </summary>
    [JsonPropertyName("settleCcy")]
    public string SettleAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>settleCcyList</c>"] Available settlement assets for USD contracts
    /// </summary>
    [JsonPropertyName("settleCcyList")]
    public string[] SettleAssetList { get; set; } = [];
}
