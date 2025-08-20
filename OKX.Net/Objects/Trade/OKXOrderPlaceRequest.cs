using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;
using System;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order place request
/// </summary>
[SerializationModel]
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
    [JsonPropertyName("tdMode")]
    public Enums.TradeMode TradeMode { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonPropertyName("ordType")]
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
    [JsonPropertyName("tgtCcy"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// Place options orders in USD, only applicable to options
    /// </summary>
    [JsonPropertyName("pxUsd"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// Place options orders based on implied volatility, where 1 represents 100%. Only applicable to OPTIONS
    /// </summary>
    [JsonPropertyName("pxVol"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? PriceVol { get; set; }

    /// <summary>
    /// Whether to disallow the system from amending the size of the SPOT Market Order. If true, system will not amend and reject the market order if user does not have sufficient funds.
    /// </summary>
    [JsonPropertyName("banAmend"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? BanAmend { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonPropertyName("stpMode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SelfTradePreventionMode? StpMode { get; set; }

    /// <summary>
    /// The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.
    /// </summary>
    [JsonPropertyName("tradeQuoteCcy")]
    public string? TradeQuoteAsset { get; set; }

    /// <summary>
    /// Attached take profit / stop loss orders
    /// </summary>
    [JsonPropertyName("attachAlgoOrds"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public OKXAttachedAlgoOrder[]? AttachedAlgoOrders { get; set; }
}
