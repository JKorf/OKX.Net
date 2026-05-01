using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;
/// <summary>
/// Algo order attached to an order
/// </summary>
[SerializationModel]
public record OKXAttachedAlgoOrder
{
    /// <summary>
    /// ["<c>attachAlgoClOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("attachAlgoClOrdId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>tpTriggerPx</c>"] Take profit trigger price
    /// </summary>
    [JsonPropertyName("tpTriggerPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TakeProfitTriggerPrice { get; set; }
    /// <summary>
    /// ["<c>tpOrdPx</c>"] Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrdPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TakeProfitOrderPrice { get; set; }
    /// <summary>
    /// ["<c>tpOrdKind</c>"] Take profit order kind
    /// </summary>
    [JsonPropertyName("tpOrdKind"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TriggerOrderKind? TakeProfitOrderKind { get; set; }
    /// <summary>
    /// ["<c>tpTriggerPxType</c>"] Take profit price type
    /// </summary>
    [JsonPropertyName("tpTriggerPxType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TriggerPriceType? TakeProfitPriceType { get; set; }
    /// <summary>
    /// ["<c>sz</c>"] Take profit quantity
    /// </summary>
    [JsonPropertyName("sz"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TakeProfitQuantity { get; set; }

    /// <summary>
    /// ["<c>slTriggerPx</c>"] Stop loss trigger price
    /// </summary>
    [JsonPropertyName("slTriggerPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? StopLossTriggerPrice { get; set; }
    /// <summary>
    /// ["<c>slOrdPx</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrdPx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? StopLossOrderPrice { get; set; }
    /// <summary>
    /// ["<c>slTriggerPxType</c>"] Stop loss price type
    /// </summary>
    [JsonPropertyName("slTriggerPxType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TriggerPriceType? StopLossPriceType { get; set; }

    /// <summary>
    /// ["<c>amendPxOnTriggerType</c>"] Whether to enable Cost-price SL. Only applicable to SL order of split TPs. Whether slTriggerPx will move to avgPx when the first TP order is triggered, 0: disable, the default value, 1: Enable
    /// </summary>
    [JsonPropertyName("amendPxOnTriggerType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AmendPriceOnTriggerType { get; set; }

    /// <summary>
    /// ["<c>callbackRatio</c>"] Callback ratio, e.g. 0.05 represents 5%.
    /// </summary>
    [JsonPropertyName("callbackRatio"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? CallbackRatio { get; set; }
    /// <summary>
    /// ["<c>callbackSpread</c>"] Callback spread (price distance).
    /// </summary>
    [JsonPropertyName("callbackSpread"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? CallbackSpread { get; set; }
    /// <summary>
    /// ["<c>activePx</c>"] Activation price. If not provided, the trailing stop is activated immediately upon order placement.
    /// </summary>
    [JsonPropertyName("activePx"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? ActivePrice { get; set; }
}
