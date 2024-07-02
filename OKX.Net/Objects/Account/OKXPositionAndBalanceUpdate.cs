﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position and balance update
/// </summary>
public record OKXPositionAndBalanceUpdate
{
    /// <summary>
    /// Trigger event type
    /// </summary>
    [JsonPropertyName("eventType")]
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Balance data
    /// </summary>
    [JsonPropertyName("balData")]
    public IEnumerable<OKXBalanceUpdate> BalanceData { get; set; } = Array.Empty<OKXBalanceUpdate>();

    /// <summary>
    /// Position data
    /// </summary>
    [JsonPropertyName("posData")]
    public IEnumerable<OKXAccountPositionUpdate> PositionData { get; set; } = Array.Empty<OKXAccountPositionUpdate>();
    /// <summary>
    /// Trades data
    /// </summary>
    [JsonPropertyName("trades")]
    public IEnumerable<OKXTradeReference> TradeData { get; set; } = Array.Empty<OKXTradeReference>();
}

/// <summary>
/// Balance info
/// </summary>
public record OKXBalanceUpdate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Cash balance
    /// </summary>
    [JsonPropertyName("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonPropertyName("uTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// Position info
/// </summary>
public record OKXAccountPositionUpdate
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
    /// Position quantity
    /// </summary>
    [JsonPropertyName("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonPropertyName("avgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// Position asset
    /// </summary>
    [JsonPropertyName("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// Last trade ID
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string? TradeId { get; set; }

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
    /// Update time
    /// </summary>
    [JsonPropertyName("uTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// Trade reference
/// </summary>
public record OKXTradeReference
{
    /// <summary>
    /// Symbol name
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string TradeId { get; set; } = string.Empty;
}