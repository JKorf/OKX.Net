using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Market;

/// <summary>
/// Index info
/// </summary>
[SerializationModel]
public record OKXIndexComponents
{
    /// <summary>
    /// Last price
    /// </summary>
    [JsonPropertyName("last")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    [JsonPropertyName("index")]
    public string Index { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Components
    /// </summary>
    [JsonPropertyName("components")]
    public OKXIndexComponent[] Components { get; set; } = Array.Empty<OKXIndexComponent>();
}

/// <summary>
/// Index component
/// </summary>
[SerializationModel]
public record OKXIndexComponent
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("symPx")]
    public decimal Price { get; set; }

    /// <summary>
    /// Weight
    /// </summary>
    [JsonPropertyName("wgt")]
    public decimal Weight { get; set; }

    /// <summary>
    /// Convert price
    /// </summary>
    [JsonPropertyName("cnvPx")]
    public decimal ConvertPrice { get; set; }

    /// <summary>
    /// Exchange
    /// </summary>
    [JsonPropertyName("exch")]
    public string Exchange { get; set; } = string.Empty;
}
