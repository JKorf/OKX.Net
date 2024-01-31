namespace OKX.Net.Objects.Market;

/// <summary>
/// Index info
/// </summary>
public class OKXIndexComponents
{
    /// <summary>
    /// Last price
    /// </summary>
    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    [JsonProperty("index")]
    public string Index { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Components
    /// </summary>
    [JsonProperty("components")]
    public IEnumerable<OKXIndexComponent> Components { get; set; } = Array.Empty<OKXIndexComponent>();
}

/// <summary>
/// Index component
/// </summary>
public class OKXIndexComponent
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("symPx")]
    public decimal Price { get; set; }

    /// <summary>
    /// Weight
    /// </summary>
    [JsonProperty("wgt")]
    public decimal Weight { get; set; }

    /// <summary>
    /// Convert price
    /// </summary>
    [JsonProperty("cnvPx")]
    public decimal ConvertPrice { get; set; }

    /// <summary>
    /// Exchange
    /// </summary>
    [JsonProperty("exch")]
    public string Exchange { get; set; } = string.Empty;
}
