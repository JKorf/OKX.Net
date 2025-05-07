using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Result of account switch pre-check
/// </summary>
[SerializationModel]
public record OKXAccountSwitchCheckResult
{
    /// <summary>
    /// Requested account mode
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel NewAccountMode { get; set; }
    /// <summary>
    /// Current account mode
    /// </summary>
    [JsonPropertyName("curAcctLv")]
    public AccountLevel CurrentAccountMode { get; set; }
    /// <summary>
    /// Margin after
    /// </summary>
    [JsonPropertyName("mgnAft")]
    public OKXAccountSwitchMargin? MarginAfter { get; set; }
    /// <summary>
    /// Margin before
    /// </summary>
    [JsonPropertyName("mgnBf")]
    public OKXAccountSwitchMargin? MarginBefore { get; set; }
    /// <summary>
    /// Risk offset type
    /// </summary>
    [JsonPropertyName("riskOffsetType")]
    public RiskOffsetType? RiskOffsetType { get; set; }
    /// <summary>
    /// Result of the check
    /// </summary>
    [JsonPropertyName("sCode")]
    public AccountSwitchCheckResult ResultStatus { get; set; }
    /// <summary>
    /// Position tier check
    /// </summary>
    [JsonPropertyName("posTierCheck")]
    public OKXAccountSwitchPosTier[]? PositionTierCheck { get; set; }
    /// <summary>
    /// Positions
    /// </summary>
    [JsonPropertyName("posList")]
    public OKXAccountSwitchPosition[]? Positions { get; set; }
    /// <summary>
    /// Unmatched info check
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
    /// Total asset
    /// </summary>
    [JsonPropertyName("totalAsset")]
    public decimal? TotalAsset { get; set; }
    /// <summary>
    /// Unmatched info type
    /// </summary>
    [JsonPropertyName("type")]
    public UnmatchedInfoType? Type { get; set; }
    /// <summary>
    /// Positions
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
    /// Symbol family
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;
    /// <summary>
    /// Symbol type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }
    /// <summary>
    /// Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal PositionQuantity { get; set; }
    /// <summary>
    /// Position leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// Max size
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
    /// Account available equity in USD
    /// </summary>
    [JsonPropertyName("acctAvailEq")]
    public decimal availableEquity { get; set; }
    /// <summary>
    /// Margin ratio in USD
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
    /// Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public string PositionId { get; set; } = string.Empty;
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }
}


