using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Fee rate
/// </summary>
[SerializationModel]
public record OKXFeeRate
{
    /// <summary>
    /// Category
    /// </summary>
    [JsonPropertyName("category")]
    public FeeRateCategory? Category { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Maker
    /// </summary>
    [JsonPropertyName("maker")]
    public decimal? Maker { get; set; }

    /// <summary>
    /// Maker fee rate of USDT-margined contracts
    /// </summary>
    [JsonPropertyName("makerU")]
    public decimal? MakerUsdtMarginContracts { get; set; }

    /// <summary>
    /// Taker fee rate of USDT-margined contracts
    /// </summary>
    [JsonPropertyName("takerU")]
    public decimal? TakerUsdtMarginContracts { get; set; }

    /// <summary>
    /// Maker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonPropertyName("makerUSDC")]
    public decimal? MakerFeeUsdc { get; set; }

    /// <summary>
    /// Taker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonPropertyName("takerUSDC")]
    public decimal? TakerFeeUsdc { get; set; }

    /// <summary>
    /// Taker
    /// </summary>
    [JsonPropertyName("taker")]
    public decimal? Taker { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Delivery
    /// </summary>
    [JsonPropertyName("delivery")]
    public decimal? Delivery { get; set; }

    /// <summary>
    /// Exercise
    /// </summary>
    [JsonPropertyName("exercise")]
    public decimal? Exercise { get; set; }

    /// <summary>
    /// Rule type this applies to
    /// </summary>
    [JsonPropertyName("ruleType")]
    public SymbolRuleType? RuleType { get; set; }

    /// <summary>
    /// Fiat fees
    /// </summary>
    [JsonPropertyName("fiat")]
    public OKXFiatFee[] Fiat { get; set; } = Array.Empty<OKXFiatFee>();
}

/// <summary>
/// Fiat fee rate
/// </summary>
[SerializationModel]
public record OKXFiatFee
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Taker fee
    /// </summary>
    [JsonPropertyName("taker")]
    public decimal TakerFeeRate { get; set; }
    /// <summary>
    /// Maker fee
    /// </summary>
    [JsonPropertyName("maker")]
    public decimal MakerFeeRate { get; set; }
}
