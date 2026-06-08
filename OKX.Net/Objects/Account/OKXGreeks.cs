using OKX.Net.Enums;
namespace OKX.Net.Objects.Account;

/// <summary>
/// Account greeks
/// </summary>
public record OKXGreeks
{
    /// <summary>
    /// ["<c>deltaBS</c>"] delta: Black-Scholes Greeks in dollars
    /// </summary>
    [JsonPropertyName("deltaBS")]
    public decimal? DeltaBS { get; set; }
    /// <summary>
    /// ["<c>deltaPA</c>"] delta: Greeks in coins
    /// </summary>
    [JsonPropertyName("deltaPA")]
    public decimal? DeltaPA { get; set; }
    /// <summary>
    /// ["<c>gammaBS</c>"] gamma: Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonPropertyName("gammaBS")]
    public decimal? GammaBS { get; set; }
    /// <summary>
    /// ["<c>gammaPA</c>"] gamma: Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonPropertyName("gammaPA")]
    public decimal? GammaPA { get; set; }
    /// <summary>
    /// ["<c>thetaBS</c>"] theta: Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonPropertyName("thetaBS")]
    public decimal? ThetaBS { get; set; }
    /// <summary>
    /// ["<c>thetaPA</c>"] theta: Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonPropertyName("thetaPA")]
    public decimal? ThetaPA { get; set; }
    /// <summary>
    /// ["<c>vegaBS</c>"] vega: Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonPropertyName("vegaBS")]
    public decimal? VegaBS { get; set; }
    /// <summary>
    /// ["<c>vegaPA</c>"] vega: Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonPropertyName("vegaPA")]
    public decimal? VegaPA { get; set; }
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>tx</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }

}
