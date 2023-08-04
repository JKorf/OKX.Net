namespace OKX.Net.Objects.Public;

/// <summary>
/// Interest rate
/// </summary>
public class OKXInterestRate
{
    /// <summary>
    /// Basic
    /// </summary>
    [JsonProperty("basic")]
    public IEnumerable<OKXPublicInterestRateBasic> Basic { get; set; } = Array.Empty<OKXPublicInterestRateBasic>();

    /// <summary>
    /// VIP
    /// </summary>
    [JsonProperty("vip")]
    public IEnumerable<OKXPublicInterestRateVip> Vip { get; set; } = Array.Empty<OKXPublicInterestRateVip>();

    /// <summary>
    /// Regular
    /// </summary>
    [JsonProperty("regular")]
    public IEnumerable<OKXPublicInterestRateRegular> regular { get; set; } = Array.Empty<OKXPublicInterestRateRegular>();

}

/// <summary>
/// Basic interest rate
/// </summary>
public class OKXPublicInterestRateBasic
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quota
    /// </summary>
    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }
}

/// <summary>
/// VIP interest rate
/// </summary>
public class OKXPublicInterestRateVip
{
    /// <summary>
    /// Interest rate discount
    /// </summary>
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// Loan quota coef
    /// </summary>
    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;
}

/// <summary>
/// Regular interest rate
/// </summary>
public class OKXPublicInterestRateRegular
{
    /// <summary>
    /// Interest rate discount
    /// </summary>
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// Loan quota coef
    /// </summary>
    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;
}
