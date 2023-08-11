using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account config
/// </summary>
public class OKXAccountConfiguration
{
    /// <summary>
    /// User id
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv"), JsonConverter(typeof(AccountLevelConverter))]
    public OKXAccountLevel AccountLevel { get; set; }

    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OKXPositionMode PositionMode { get; set; }

    /// <summary>
    /// Auto loan
    /// </summary>
    [JsonProperty("autoLoan"), JsonConverter(typeof(OKXBooleanConverter))]
    public bool AutoLoan { get; set; }

    /// <summary>
    /// Greeks type
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OKXGreeksType GreeksType { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Level temporary
    /// </summary>
    [JsonProperty("levelTmp")]
    public string LevelTemporary { get; set; } = string.Empty;

    /// <summary>
    /// Contract isolated margin trading mode
    /// </summary>
    [JsonProperty("ctIsoMode"), JsonConverter(typeof(MarginTransferModeConverter))]
    public OKXMarginTransferMode ContractIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Margin isolated trading mode
    /// </summary>

    [JsonProperty("mgnIsoMode"), JsonConverter(typeof(MarginTransferModeConverter))]
    public OKXMarginTransferMode MarginIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Liquidation gear
    /// </summary>
    [JsonProperty("liquidationGear")]
    public string LiquidationGear { get; set; } = string.Empty;

    /// <summary>
    /// Spot offset type
    /// </summary>
    [JsonProperty("spotOffsetType")]
    public string SpotOffsetType { get; set; } = string.Empty;

    /// <summary>
    /// API key note
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Account role
    /// </summary>
    [JsonProperty("roleType")]
    [JsonConverter(typeof(EnumConverter))]
    public OKXAccountRoleType RoleType { get; set; }

    /// <summary>
    /// Optional trading activation status
    /// </summary>
    [JsonProperty("opAuth")]
    [JsonConverter (typeof(EnumConverter))]
    public OKXOptionalTradingStatus OpAuth { get; set; }

    /// <summary>
    /// KYC level
    /// </summary>
    [JsonProperty("kycLv")]
    public string KycLevel { get; set; } = string.Empty;

    /// <summary>
    /// IP addresses linked to the current API key
    /// </summary>
    [JsonProperty("ip")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// Permissions of the current API key
    /// </summary>
    [JsonProperty("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// Main user id
    /// </summary>
    [JsonProperty("mainUid")]
    public string MainUserId { get; set; } = string.Empty;
}
