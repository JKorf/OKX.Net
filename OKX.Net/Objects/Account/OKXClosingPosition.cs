using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position info
/// </summary>
[SerializationModel]
public record OKXClosingPosition
{
    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Closing type
    /// </summary>
    [JsonPropertyName("type")]
    public ClosingPositionType Type { get; set; }
    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>cTime</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// ["<c>uTime</c>"] Last update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// ["<c>openAvgPx</c>"] Average open price
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// ["<c>closeAvgPx</c>"] Average closing price
    /// </summary>
    [JsonPropertyName("closeAvgPx")]
    public decimal? CloseAveragePrice { get; set; }

    /// <summary>
    /// ["<c>posId</c>"] Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// ["<c>openMaxPos</c>"] Max open position
    /// </summary>
    [JsonPropertyName("openMaxPos")]
    public decimal? OpenMaxPos { get; set; }

    /// <summary>
    /// ["<c>closeTotalPos</c>"] Total close position
    /// </summary>
    [JsonPropertyName("closeTotalPos")]
    public decimal? CloseTotalPos { get; set; }

    /// <summary>
    /// ["<c>pnl</c>"] Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// ["<c>direction</c>"] Direction
    /// </summary>
    [JsonPropertyName("direction")]
    public PositionSide Direction { get; set; }

    /// <summary>
    /// ["<c>triggerPx</c>"] Trigger price
    /// </summary>
    [JsonPropertyName("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    /// <summary>
    /// ["<c>liqPx</c>"] Liquidation price
    /// </summary>
    [JsonPropertyName("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// ["<c>uly</c>"] Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string? Asset { get; set; }

    /// <summary>
    /// ["<c>pnlRatio</c>"] Profit and loss ratio
    /// </summary>
    [JsonPropertyName("pnlRatio")]
    public decimal? PnlRatio { get; set; }

    /// <summary>
    /// ["<c>realizedPnl</c>"] Realized profit and loss
    /// </summary>
    [JsonPropertyName("realizedPnl")]
    public decimal? RealizedPnl { get; set; }

    /// <summary>
    /// ["<c>fee</c>"] Accumulated fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// ["<c>fundingFee</c>"] Funding fee
    /// </summary>
    [JsonPropertyName("fundingFee")]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// ["<c>liqPenalty</c>"] Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonPropertyName("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }
}
