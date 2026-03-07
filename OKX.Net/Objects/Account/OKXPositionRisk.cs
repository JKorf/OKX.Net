using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position risk info
/// </summary>
[SerializationModel]
public record OKXPositionRisk
{
    /// <summary>
    /// ["<c>adjEq</c>"] Adjusted equity
    /// </summary>
    [JsonPropertyName("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>balData</c>"] Balance data
    /// </summary>
    [JsonPropertyName("balData")]
    public OKXAccountPositionRiskBalanceData[] BalanceData { get; set; } = Array.Empty<OKXAccountPositionRiskBalanceData>();

    /// <summary>
    /// ["<c>posData</c>"] Position data
    /// </summary>
    [JsonPropertyName("posData")]
    public OKXAccountPositionRiskPositionData[] PositionData { get; set; } = Array.Empty<OKXAccountPositionRiskPositionData>();
}

/// <summary>
/// Balance info
/// </summary>
[SerializationModel]
public record OKXAccountPositionRiskBalanceData
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>disEq</c>"] Discount equity
    /// </summary>
    [JsonPropertyName("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// ["<c>eq</c>"] Equity
    /// </summary>
    [JsonPropertyName("eq")]
    public decimal? Equity { get; set; }
}

/// <summary>
/// Position info
/// </summary>
[SerializationModel]
public record OKXAccountPositionRiskPositionData
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// ["<c>notionalCcy</c>"] Notional currency
    /// </summary>
    [JsonPropertyName("notionalCcy")]
    public decimal? NotionalCcy { get; set; }

    /// <summary>
    /// ["<c>notionalUsd</c>"] Notional usd
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// ["<c>pos</c>"] Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>posCcy</c>"] Position asset
    /// </summary>
    [JsonPropertyName("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>posId</c>"] Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>baseBal</c>"] Base asset balance
    /// </summary>
    [JsonPropertyName("baseBal")]
    public decimal? BaseBalance { get; set; }

    /// <summary>
    /// ["<c>quoteBal</c>"] Quote asset balance
    /// </summary>
    [JsonPropertyName("quoteBal")]
    public decimal? QuoteBalance { get; set; }
}
