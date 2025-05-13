using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Dust assets
/// </summary>
[SerializationModel]
public record OKXDustAssets
{
    /// <summary>
    /// Convertable assets
    /// </summary>
    [JsonPropertyName("fromData")]
    public OKXDustAsset[] Convertable { get; set; } = Array.Empty<OKXDustAsset>();
    /// <summary>
    /// Available target assets
    /// </summary>
    [JsonPropertyName("toCcy")]
    public string[] ToAssets { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Dust asset 
/// </summary>
[SerializationModel]
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

