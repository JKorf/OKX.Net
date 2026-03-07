using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Result of account switch pre-check
/// </summary>
[SerializationModel]
public record OKXAccountSwitchCheckResult
{
    /// <summary>
    /// ["<c>acctLv</c>"] Requested account mode
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel NewAccountMode { get; set; }
    /// <summary>
    /// ["<c>curAcctLv</c>"] Current account mode
    /// </summary>
    [JsonPropertyName("curAcctLv")]
    public AccountLevel CurrentAccountMode { get; set; }
    /// <summary>
    /// ["<c>mgnAft</c>"] Margin after
    /// </summary>
    [JsonPropertyName("mgnAft")]
    public OKXAccountSwitchMargin? MarginAfter { get; set; }
    /// <summary>
    /// ["<c>mgnBf</c>"] Margin before
    /// </summary>
    [JsonPropertyName("mgnBf")]
    public OKXAccountSwitchMargin? MarginBefore { get; set; }
    /// <summary>
    /// ["<c>riskOffsetType</c>"] Risk offset type
    /// </summary>
    [JsonPropertyName("riskOffsetType")]
    public RiskOffsetType? RiskOffsetType { get; set; }
    /// <summary>
    /// ["<c>sCode</c>"] Result of the check
    /// </summary>
    [JsonPropertyName("sCode")]
    public AccountSwitchCheckResult ResultStatus { get; set; }
    /// <summary>
    /// ["<c>posTierCheck</c>"] Position tier check
    /// </summary>
    [JsonPropertyName("posTierCheck")]
    public OKXAccountSwitchPosTier[]? PositionTierCheck { get; set; }
    /// <summary>
    /// ["<c>posList</c>"] Positions
    /// </summary>
    [JsonPropertyName("posList")]
    public OKXAccountSwitchPosition[]? Positions { get; set; }
    /// <summary>
    /// ["<c>unmatchedInfoCheck</c>"] Unmatched info check
    /// </summary>
    [JsonPropertyName("unmatchedInfoCheck")]
    public OKXAccountSwitchUnmatched[] UnmatchedInfoCheck { get; set; } = Array.Empty<OKXAccountSwitchUnmatched>();
}

/// <summary>
/// Unmatched info
/// </summary>
[SerializationModel]
public record OKXAccountSwitchUnmatched
{
    /// <summary>
    /// ["<c>totalAsset</c>"] Total asset
    /// </summary>
    [JsonPropertyName("totalAsset")]
    public decimal? TotalAsset { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Unmatched info type
    /// </summary>
    [JsonPropertyName("type")]
    public UnmatchedInfoType? Type { get; set; }
    /// <summary>
    /// ["<c>posList</c>"] Positions
    /// </summary>
    [JsonPropertyName("posList")]
    public OKXAccountSwitchPosition[]? Positions { get; set; }
}

/// <summary>
/// Position tier check info
/// </summary>
[SerializationModel]
public record OKXAccountSwitchPosTier
{
    /// <summary>
    /// ["<c>instFamily</c>"] Symbol family
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>instType</c>"] Symbol type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }
    /// <summary>
    /// ["<c>pos</c>"] Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal PositionQuantity { get; set; }
    /// <summary>
    /// ["<c>lever</c>"] Position leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>maxSz</c>"] Max size
    /// </summary>
    [JsonPropertyName("maxSz")]
    public decimal MaxSize { get; set; }
}

/// <summary>
/// Margin info
/// </summary>
[SerializationModel]
public record OKXAccountSwitchMargin
{
    /// <summary>
    /// ["<c>acctAvailEq</c>"] Account available equity in USD
    /// </summary>
    [JsonPropertyName("acctAvailEq")]
    public decimal availableEquity { get; set; }
    /// <summary>
    /// ["<c>mgnRatio</c>"] Margin ratio in USD
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal MarginRatio { get; set; }
}

/// <summary>
/// Position reference
/// </summary>
[SerializationModel]
public record OKXAccountSwitchPosition
{
    /// <summary>
    /// ["<c>posId</c>"] Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public string PositionId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }
}


