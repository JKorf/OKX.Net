using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
namespace OKX.Net.Objects.Public;

/// <summary>
/// Discount info
/// </summary>
[SerializationModel]
public record OKXDiscountInfo
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Interest-free quota
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Collateral restriction status
    /// </summary>
    [JsonPropertyName("colRes")]
    public CollateralRestrictionStatus CollateralRestrictionStatus { get; set; }

    /// <summary>
    /// Discount level
    /// </summary>
    [JsonPropertyName("discountLv")]
    public int? DiscountLevel { get; set; }

    /// <summary>
    /// Minimal discount rate
    /// </summary>
    [JsonPropertyName("minDiscountRate")]
    public decimal? MinDiscountRate { get; set; }

    /// <summary>
    /// DEPRECATED, use DiscountDetails instead
    /// </summary>
    [JsonPropertyName("discountInfo")]
    public OKXPublicDiscountInfoDetail[] Details { get; set; } = Array.Empty<OKXPublicDiscountInfoDetail>();

    /// <summary>
    /// Discount info
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

/// <summary>
/// Discount details
/// </summary>
[SerializationModel]
public record OKXPublicDiscountDetails
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

    /// <summary>
    /// Tier
    /// </summary>
    [JsonPropertyName("tier")]
    public string? Tier { get; set; }

    /// <summary>
    /// Liquidation penalty rate
    /// </summary>
    [JsonPropertyName("liqPenaltyRate")]
    public decimal? LiquidationPenaltyRate { get; set; }

    /// <summary>
    /// Discount equity in currency for quick calculation if your equity is the MaximumAmount
    /// </summary>
    [JsonPropertyName("disCcyEq")]
    public decimal? DiscountAssetEquity { get; set; }
}
