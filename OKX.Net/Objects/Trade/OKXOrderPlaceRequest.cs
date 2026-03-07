using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order place request
/// </summary>
[SerializationModel]
public record OKXOrderPlaceRequest
{
    /// <summary>
    /// ["<c>instId</c>"] Deprecated, use SymbolCode parameter instead
    /// </summary>
    [JsonPropertyName("instId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Symbol { get; set; }
    /// <summary>
    /// ["<c>instIdCode</c>"] Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long? SymbolCode { get; set; }

    /// <summary>
    /// ["<c>tdMode</c>"] Trade mode
    /// </summary>
    [JsonPropertyName("tdMode")]
    public Enums.TradeMode TradeMode { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// ["<c>ordType</c>"] Order type
    /// </summary>
    [JsonPropertyName("ordType")]
    public OrderType OrderType { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    [JsonPropertyName("sz"), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    [JsonPropertyName("px"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// ["<c>tgtCcy</c>"] Quantity type
    /// </summary>
    [JsonPropertyName("tgtCcy"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// ["<c>pxUsd</c>"] Place options orders in USD, only applicable to options
    /// </summary>
    [JsonPropertyName("pxUsd"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// ["<c>pxVol</c>"] Place options orders based on implied volatility, where 1 represents 100%. Only applicable to OPTIONS
    /// </summary>
    [JsonPropertyName("pxVol"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
    public decimal? PriceVol { get; set; }

    /// <summary>
    /// ["<c>banAmend</c>"] Whether to disallow the system from amending the size of the SPOT Market Order. If true, system will not amend and reject the market order if user does not have sufficient funds.
    /// </summary>
    [JsonPropertyName("banAmend"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? BanAmend { get; set; }

    /// <summary>
    /// ["<c>stpMode</c>"] Self trade prevention mode
    /// </summary>
    [JsonPropertyName("stpMode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SelfTradePreventionMode? StpMode { get; set; }

    /// <summary>
    /// ["<c>tradeQuoteCcy</c>"] The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.
    /// </summary>
    [JsonPropertyName("tradeQuoteCcy")]
    public string? TradeQuoteAsset { get; set; }

    /// <summary>
    /// ["<c>attachAlgoOrds</c>"] Attached take profit / stop loss orders
    /// </summary>
    [JsonPropertyName("attachAlgoOrds"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public OKXAttachedAlgoOrder[]? AttachedAlgoOrders { get; set; }
}
