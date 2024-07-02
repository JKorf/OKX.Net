namespace OKX.Net.Objects.Account;

/// <summary>
/// Dust conversion result
/// </summary>
public record OKXDustConvertResult
{
    /// <summary>
    /// Total quantity of OKB after conversion
    /// </summary>
    [JsonPropertyName("totalCnvAmt")]
    public decimal TotalConvertedQuantity { get; set; }
    /// <summary>
    /// Details of asset conversion
    /// </summary>
    [JsonPropertyName("details")]
    public IEnumerable<OKXDustConvertAsset> Assets { get; set; } = Array.Empty<OKXDustConvertAsset>();
}

/// <summary>
/// Dust conversion asset details
/// </summary>
public record OKXDustConvertAsset
{
    /// <summary>
    /// Quantity of asset before conversion
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// Asset name
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Quantity of OKB after conversion
    /// </summary>
    [JsonPropertyName("cnvAmt")]
    public decimal ConvertQuantity { get; set; }
    /// <summary>
    /// Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
}