using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Instrument
/// </summary>
public class OKXInstrument
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Category
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Base asset
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseAsset { get; set; } = string.Empty;

    /// <summary>
    /// Quote asset
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteAsset { get; set; } = string.Empty;

    /// <summary>
    /// settlement asset
    /// </summary>
    [JsonProperty("settleCcy")]
    public string SettlementAsset { get; set; } = string.Empty;

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Contract multiplier
    /// </summary>
    [JsonProperty("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    /// <summary>
    /// Contract value asset
    /// </summary>
    [JsonProperty("ctValCcy")]
    public string ContractValueAsset { get; set; } = string.Empty;

    /// <summary>
    /// Option type
    /// </summary>
    [JsonProperty("optType"), JsonConverter(typeof(OptionTypeConverter))]
    public OKXOptionType? OptionType { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("stk")]
    public decimal? StrikePrice { get; set; }

    /// <summary>
    /// Listing time
    /// </summary>
    [JsonProperty("listTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ListingTime { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [JsonProperty("expTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ExpiryTime { get; set; }

    /// <summary>
    /// Maximum leverage
    /// </summary>
    [JsonProperty("lever")]
    public int? MaximumLeverage { get; set; }

    /// <summary>
    /// Tick size
    /// </summary>
    [JsonProperty("tickSz")]
    public decimal TickSize { get; set; }

    /// <summary>
    /// Lot size
    /// </summary>
    [JsonProperty("lotSz")]
    public decimal LotSize { get; set; }

    /// <summary>
    /// Minimum order size
    /// </summary>
    [JsonProperty("minSz")]
    public decimal MinimumOrderSize { get; set; }

    /// <summary>
    /// Contract type
    /// </summary>
    [JsonProperty("ctType"), JsonConverter(typeof(ContractTypeConverter))]
    public OKXContractType? ContractType { get; set; }

    /// <summary>
    /// Alias
    /// </summary>
    [JsonProperty("alias"), JsonConverter(typeof(InstrumentAliasConverter))]
    public OKXInstrumentAlias? Alias { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(InstrumentStateConverter))]
    public OKXInstrumentState state { get; set; }

    /// <summary>
    /// The maximum order quantity of the contract or spot limit order.
    /// </summary>
    [JsonProperty("maxLmtSz")]
    public decimal? MaxLimitQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot market order.
    /// </summary>
    [JsonProperty("maxMktSz")]
    public decimal? MaxMarketQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot twap order.
    /// </summary>
    [JsonProperty("maxTwapSz")]
    public decimal? MaxTwapQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot iceBerg order.
    /// </summary>
    [JsonProperty("maxIcebergSz")]
    public decimal? MaxIcebergQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot trigger order.
    /// </summary>
    [JsonProperty("maxTriggerSz")]
    public decimal? MaxTriggerQuantity { get; set; }
    /// <summary>
    /// The maximum order quantity of the contract or spot stop market order.
    /// </summary>
    [JsonProperty("maxStopSz")]
    public decimal? MaxStopQuantity { get; set; }
}
