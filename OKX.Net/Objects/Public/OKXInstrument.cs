using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Instrument
/// </summary>
[SerializationModel]
public record OKXInstrument
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Category
    /// </summary>
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Base asset
    /// </summary>
    [JsonPropertyName("baseCcy")]
    public string BaseAsset { get; set; } = string.Empty;

    /// <summary>
    /// Quote asset
    /// </summary>
    [JsonPropertyName("quoteCcy")]
    public string QuoteAsset { get; set; } = string.Empty;

    /// <summary>
    /// settlement asset
    /// </summary>
    [JsonPropertyName("settleCcy")]
    public string SettlementAsset { get; set; } = string.Empty;

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonPropertyName("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Contract multiplier
    /// </summary>
    [JsonPropertyName("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    /// <summary>
    /// Contract value asset
    /// </summary>
    [JsonPropertyName("ctValCcy")]
    public string ContractValueAsset { get; set; } = string.Empty;

    /// <summary>
    /// Option type
    /// </summary>
    [JsonPropertyName("optType")]
    public OptionType? OptionType { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonPropertyName("stk")]
    public decimal? StrikePrice { get; set; }

    /// <summary>
    /// Listing time
    /// </summary>
    [JsonPropertyName("listTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ListingTime { get; set; }

    /// <summary>
    /// The end time of call auction, only applicable to SPOT that are listed through call auctions
    /// </summary>
    [JsonPropertyName("auctionEndTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? AuctionEndTime { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [JsonPropertyName("expTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ExpiryTime { get; set; }

    /// <summary>
    /// Maximum leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public int? MaximumLeverage { get; set; }

    /// <summary>
    /// Tick size
    /// </summary>
    [JsonPropertyName("tickSz")]
    public decimal? TickSize { get; set; }

    /// <summary>
    /// Lot size
    /// </summary>
    [JsonPropertyName("lotSz")]
    public decimal? LotSize { get; set; }

    /// <summary>
    /// Minimum order size
    /// </summary>
    [JsonPropertyName("minSz")]
    public decimal? MinimumOrderSize { get; set; }

    /// <summary>
    /// Contract type
    /// </summary>
    [JsonPropertyName("ctType")]
    public ContractType? ContractType { get; set; }

    /// <summary>
    /// Alias
    /// </summary>
    [JsonPropertyName("alias")]
    public InstrumentAlias? Alias { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonPropertyName("state")]
    public InstrumentState State { get; set; }

    /// <summary>
    /// The maximum order quantity of the contract or spot limit order.
    /// </summary>
    [JsonPropertyName("maxLmtSz")]
    public decimal? MaxLimitQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot market order.
    /// </summary>
    [JsonPropertyName("maxMktSz")]
    public decimal? MaxMarketQuantity { get; set; }
    /// <summary>
    /// The maximum USD order value for limit orders
    /// </summary>
    [JsonPropertyName("maxLmtAmt")]
    public decimal? MaxLimitValue { get; set; }
    /// <summary>
    /// The maximum USD order value for market orders
    /// </summary>
    [JsonPropertyName("maxMktAmt")]
    public decimal? MaxMarketValue { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot twap order.
    /// </summary>
    [JsonPropertyName("maxTwapSz")]
    public decimal? MaxTwapQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot iceBerg order.
    /// </summary>
    [JsonPropertyName("maxIcebergSz")]
    public decimal? MaxIcebergQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot trigger order.
    /// </summary>
    [JsonPropertyName("maxTriggerSz")]
    public decimal? MaxTriggerQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot stop market order.
    /// </summary>
    [JsonPropertyName("maxStopSz")]
    public decimal? MaxStopQuantity { get; set; }
    /// <summary>
    /// Trading rule type
    /// </summary>
    [JsonPropertyName("ruleType")]
    public SymbolRuleType? RuleType { get; set; }
    /// <summary>
    /// Whether daily settlement is enabled
    /// </summary>
    [JsonPropertyName("futureSettlement")]
    public bool? FutureSettlement { get; set; }
    /// <summary>
    /// Continuous trading switch time. The switch time from call auction, prequote to continuous trading. Only applicable to SPOT/MARGIN that are listed through call auction or prequote
    /// </summary>
    [JsonPropertyName("contTdSwTime")]
    public DateTime? ContinuousTradingSwitchTime { get; set; }
    /// <summary>
    /// Open type, only applicable to SPOT/MARGIN
    /// </summary>
    [JsonPropertyName("openType")]
    public OpenType? OpenType { get; set; }
    /// <summary>
    /// Trade quote asset list
    /// </summary>
    [JsonPropertyName("tradeQuoteCcyList")]
    public string[] TradeQuoteAssetList { get; set; } = [];
    /// <summary>
    /// Symbol code
    /// </summary>
    [JsonPropertyName("instIdCode")]
    public long? SymbolCode { get; set; }
    /// <summary>
    /// Timestamp the market is switched from pre-market mode to normal mode
    /// </summary>
    [JsonPropertyName("preMktSwTime")]
    public DateTime? PreMarketSwitchTime { get; set; }
    /// <summary>
    /// Maximum position value (USD) for this instrument at the user level, based on the notional value of all same-direction open positions and resting orders. The effective user limit is max(posLmtAmt, oiUSD × posLmtPct). Applicable to SWAP/FUTURES.
    /// </summary>
    [JsonPropertyName("posLmtAmt")]
    public decimal? PositionLimitQuantity { get; set; }
    /// <summary>
    /// Maximum position ratio (e.g., 30 for 30%) a user may hold relative to the platform’s current total position value. The effective user limit is max(posLmtAmt, oiUSD × posLmtPct). Applicable to SWAP/FUTURES.
    /// </summary>
    [JsonPropertyName("posLmtPct")]
    public decimal? PositionLimitPercentage { get; set; }
    /// <summary>
    /// Platform-wide maximum position value (USD) for this instrument. If the global position limit switch is enabled and platform total open interest reaches or exceeds this value, all users’ new opening orders for this instrument are rejected; otherwise, orders pass.
    /// </summary>
    [JsonPropertyName("maxPlatOILmt")]
    public decimal? PositionPlatformLimitQuantity { get; set; }
}
