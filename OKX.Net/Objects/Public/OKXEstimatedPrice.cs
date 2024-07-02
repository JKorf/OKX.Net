﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Estimated price
/// </summary>
public record OKXEstimatedPrice
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
    /// Estimated price
    /// </summary>
    [JsonPropertyName("settlePx")]
    public decimal EstimatedPrice { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
