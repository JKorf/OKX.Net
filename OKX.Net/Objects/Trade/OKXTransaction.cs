﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Transaction info
/// </summary>
public record OKXTransaction
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    [JsonConverterCtor(typeof(ReplaceConverter), $"{OKXExchange.ClientOrderIdPrefix}->")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Bill id
    /// </summary>
    [JsonPropertyName("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Fill price
    /// </summary>
    [JsonPropertyName("fillPx")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Fill quantity
    /// </summary>
    [JsonPropertyName("fillSz")]
    public decimal? QuantityFilled { get; set; }

    /// <summary>
    /// Index price at the moment of trade execution
    /// </summary>
    [JsonPropertyName("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// Last filled profit and loss, applicable to orders which have a trade and aim to close position
    /// </summary>
    [JsonPropertyName("fillPnl")]
    public decimal? FillProfitAndLoss { get; set; }

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
    /// Order flow type
    /// </summary>
    [JsonPropertyName("execType"), JsonConverter(typeof(EnumConverter))]
    public OrderFlowType OrderFlowType { get; set; }

    /// <summary>
    /// Fee asset
    /// </summary>
    [JsonPropertyName("feeCcy")]
    public string FeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Trade time
    /// </summary>
    [JsonPropertyName("fillTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FillTime { get; set; }

    /// <summary>
    /// Data time
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Implied volitality for options
    /// </summary>
    [JsonPropertyName("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// Usd fill price for options
    /// </summary>
    [JsonPropertyName("fillPxUsd")]
    public decimal? FillUsdPrice { get; set; }

    /// <summary>
    /// Mark volatility when filled for options
    /// </summary>
    [JsonPropertyName("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// Forward price when filled for options
    /// </summary>
    [JsonPropertyName("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }

    /// <summary>
    /// Mark price when filled
    /// </summary>
    [JsonPropertyName("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonPropertyName("subType")]
    public string TransactionType { get; set; } = string.Empty;
}