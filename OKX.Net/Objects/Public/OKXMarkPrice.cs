﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Mark price
/// </summary>
public record OKXMarkPrice
{
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
    /// Mark price
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
