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
    [JsonProperty("eventType")]
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Balance data
    /// </summary>
    [JsonProperty("balData")]
    public IEnumerable<OKXBalanceUpdate> BalanceData { get; set; } = Array.Empty<OKXBalanceUpdate>();

    /// <summary>
    /// Position data
    /// </summary>
    [JsonProperty("posData")]
    public IEnumerable<OKXAccountPositionUpdate> PositionData { get; set; } = Array.Empty<OKXAccountPositionUpdate>();
    /// <summary>
    /// Trades data
    /// </summary>
    [JsonProperty("trades")]
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
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Cash balance
    /// </summary>
    [JsonProperty("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("uTime")]
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
    [JsonProperty("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Position quantity
    /// </summary>
    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// Position asset
    /// </summary>
    [JsonProperty("posCcy")]
    public string PositionAsset { get; set; } = string.Empty;

    /// <summary>
    /// Last trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public string? TradeId { get; set; }

    /// <summary>
    /// Position id
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("uTime")]
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
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// Trade id
    /// </summary>
    [JsonProperty("tradeId")]
    public string TradeId { get; set; } = string.Empty;
}