﻿namespace OKX.Net.Objects.Trading;

/// <summary>
/// Interest volume
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OKXInterestVolume
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Open interest
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenInterest { get; set; }

    /// <summary>
    /// Volume
    /// </summary>
    [ArrayProperty(2)]
    public decimal Volume { get; set; }
}
