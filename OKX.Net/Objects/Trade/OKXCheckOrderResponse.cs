using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Check order info
/// </summary>
[SerializationModel]
public record OKXCheckOrderResponse
{
    /// <summary>
    /// ["<c>adjEq</c>"] Current adjusted / Effective equity in USD
    /// </summary>
    [JsonPropertyName("adjEq")]
    public decimal AdjustedEquity { get; set; }
    /// <summary>
    /// ["<c>adjEqChg</c>"] After placing order, changed quantity of adjusted / Effective equity in USD
    /// </summary>
    [JsonPropertyName("adjEqChg")]
    public decimal AdjustedEquityChange { get; set; }
    /// <summary>
    /// ["<c>availBal</c>"] Current available balance in margin coin currency, only applicable to turn auto borrow off
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal AvailableBalance { get; set; }
    /// <summary>
    /// ["<c>availBalChg</c>"] After placing order, changed quantity of available balance after placing order, only applicable to turn auto borrow off
    /// </summary>
    [JsonPropertyName("availBalChg")]
    public decimal AvailableBalanceChange { get; set; }
    /// <summary>
    /// ["<c>imr</c>"] Current initial margin requirement in USD
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal InitialMarginRequirement { get; set; }
    /// <summary>
    /// ["<c>imrChg</c>"] After placing order, changed quantity of initial margin requirement in USD
    /// </summary>
    [JsonPropertyName("imrChg")]
    public decimal InitialMarginRequirementChange { get; set; }
    /// <summary>
    /// ["<c>liab</c>"] Current liabilities of currency. For cross, it is cross liabilities. For isolated position, it is isolated liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal Liabilities { get; set; }
    /// <summary>
    /// ["<c>liabChg</c>"] After placing order, changed quantity of liabilities
    /// </summary>
    [JsonPropertyName("liabChg")]
    public decimal LiabilitiesChange { get; set; }
    /// <summary>
    /// ["<c>liabChgCcy</c>"] After placing order, the unit of changed liabilities quantity. Only applicable cross and in auto borrow
    /// </summary>
    [JsonPropertyName("liabChgCcy")]
    public string? LiabilitiesChangeAsset { get; set; }
    /// <summary>
    /// ["<c>liqPx</c>"] Current estimated liquidation price
    /// </summary>
    [JsonPropertyName("liqPx")]
    public decimal LiquidationPrice { get; set; }
    /// <summary>
    /// ["<c>liqPxDiff</c>"] After placing order, the distance between estimated liquidation price and mark price
    /// </summary>
    [JsonPropertyName("liqPxDiff")]
    public string LiquidationPriceDifference { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>liqPxDiffRatio</c>"] After placing order, the distance rate between estimated liquidation price and mark price
    /// </summary>
    [JsonPropertyName("liqPxDiffRatio")]
    public decimal LiquidationPriceDifferenceRatio { get; set; }
    /// <summary>
    /// ["<c>mgnRatio</c>"] Current margin ratio in USD
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal MarginRatio { get; set; }
    /// <summary>
    /// ["<c>mgnRatioChg</c>"] After placing order, changed quantity of margin ratio in USD
    /// </summary>
    [JsonPropertyName("mgnRatioChg")]
    public decimal MarginRatioChange { get; set; }
    /// <summary>
    /// ["<c>mmr</c>"] Current Maintenance margin requirement in USD
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal MaintenanceMarginRequirement { get; set; }
    /// <summary>
    /// ["<c>mmrChg</c>"] After placing order, changed quantity of maintenance margin requirement in USD
    /// </summary>
    [JsonPropertyName("mmrChg")]
    public decimal MaintenanceMarginRequirementChange { get; set; }
    /// <summary>
    /// ["<c>posBal</c>"] Current positive asset, only applicable to margin isolated position
    /// </summary>
    [JsonPropertyName("posBal")]
    public string? PositionBalance { get; set; }
    /// <summary>
    /// ["<c>posBalChg</c>"] After placing order, positive asset of margin isolated, only applicable to margin isolated position
    /// </summary>
    [JsonPropertyName("posBalChg")]
    public string? PositionBalanceChange { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Unit type
    /// </summary>
    [JsonPropertyName("type")]
    public CheckUnitType? Type { get; set; }
}


