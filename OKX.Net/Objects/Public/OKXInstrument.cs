using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Instrument
/// </summary>
[SerializationModel]
public record OKXInstrument
{
    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instFamily</c>"] Instrument family, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>uly</c>"] Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>category</c>"] Category
    /// </summary>
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>baseCcy</c>"] Base asset
    /// </summary>
    [JsonPropertyName("baseCcy")]
    public string BaseAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>quoteCcy</c>"] Quote asset
    /// </summary>
    [JsonPropertyName("quoteCcy")]
    public string QuoteAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>settleCcy</c>"] settlement asset
    /// </summary>
    [JsonPropertyName("settleCcy")]
    public string SettlementAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ctVal</c>"] Contract value
    /// </summary>
    [JsonPropertyName("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// ["<c>ctMult</c>"] Contract multiplier
    /// </summary>
    [JsonPropertyName("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    /// <summary>
    /// ["<c>ctValCcy</c>"] Contract value asset
    /// </summary>
    [JsonPropertyName("ctValCcy")]
    public string ContractValueAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>optType</c>"] Option type
    /// </summary>
    [JsonPropertyName("optType")]
    public OptionType? OptionType { get; set; }

    /// <summary>
    /// ["<c>stk</c>"] Strike price
    /// </summary>
    [JsonPropertyName("stk")]
    public decimal? StrikePrice { get; set; }

    /// <summary>
    /// ["<c>listTime</c>"] Listing time
    /// </summary>
    [JsonPropertyName("listTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ListingTime { get; set; }

    /// <summary>
    /// ["<c>auctionEndTime</c>"] The end time of call auction, only applicable to SPOT that are listed through call auctions
    /// </summary>
    [JsonPropertyName("auctionEndTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? AuctionEndTime { get; set; }

    /// <summary>
    /// ["<c>expTime</c>"] Expiry time
    /// </summary>
    [JsonPropertyName("expTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ExpiryTime { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Maximum leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public int? MaximumLeverage { get; set; }

    /// <summary>
    /// ["<c>tickSz</c>"] Tick size
    /// </summary>
    [JsonPropertyName("tickSz")]
    public decimal? TickSize { get; set; }

    /// <summary>
    /// ["<c>lotSz</c>"] Lot size
    /// </summary>
    [JsonPropertyName("lotSz")]
    public decimal? LotSize { get; set; }

    /// <summary>
    /// ["<c>minSz</c>"] Minimum order size
    /// </summary>
    [JsonPropertyName("minSz")]
    public decimal? MinimumOrderSize { get; set; }

    /// <summary>
    /// ["<c>ctType</c>"] Contract type
    /// </summary>
    [JsonPropertyName("ctType")]
    public ContractType? ContractType { get; set; }

    /// <summary>
    /// ["<c>alias</c>"] Alias
    /// </summary>
    [JsonPropertyName("alias")]
    public InstrumentAlias? Alias { get; set; }

    /// <summary>
    /// ["<c>state</c>"] State
    /// </summary>
    [JsonPropertyName("state")]
    public InstrumentState State { get; set; }

    /// <summary>
    /// ["<c>maxLmtSz</c>"] The maximum order quantity of the contract or spot limit order.
    /// </summary>
    [JsonPropertyName("maxLmtSz")]
    public decimal? MaxLimitQuantity { get; set; }
    /// <summary>
    /// ["<c>maxMktSz</c>"] The maximum order quantity of the contract or spot market order.
    /// </summary>
    [JsonPropertyName("maxMktSz")]
    public decimal? MaxMarketQuantity { get; set; }
    /// <summary>
    /// ["<c>maxLmtAmt</c>"] The maximum USD order value for limit orders
    /// </summary>
    [JsonPropertyName("maxLmtAmt")]
    public decimal? MaxLimitValue { get; set; }
    /// <summary>
    /// ["<c>maxMktAmt</c>"] The maximum USD order value for market orders
    /// </summary>
    [JsonPropertyName("maxMktAmt")]
    public decimal? MaxMarketValue { get; set; }
    /// <summary>
    /// ["<c>maxTwapSz</c>"] The maximum order quantity of the contract or spot twap order.
    /// </summary>
    [JsonPropertyName("maxTwapSz")]
    public decimal? MaxTwapQuantity { get; set; }
    /// <summary>
    /// ["<c>maxIcebergSz</c>"] The maximum order quantity of the contract or spot iceBerg order.
    /// </summary>
    [JsonPropertyName("maxIcebergSz")]
    public decimal? MaxIcebergQuantity { get; set; }
    /// <summary>
    /// ["<c>maxTriggerSz</c>"] The maximum order quantity of the contract or spot trigger order.
    /// </summary>
    [JsonPropertyName("maxTriggerSz")]
    public decimal? MaxTriggerQuantity { get; set; }
    /// <summary>
    /// ["<c>maxStopSz</c>"] The maximum order quantity of the contract or spot stop market order.
    /// </summary>
    [JsonPropertyName("maxStopSz")]
    public decimal? MaxStopQuantity { get; set; }
    /// <summary>
    /// ["<c>ruleType</c>"] Trading rule type
    /// </summary>
    [JsonPropertyName("ruleType")]
    public SymbolRuleType? RuleType { get; set; }
    /// <summary>
    /// ["<c>futureSettlement</c>"] Whether daily settlement is enabled
    /// </summary>
    [JsonPropertyName("futureSettlement")]
    public bool? FutureSettlement { get; set; }
    /// <summary>
    /// ["<c>contTdSwTime</c>"] Continuous trading switch time. The switch time from call auction, prequote to continuous trading. Only applicable to SPOT/MARGIN that are listed through call auction or prequote
    /// </summary>
    [JsonPropertyName("contTdSwTime")]
    public DateTime? ContinuousTradingSwitchTime { get; set; }
    /// <summary>
    /// ["<c>openType</c>"] Open type, only applicable to SPOT/MARGIN
    /// </summary>
    [JsonPropertyName("openType")]
    public OpenType? OpenType { get; set; }
    /// <summary>
    /// ["<c>tradeQuoteCcyList</c>"] Trade quote asset list
    /// </summary>
    [JsonPropertyName("tradeQuoteCcyList")]
    public string[] TradeQuoteAssetList { get; set; } = [];
    /// <summary>
    /// ["<c>instIdCode</c>"] Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode")]
    public long? SymbolCode { get; set; }
    /// <summary>
    /// ["<c>preMktSwTime</c>"] Timestamp the market is switched from pre-market mode to normal mode
    /// </summary>
    [JsonPropertyName("preMktSwTime")]
    public DateTime? PreMarketSwitchTime { get; set; }
    /// <summary>
    /// ["<c>posLmtAmt</c>"] Maximum position value (USD) for this instrument at the user level, based on the notional value of all same-direction open positions and resting orders. The effective user limit is max(posLmtAmt, oiUSD � posLmtPct). Applicable to SWAP/FUTURES.
    /// </summary>
    [JsonPropertyName("posLmtAmt")]
    public decimal? PositionLimitQuantity { get; set; }
    /// <summary>
    /// ["<c>posLmtPct</c>"] Maximum position ratio (e.g., 30 for 30%) a user may hold relative to the platform�s current total position value. The effective user limit is max(posLmtAmt, oiUSD � posLmtPct). Applicable to SWAP/FUTURES.
    /// </summary>
    [JsonPropertyName("posLmtPct")]
    public decimal? PositionLimitPercentage { get; set; }
    /// <summary>
    /// ["<c>maxPlatOILmt</c>"] Platform-wide maximum position value (USD) for this instrument. If the global position limit switch is enabled and platform total open interest reaches or exceeds this value, all users� new opening orders for this instrument are rejected; otherwise, orders pass.
    /// </summary>
    [JsonPropertyName("maxPlatOILmt")]
    public decimal? PositionPlatformLimitQuantity { get; set; }
    /// <summary>
    /// ["<c>instCategory</c>"] Symbol category
    /// </summary>
    [JsonPropertyName("instCategory")]
    public SymbolCategory? SymbolCategory { get; set; }
}
