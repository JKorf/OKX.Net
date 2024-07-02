namespace OKX.Net.Objects.Public;

/// <summary>
/// Discount info
/// </summary>
public record OKXDiscountInfo
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Discount level
    /// </summary>
    [JsonPropertyName("discountLv")]
    public int DiscountLevel { get; set; }

    /// <summary>
    /// Discount info
    /// </summary>
    [JsonPropertyName("discountInfo")]
    public IEnumerable<OKXPublicDiscountInfoDetail> Details { get; set; } = Array.Empty<OKXPublicDiscountInfoDetail>();
}

/// <summary>
/// Discount details
/// </summary>
public record OKXPublicDiscountInfoDetail
{
    /// <summary>
    /// Discount rate
    /// </summary>
    [JsonPropertyName("discountRate")]
    public decimal? DiscountRate { get; set; }

    /// <summary>
    /// Max amount
    /// </summary>
    [JsonPropertyName("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// Min amount
    /// </summary>
    [JsonPropertyName("minAmt")]
    public decimal? MinimumAmount { get; set; }
}
