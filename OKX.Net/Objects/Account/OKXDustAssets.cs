using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Dust assets
/// </summary>
public record OKXDustAssets
{
    /// <summary>
    /// Convertable assets
    /// </summary>
    [JsonPropertyName("fromData")]
    public IEnumerable<OKXDustAsset> Convertable { get; set; } = Array.Empty<OKXDustAsset>();
    /// <summary>
    /// Available target assets
    /// </summary>
    [JsonPropertyName("toCcy")]
    public IEnumerable<string> ToAssets { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Dust asset 
/// </summary>
public record OKXDustAsset
{
    /// <summary>
    /// Quantity available for conversion
    /// </summary>
    [JsonPropertyName("fromAmt")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("fromCcy")]
    public string Asset { get; set; } = string.Empty;
}

