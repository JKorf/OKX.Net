using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Option summary
/// </summary>
public class OKXOptionSummary
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Delta
    /// </summary>
    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// Delta BS
    /// </summary>
    [JsonProperty("deltaBS")]
    public decimal DeltaBS { get; set; }

    /// <summary>
    /// Gamme BS
    /// </summary>
    [JsonProperty("gammaBS")]
    public decimal GammaBS { get; set; }

    /// <summary>
    /// Vega BS
    /// </summary>
    [JsonProperty("vegaBS")]
    public decimal VegaBS { get; set; }

    /// <summary>
    /// Theta BS
    /// </summary>
    [JsonProperty("thetaBS")]
    public decimal ThetaBS { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Mark volatility
    /// </summary>
    [JsonProperty("markVol")]
    public decimal? MarkVolatility { get; set; }

    /// <summary>
    /// Bit volatitlity
    /// </summary>
    [JsonProperty("bidVol")]
    public decimal? BidVolatility { get; set; }

    /// <summary>
    /// Ask volatility
    /// </summary>
    [JsonProperty("askVol")]
    public decimal? AskVolatility { get; set; }

    /// <summary>
    /// Real volatitlity
    /// </summary>
    [JsonProperty("realVol")]
    public decimal? RealVolatility { get; set; }

    /// <summary>
    /// Implied volatility of at-the-money options
    /// </summary>
    [JsonProperty("volLv")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// Forward price
    /// </summary>
    [JsonProperty("fwdPx")]
    public decimal? ForwardPrice { get; set; }
}
