using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order place request
/// </summary>
public record OKXOrderPlaceRequest
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(EnumConverter))]
    public TradeMode TradeMode { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(EnumConverter))]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(EnumConverter))]
    public OrderType OrderType { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz"), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Reduce only
    /// </summary>
    [JsonProperty("reduceOnly", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Quantity type
    /// </summary>
    [JsonProperty("tgtCcy", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(EnumConverter))]
    public QuantityAsset? QuantityType { get; set; }
}