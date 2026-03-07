using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Option summary
/// </summary>
[SerializationModel]
public record OKXOptionSummary
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>uly</c>"] Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>delta</c>"] Delta
    /// </summary>
    [JsonPropertyName("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// ["<c>gamma</c>"] Gamma
    /// </summary>
    [JsonPropertyName("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// ["<c>vega</c>"] Vega
    /// </summary>
    [JsonPropertyName("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// ["<c>theta</c>"] Theta
    /// </summary>
    [JsonPropertyName("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// ["<c>deltaBS</c>"] Delta BS
    /// </summary>
    [JsonPropertyName("deltaBS")]
    public decimal DeltaBS { get; set; }

    /// <summary>
    /// ["<c>gammaBS</c>"] Gamme BS
    /// </summary>
    [JsonPropertyName("gammaBS")]
    public decimal GammaBS { get; set; }

    /// <summary>
    /// ["<c>vegaBS</c>"] Vega BS
    /// </summary>
    [JsonPropertyName("vegaBS")]
    public decimal VegaBS { get; set; }

    /// <summary>
    /// ["<c>thetaBS</c>"] Theta BS
    /// </summary>
    [JsonPropertyName("thetaBS")]
    public decimal ThetaBS { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// ["<c>markVol</c>"] Mark volatility
    /// </summary>
    [JsonPropertyName("markVol")]
    public decimal? MarkVolatility { get; set; }

    /// <summary>
    /// ["<c>bidVol</c>"] Bit volatitlity
    /// </summary>
    [JsonPropertyName("bidVol")]
    public decimal? BidVolatility { get; set; }

    /// <summary>
    /// ["<c>askVol</c>"] Ask volatility
    /// </summary>
    [JsonPropertyName("askVol")]
    public decimal? AskVolatility { get; set; }

    /// <summary>
    /// ["<c>realVol</c>"] Real volatitlity
    /// </summary>
    [JsonPropertyName("realVol")]
    public decimal? RealVolatility { get; set; }

    /// <summary>
    /// ["<c>volLv</c>"] Implied volatility of at-the-money options
    /// </summary>
    [JsonPropertyName("volLv")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// ["<c>fwdPx</c>"] Forward price
    /// </summary>
    [JsonPropertyName("fwdPx")]
    public decimal? ForwardPrice { get; set; }
}
