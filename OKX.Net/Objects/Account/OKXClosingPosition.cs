using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position info
/// </summary>
[SerializationModel]
public record OKXClosingPosition
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Closing type
    /// </summary>
    [JsonPropertyName("type")]
    public ClosingPositionType Type { get; set; }
    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Last update time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// Average closing price
    /// </summary>
    [JsonPropertyName("closeAvgPx")]
    public decimal? CloseAveragePrice { get; set; }

    /// <summary>
    /// Position id
    /// </summary>
    [JsonPropertyName("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Max open position
    /// </summary>
    [JsonPropertyName("openMaxPos")]
    public decimal? OpenMaxPos { get; set; }

    /// <summary>
    /// Total close position
    /// </summary>
    [JsonPropertyName("closeTotalPos")]
    public decimal? CloseTotalPos { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonPropertyName("direction")]
    public PositionSide Direction { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonPropertyName("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonPropertyName("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string? Asset { get; set; }

    /// <summary>
    /// Profit and loss ratio
    /// </summary>
    [JsonPropertyName("pnlRatio")]
    public decimal? PnlRatio { get; set; }

    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonPropertyName("realizedPnl")]
    public decimal? RealizedPnl { get; set; }

    /// <summary>
    /// Accumulated fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Funding fee
    /// </summary>
    [JsonPropertyName("fundingFee")]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonPropertyName("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }
}
