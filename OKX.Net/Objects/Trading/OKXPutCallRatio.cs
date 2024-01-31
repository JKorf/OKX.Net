﻿namespace OKX.Net.Objects.Trading;

/// <summary>
/// Put/Call ratio
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OKXPutCallRatio
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Open interest ratio
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenInterestRatio { get; set; }

    /// <summary>
    /// Volume ratio
    /// </summary>
    [ArrayProperty(2)]
    public decimal VolumeRatio { get; set; }
}
