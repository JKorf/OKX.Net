using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position risk info
/// </summary>
public record OKXPositionRisk
{
    /// <summary>
    /// Adjusted equity
    /// </summary>
    [JsonPropertyName("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Balance data
    /// </summary>
    [JsonPropertyName("balData")]
    public IEnumerable<OKXAccountPositionRiskBalanceData> BalanceData { get; set; } = Array.Empty<OKXAccountPositionRiskBalanceData>();

    /// <summary>
    /// Position data
    /// </summary>
    [JsonPropertyName("posData")]
    public IEnumerable<OKXAccountPositionRiskPositionData> PositionData { get; set; } = Array.Empty<OKXAccountPositionRiskPositionData>();
}

/// <summary>
/// Balance info
/// </summary>
public record OKXAccountPositionRiskBalanceData
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Discount equity
    /// </summary>
    [JsonPropertyName("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// Equity
    /// </summary>
    [JsonPropertyName("eq")]
    public decimal? Equity { get; set; }
}

/// <summary>
/// Position info
/// </summary>
public record OKXAccountPositionRiskPositionData
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Notional currency
    /// </summary>
    [JsonPropertyName("notionalCcy")]
    public decimal? NotionalCcy { get; set; }

    /// <summary>
    /// Notional usd
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Position asset
    /// </summary>
    [JsonPropertyName("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Base asset balance
    /// </summary>
    [JsonPropertyName("baseBal")]
    public decimal? BaseBalance { get; set; }

    /// <summary>
    /// Quote asset balance
    /// </summary>
    [JsonPropertyName("quoteBal")]
    public decimal? QuoteBalance { get; set; }
}
