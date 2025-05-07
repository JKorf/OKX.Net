using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Check order info
/// </summary>
[SerializationModel]
public record OKXCheckOrderResponse
{
    /// <summary>
    /// Current adjusted / Effective equity in USD
    /// </summary>
    [JsonPropertyName("adjEq")]
    public decimal AdjustedEquity { get; set; }
    /// <summary>
    /// After placing order, changed quantity of adjusted / Effective equity in USD
    /// </summary>
    [JsonPropertyName("adjEqChg")]
    public decimal AdjustedEquityChange { get; set; }
    /// <summary>
    /// Current available balance in margin coin currency, only applicable to turn auto borrow off
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal AvailableBalance { get; set; }
    /// <summary>
    /// After placing order, changed quantity of available balance after placing order, only applicable to turn auto borrow off
    /// </summary>
    [JsonPropertyName("availBalChg")]
    public decimal AvailableBalanceChange { get; set; }
    /// <summary>
    /// Current initial margin requirement in USD
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal InitialMarginRequirement { get; set; }
    /// <summary>
    /// After placing order, changed quantity of initial margin requirement in USD
    /// </summary>
    [JsonPropertyName("imrChg")]
    public decimal InitialMarginRequirementChange { get; set; }
    /// <summary>
    /// Current liabilities of currency. For cross, it is cross liabilities. For isolated position, it is isolated liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal Liabilities { get; set; }
    /// <summary>
    /// After placing order, changed quantity of liabilities
    /// </summary>
    [JsonPropertyName("liabChg")]
    public decimal LiabilitiesChange { get; set; }
    /// <summary>
    /// After placing order, the unit of changed liabilities quantity. Only applicable cross and in auto borrow
    /// </summary>
    [JsonPropertyName("liabChgCcy")]
    public string? LiabilitiesChangeAsset { get; set; }
    /// <summary>
    /// Current estimated liquidation price
    /// </summary>
    [JsonPropertyName("liqPx")]
    public decimal LiquidationPrice { get; set; }
    /// <summary>
    /// After placing order, the distance between estimated liquidation price and mark price
    /// </summary>
    [JsonPropertyName("liqPxDiff")]
    public string LiquidationPriceDifference { get; set; } = string.Empty;
    /// <summary>
    /// After placing order, the distance rate between estimated liquidation price and mark price
    /// </summary>
    [JsonPropertyName("liqPxDiffRatio")]
    public decimal LiquidationPriceDifferenceRatio { get; set; }
    /// <summary>
    /// Current margin ratio in USD
    /// </summary>
    [JsonPropertyName("mgnRatio")]
    public decimal MarginRatio { get; set; }
    /// <summary>
    /// After placing order, changed quantity of margin ratio in USD
    /// </summary>
    [JsonPropertyName("mgnRatioChg")]
    public decimal MarginRatioChange { get; set; }
    /// <summary>
    /// Current Maintenance margin requirement in USD
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal MaintenanceMarginRequirement { get; set; }
    /// <summary>
    /// After placing order, changed quantity of maintenance margin requirement in USD
    /// </summary>
    [JsonPropertyName("mmrChg")]
    public decimal MaintenanceMarginRequirementChange { get; set; }
    /// <summary>
    /// Current positive asset, only applicable to margin isolated position
    /// </summary>
    [JsonPropertyName("posBal")]
    public string? PositionBalance { get; set; }
    /// <summary>
    /// After placing order, positive asset of margin isolated, only applicable to margin isolated position
    /// </summary>
    [JsonPropertyName("posBalChg")]
    public string? PositionBalanceChange { get; set; }
    /// <summary>
    /// Unit type
    /// </summary>
    [JsonPropertyName("type")]
    public CheckUnitType? Type { get; set; }
}


