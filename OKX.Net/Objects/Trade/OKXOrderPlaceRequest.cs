﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order place request
/// </summary>
public record OKXOrderPlaceRequest
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonPropertyName("tdMode"), JsonConverter(typeof(EnumConverter))]
    public TradeMode TradeMode { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonPropertyName("ordType"), JsonConverter(typeof(EnumConverter))]
    public OrderType OrderType { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("sz"), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("px"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Quantity type
    /// </summary>
    [JsonPropertyName("tgtCcy"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(EnumConverter))]
    public QuantityAsset? QuantityType { get; set; }
}