using OKX.Net.Enums;

namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Lead trader current lead position
/// </summary>
[SerializationModel]
public record OKXCurrentSubposition
{
    /// <summary>
    /// ["<c>ccy</c>"] Currency
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instId</c>"] Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonPropertyName("instId")]
    public string? Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// ["<c>margin</c>"] Margin
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// ["<c>markPx</c>"] Latest mark price, only applicable to contract
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode. cross isolated
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// ["<c>openAvgPx</c>"] Average open price
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// ["<c>openTime</c>"] Open time
    /// </summary>
    [JsonPropertyName("openTime")]
    public long? OpenTimestamp { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>subPos</c>"] Quantity of positions
    /// </summary>
    [JsonPropertyName("subPos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>subPosId</c>"] Lead position ID
    /// </summary>
    [JsonPropertyName("subPosId")]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>uniqueCode</c>"] Lead trader unique code
    /// </summary>
    [JsonPropertyName("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>upl</c>"] Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal UnrealizedPnl { get; set; }

    /// <summary>
    /// ["<c>uplRatio</c>"] Unrealized profit and loss ratio
    /// </summary>
    [JsonPropertyName("uplRatio")]
    public decimal UnrealizedProfitAndLossRatio { get; set; }
}
