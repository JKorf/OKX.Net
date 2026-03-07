using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Fee rate
/// </summary>
[SerializationModel]
public record OKXFeeRate
{
    /// <summary>
    /// ["<c>category</c>"] Category
    /// </summary>
    [JsonPropertyName("category")]
    public FeeRateCategory? Category { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>level</c>"] Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>maker</c>"] Maker
    /// </summary>
    [JsonPropertyName("maker")]
    public decimal? Maker { get; set; }

    /// <summary>
    /// ["<c>makerU</c>"] Maker fee rate of USDT-margined contracts
    /// </summary>
    [JsonPropertyName("makerU")]
    public decimal? MakerUsdtMarginContracts { get; set; }

    /// <summary>
    /// ["<c>takerU</c>"] Taker fee rate of USDT-margined contracts
    /// </summary>
    [JsonPropertyName("takerU")]
    public decimal? TakerUsdtMarginContracts { get; set; }

    /// <summary>
    /// ["<c>makerUSDC</c>"] Maker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonPropertyName("makerUSDC")]
    public decimal? MakerFeeUsdc { get; set; }

    /// <summary>
    /// ["<c>takerUSDC</c>"] Taker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonPropertyName("takerUSDC")]
    public decimal? TakerFeeUsdc { get; set; }

    /// <summary>
    /// ["<c>taker</c>"] Taker
    /// </summary>
    [JsonPropertyName("taker")]
    public decimal? Taker { get; set; }

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>delivery</c>"] Delivery
    /// </summary>
    [JsonPropertyName("delivery")]
    public decimal? Delivery { get; set; }

    /// <summary>
    /// ["<c>exercise</c>"] Exercise
    /// </summary>
    [JsonPropertyName("exercise")]
    public decimal? Exercise { get; set; }

    /// <summary>
    /// ["<c>ruleType</c>"] Rule type this applies to
    /// </summary>
    [JsonPropertyName("ruleType")]
    public SymbolRuleType? RuleType { get; set; }

    /// <summary>
    /// ["<c>fiat</c>"] Fiat fees
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
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>taker</c>"] Taker fee
    /// </summary>
    [JsonPropertyName("taker")]
    public decimal TakerFeeRate { get; set; }
    /// <summary>
    /// ["<c>maker</c>"] Maker fee
    /// </summary>
    [JsonPropertyName("maker")]
    public decimal MakerFeeRate { get; set; }
}
