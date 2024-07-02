namespace OKX.Net.Objects.Public;

/// <summary>
/// Interest rate
/// </summary>
public record OKXInterestRate
{
    /// <summary>
    /// Basic
    /// </summary>
    [JsonPropertyName("basic")]
    public IEnumerable<OKXPublicInterestRateBasic> Basic { get; set; } = Array.Empty<OKXPublicInterestRateBasic>();

    /// <summary>
    /// VIP
    /// </summary>
    [JsonPropertyName("vip")]
    public IEnumerable<OKXPublicInterestRateVip> Vip { get; set; } = Array.Empty<OKXPublicInterestRateVip>();

    /// <summary>
    /// Regular
    /// </summary>
    [JsonPropertyName("regular")]
    public IEnumerable<OKXPublicInterestRateRegular> regular { get; set; } = Array.Empty<OKXPublicInterestRateRegular>();

}

/// <summary>
/// Basic interest rate
/// </summary>
public record OKXPublicInterestRateBasic
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quota
    /// </summary>
    [JsonPropertyName("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? Rate { get; set; }
}

/// <summary>
/// VIP interest rate
/// </summary>
public record OKXPublicInterestRateVip
{
    /// <summary>
    /// Interest rate discount
    /// </summary>
    [JsonPropertyName("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// Loan quota coef
    /// </summary>
    [JsonPropertyName("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
}

/// <summary>
/// Regular interest rate
/// </summary>
public record OKXPublicInterestRateRegular
{
    /// <summary>
    /// Interest rate discount
    /// </summary>
    [JsonPropertyName("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// Loan quota coef
    /// </summary>
    [JsonPropertyName("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
}
