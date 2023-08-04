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
}
