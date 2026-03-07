using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position and balance update
/// </summary>
[SerializationModel]
public record OKXPositionAndBalanceUpdate
{
    /// <summary>
    /// ["<c>eventType</c>"] Trigger event type
    /// </summary>
    [JsonPropertyName("eventType")]
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>pTime</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>balData</c>"] Balance data
    /// </summary>
    [JsonPropertyName("balData")]
    public OKXBalanceUpdate[] BalanceData { get; set; } = Array.Empty<OKXBalanceUpdate>();

    /// <summary>
    /// ["<c>posData</c>"] Position data
    /// </summary>
    [JsonPropertyName("posData")]
    public OKXAccountPositionUpdate[] PositionData { get; set; } = Array.Empty<OKXAccountPositionUpdate>();
    /// <summary>
    /// ["<c>trades</c>"] Trades data
    /// </summary>
    [JsonPropertyName("trades")]
    public OKXTradeReference[] TradeData { get; set; } = Array.Empty<OKXTradeReference>();
}

/// <summary>
/// Balance info
/// </summary>
[SerializationModel]
public record OKXBalanceUpdate
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>cashBal</c>"] Cash balance
    /// </summary>
    [JsonPropertyName("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// ["<c>uTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("uTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// Position info
/// </summary>
[SerializationModel]
public record OKXAccountPositionUpdate
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
    /// ["<c>pos</c>"] Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>avgPx</c>"] Average open price
    /// </summary>
    [JsonPropertyName("avgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// ["<c>posCcy</c>"] Position asset
    /// </summary>
    [JsonPropertyName("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>tradeId</c>"] Last trade ID
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string? TradeId { get; set; }

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
    /// ["<c>uTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("uTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// Trade reference
/// </summary>
[SerializationModel]
public record OKXTradeReference
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol name
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>tradeId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string TradeId { get; set; } = string.Empty;
}
