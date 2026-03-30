using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// One click repay history V2
/// </summary>
[SerializationModel]
public record OKXOneClickRepayHistoryV2
{
    /// <summary>
    /// ["<c>debtCcy</c>"] Debt currency
    /// </summary>
    [JsonPropertyName("debtCcy")]
    public string DebtAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>repayCcyList</c>"] Repay currency list
    /// </summary>
    [JsonPropertyName("repayCcyList")]
    public string[] RepayAssetList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// ["<c>fillDebtSz</c>"] Amount of debt currency transacted
    /// </summary>
    [JsonPropertyName("fillDebtSz")]
    public decimal FillDebtQuantity { get; set; }

    /// <summary>
    /// ["<c>status</c>"] Current status of one-click repay
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ordIdInfo</c>"] Order info
    /// </summary>
    [JsonPropertyName("ordIdInfo")]
    public OKXOneClickRepayOrderInfo[] OrderInfo { get; set; } = Array.Empty<OKXOneClickRepayOrderInfo>();
}

/// <summary>
/// Order info for one-click repay
/// </summary>
[SerializationModel]
public record OKXOneClickRepayOrderInfo
{
    /// <summary>
    /// ["<c>ordId</c>"] Order ID
    /// </summary>
    [JsonPropertyName("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instId</c>"] Instrument ID
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ordType</c>"] Order type
    /// </summary>
    [JsonPropertyName("ordType")]
    public OrderType OrderType { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// ["<c>px</c>"] Order price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Order quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>state</c>"] Order state
    /// </summary>
    [JsonPropertyName("state")]
    public OrderStatus State { get; set; }

    /// <summary>
    /// ["<c>fillPx</c>"] Last filled price
    /// </summary>
    [JsonPropertyName("fillPx")]
    public decimal FillPrice { get; set; }

    /// <summary>
    /// ["<c>fillSz</c>"] Last filled quantity
    /// </summary>
    [JsonPropertyName("fillSz")]
    public decimal FillQuantity { get; set; }

    /// <summary>
    /// ["<c>cTime</c>"] Order creation time
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}