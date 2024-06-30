using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Fee rate
/// </summary>
public record OKXFeeRate
{
    /// <summary>
    /// Category
    /// </summary>
    [JsonProperty("category"), JsonConverter(typeof(FeeRateCategoryConverter))]
    public OKXFeeRateCategory? Category { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Maker
    /// </summary>
    [JsonProperty("maker")]
    public decimal? Maker { get; set; }

    /// <summary>
    /// Maker fee rate of USDT-margined contracts
    /// </summary>
    [JsonProperty("makerU")]
    public decimal? MakerUsdtMarginContracts { get; set; }

    /// <summary>
    /// Taker fee rate of USDT-margined contracts
    /// </summary>
    [JsonProperty("takerU")]
    public decimal? TakerUsdtMarginContracts { get; set; }

    /// <summary>
    /// Maker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonProperty("makerUSDC")]
    public decimal? MakerFeeUsdc { get; set; }

    /// <summary>
    /// Taker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonProperty("takerUSDC")]
    public decimal? TakerFeeUsdc { get; set; }

    /// <summary>
    /// Taker
    /// </summary>
    [JsonProperty("taker")]
    public decimal? Taker { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Delivery
    /// </summary>
    [JsonProperty("delivery")]
    public decimal? Delivery { get; set; }

    /// <summary>
    /// Exercise
    /// </summary>
    [JsonProperty("exercise")]
    public decimal? Exercise { get; set; }
    /// <summary>
    /// Fiat fees
    /// </summary>
    [JsonProperty("fiat")]
    public IEnumerable<OKXFiatFee> Fiat { get; set; } = Array.Empty<OKXFiatFee>();
}

/// <summary>
/// Fiat fee rate
/// </summary>
public record OKXFiatFee
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Taker fee
    /// </summary>
    [JsonProperty("taker")]
    public decimal TakerFeeRate { get; set; }
    /// <summary>
    /// Maker fee
    /// </summary>
    [JsonProperty("maker")]
    public decimal MakerFeeRate { get; set; }
}
