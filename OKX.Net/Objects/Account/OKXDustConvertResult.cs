namespace OKX.Net.Objects.Account;

/// <summary>
/// Dust conversion result
/// </summary>
public class OKXDustConvertResult
{
    /// <summary>
    /// Total quantity of OKB after conversion
    /// </summary>
    [JsonProperty("totalCnvAmt")]
    public decimal TotalConvertedQuantity { get; set; }
    /// <summary>
    /// Details of asset conversion
    /// </summary>
    [JsonProperty("details")]
    public IEnumerable<OKXDustConvertAsset> Assets { get; set; } = Array.Empty<OKXDustConvertAsset>();
}

/// <summary>
/// Dust conversion asset details
/// </summary>
public class OKXDustConvertAsset
{
    /// <summary>
    /// Quantity of asset before conversion
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// Asset name
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Quantity of OKB after conversion
    /// </summary>
    [JsonProperty("cnvAmt")]
    public decimal ConvertQuantity { get; set; }
    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}