using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Option summary
/// </summary>
[SerializationModel]
public record OKXOptionSummary
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Delta
    /// </summary>
    [JsonPropertyName("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonPropertyName("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonPropertyName("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonPropertyName("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// Delta BS
    /// </summary>
    [JsonPropertyName("deltaBS")]
    public decimal DeltaBS { get; set; }

    /// <summary>
    /// Gamme BS
    /// </summary>
    [JsonPropertyName("gammaBS")]
    public decimal GammaBS { get; set; }

    /// <summary>
    /// Vega BS
    /// </summary>
    [JsonPropertyName("vegaBS")]
    public decimal VegaBS { get; set; }

    /// <summary>
    /// Theta BS
    /// </summary>
    [JsonPropertyName("thetaBS")]
    public decimal ThetaBS { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Mark volatility
    /// </summary>
    [JsonPropertyName("markVol")]
    public decimal? MarkVolatility { get; set; }

    /// <summary>
    /// Bit volatitlity
    /// </summary>
    [JsonPropertyName("bidVol")]
    public decimal? BidVolatility { get; set; }

    /// <summary>
    /// Ask volatility
    /// </summary>
    [JsonPropertyName("askVol")]
    public decimal? AskVolatility { get; set; }

    /// <summary>
    /// Real volatitlity
    /// </summary>
    [JsonPropertyName("realVol")]
    public decimal? RealVolatility { get; set; }

    /// <summary>
    /// Implied volatility of at-the-money options
    /// </summary>
    [JsonPropertyName("volLv")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// Forward price
    /// </summary>
    [JsonPropertyName("fwdPx")]
    public decimal? ForwardPrice { get; set; }
}
