namespace OKX.Net.Objects.Public;

/// <summary>
/// Interest rate
/// </summary>
[SerializationModel]
public record OKXInterestRate
{
    /// <summary>
    /// ["<c>basic</c>"] Basic
    /// </summary>
    [JsonPropertyName("basic")]
    public OKXPublicInterestRateBasic[] Basic { get; set; } = Array.Empty<OKXPublicInterestRateBasic>();

    /// <summary>
    /// ["<c>vip</c>"] VIP
    /// </summary>
    [JsonPropertyName("vip")]
    public OKXPublicInterestRateVip[] Vip { get; set; } = Array.Empty<OKXPublicInterestRateVip>();

    /// <summary>
    /// ["<c>regular</c>"] Regular
    /// </summary>
    [JsonPropertyName("regular")]
    public OKXPublicInterestRateRegular[] regular { get; set; } = Array.Empty<OKXPublicInterestRateRegular>();

}

/// <summary>
/// Basic interest rate
/// </summary>
[SerializationModel]
public record OKXPublicInterestRateBasic
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>quota</c>"] Quota
    /// </summary>
    [JsonPropertyName("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// ["<c>rate</c>"] Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? Rate { get; set; }
}

/// <summary>
/// VIP interest rate
/// </summary>
[SerializationModel]
public record OKXPublicInterestRateVip
{
    /// <summary>
    /// ["<c>irDiscount</c>"] Interest rate discount
    /// </summary>
    [JsonPropertyName("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// ["<c>loanQuotaCoef</c>"] Loan quota coef
    /// </summary>
    [JsonPropertyName("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    /// <summary>
    /// ["<c>level</c>"] Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
}

/// <summary>
/// Regular interest rate
/// </summary>
[SerializationModel]
public record OKXPublicInterestRateRegular
{
    /// <summary>
    /// ["<c>irDiscount</c>"] Interest rate discount
    /// </summary>
    [JsonPropertyName("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// ["<c>loanQuotaCoef</c>"] Loan quota coef
    /// </summary>
    [JsonPropertyName("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    /// <summary>
    /// ["<c>level</c>"] Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
}
