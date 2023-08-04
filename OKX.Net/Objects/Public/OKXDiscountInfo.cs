namespace OKX.Net.Objects.Public;

/// <summary>
/// Discount info
/// </summary>
public class OKXDiscountInfo
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Discount level
    /// </summary>
    [JsonProperty("discountLv")]
    public int DiscountLevel { get; set; }

    /// <summary>
    /// Discount info
    /// </summary>
    [JsonProperty("discountInfo")]
    public IEnumerable<OKXPublicDiscountInfoDetail> Details { get; set; } = Array.Empty<OKXPublicDiscountInfoDetail>();
}

/// <summary>
/// Discount details
/// </summary>
public class OKXPublicDiscountInfoDetail
{
    /// <summary>
    /// Discount rate
    /// </summary>
    [JsonProperty("discountRate")]
    public decimal? DiscountRate { get; set; }

    /// <summary>
    /// Max amount
    /// </summary>
    [JsonProperty("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// Min amount
    /// </summary>
    [JsonProperty("minAmt")]
    public decimal? MinimumAmount { get; set; }
}
