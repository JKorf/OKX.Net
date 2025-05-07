using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Trade;
/// <summary>
/// Algo order attached to an order
/// </summary>
[SerializationModel]
public record OKXAttachedAlgoOrder
{
    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("attachAlgoClOrdId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TakeProfitTriggerPrice { get; set; }
    /// <summary>
    /// Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrdPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TakeProfitOrderPrice { get; set; }
    /// <summary>
    /// Take profit order kind
    /// </summary>
    [JsonPropertyName("tpOrdKind"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TriggerOrderKind? TakeProfitOrderKind { get; set; }
    /// <summary>
    /// Take profit price type
    /// </summary>
    [JsonPropertyName("tpTriggerPxType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TriggerPriceType? TakeProfitPriceType { get; set; }
    /// <summary>
    /// Take profit quantity
    /// </summary>
    [JsonPropertyName("sz"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TakeProfitQuantity { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? StopLossTriggerPrice { get; set; }
    /// <summary>
    /// Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrdPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? StopLossOrderPrice { get; set; }
    /// <summary>
    /// Stop loss price type
    /// </summary>
    [JsonPropertyName("slTriggerPxType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TriggerPriceType? StopLossPriceType { get; set; }

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs. Whether slTriggerPx will move to avgPx when the first TP order is triggered, 0: disable, the default value, 1: Enable
    /// </summary>
    [JsonPropertyName("amendPxOnTriggerType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AmendPriceOnTriggerType { get; set; }
}
