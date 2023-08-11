using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position risk info
/// </summary>
public class OKXPositionRisk
{
    /// <summary>
    /// Adjusted equity
    /// </summary>
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Balance data
    /// </summary>
    [JsonProperty("balData")]
    public IEnumerable<OKXAccountPositionRiskBalanceData> BalanceData { get; set; } = Array.Empty<OKXAccountPositionRiskBalanceData>();

    /// <summary>
    /// Position data
    /// </summary>
    [JsonProperty("posData")]
    public IEnumerable<OKXAccountPositionRiskPositionData> PositionData { get; set; } = Array.Empty<OKXAccountPositionRiskPositionData>();
}

/// <summary>
/// Balance info
/// </summary>
public class OKXAccountPositionRiskBalanceData
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Discount equity
    /// </summary>
    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// Equity
    /// </summary>
    [JsonProperty("eq")]
    public decimal? Equity { get; set; }
}

/// <summary>
/// Position info
/// </summary>
public class OKXAccountPositionRiskPositionData
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OKXMarginMode MarginMode { get; set; }

    /// <summary>
    /// Notional currency
    /// </summary>
    [JsonProperty("notionalCcy")]
    public decimal? NotionalCcy { get; set; }

    /// <summary>
    /// Notional usd
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Position quantity
    /// </summary>
    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Position asset
    /// </summary>
    [JsonProperty("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// Position id
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OKXPositionSide PositionSide { get; set; }

    /// <summary>
    /// Base asset balance
    /// </summary>
    [JsonProperty("baseBal")]
    public decimal? BaseBalance { get; set; }

    /// <summary>
    /// Quote asset balance
    /// </summary>
    [JsonProperty("quoteBal")]
    public decimal? QuoteBalance { get; set; }
}
