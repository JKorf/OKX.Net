using OKX.Net.Enums;
namespace OKX.Net.Objects.Public;

/// <summary>
/// Discount info
/// </summary>
[SerializationModel]
public record OKXDiscountInfo
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>amt</c>"] Interest-free quota
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>colRes</c>"] Collateral restriction status
    /// </summary>
    [JsonPropertyName("colRes")]
    public CollateralRestrictionStatus CollateralRestrictionStatus { get; set; }

    /// <summary>
    /// ["<c>discountLv</c>"] Discount level
    /// </summary>
    [JsonPropertyName("discountLv")]
    public int? DiscountLevel { get; set; }

    /// <summary>
    /// ["<c>minDiscountRate</c>"] Minimal discount rate
    /// </summary>
    [JsonPropertyName("minDiscountRate")]
    public decimal? MinDiscountRate { get; set; }

    /// <summary>
    /// ["<c>discountInfo</c>"] DEPRECATED, use DiscountDetails instead
    /// </summary>
    [JsonPropertyName("discountInfo")]
    public OKXPublicDiscountInfoDetail[] Details { get; set; } = Array.Empty<OKXPublicDiscountInfoDetail>();

    /// <summary>
    /// ["<c>details</c>"] Discount info
    /// </summary>
    [JsonPropertyName("details")]
    public OKXPublicDiscountDetails[] DiscountDetails { get; set; } = Array.Empty<OKXPublicDiscountDetails>();
}

/// <summary>
/// Discount details
/// </summary>
[SerializationModel]
public record OKXPublicDiscountInfoDetail
{
    /// <summary>
    /// ["<c>discountRate</c>"] Discount rate
    /// </summary>
    [JsonPropertyName("discountRate")]
    public decimal? DiscountRate { get; set; }

    /// <summary>
    /// ["<c>maxAmt</c>"] Max amount
    /// </summary>
    [JsonPropertyName("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// ["<c>minAmt</c>"] Min amount
    /// </summary>
    [JsonPropertyName("minAmt")]
    public decimal? MinimumAmount { get; set; }
}

/// <summary>
/// Discount details
/// </summary>
[SerializationModel]
public record OKXPublicDiscountDetails
{
    /// <summary>
    /// ["<c>discountRate</c>"] Discount rate
    /// </summary>
    [JsonPropertyName("discountRate")]
    public decimal? DiscountRate { get; set; }

    /// <summary>
    /// ["<c>maxAmt</c>"] Max amount
    /// </summary>
    [JsonPropertyName("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// ["<c>minAmt</c>"] Min amount
    /// </summary>
    [JsonPropertyName("minAmt")]
    public decimal? MinimumAmount { get; set; }

    /// <summary>
    /// ["<c>tier</c>"] Tier
    /// </summary>
    [JsonPropertyName("tier")]
    public string? Tier { get; set; }

    /// <summary>
    /// ["<c>liqPenaltyRate</c>"] Liquidation penalty rate
    /// </summary>
    [JsonPropertyName("liqPenaltyRate")]
    public decimal? LiquidationPenaltyRate { get; set; }

    /// <summary>
    /// ["<c>disCcyEq</c>"] Discount equity in currency for quick calculation if your equity is the MaximumAmount
    /// </summary>
    [JsonPropertyName("disCcyEq")]
    public decimal? DiscountAssetEquity { get; set; }
}
