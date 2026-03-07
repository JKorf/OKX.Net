namespace OKX.Net.Objects.Market;

/// <summary>
/// Index info
/// </summary>
[SerializationModel]
public record OKXIndexComponents
{
    /// <summary>
    /// ["<c>last</c>"] Last price
    /// </summary>
    [JsonPropertyName("last")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// ["<c>index</c>"] Index
    /// </summary>
    [JsonPropertyName("index")]
    public string Index { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>components</c>"] Components
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
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>symPx</c>"] Price
    /// </summary>
    [JsonPropertyName("symPx")]
    public decimal Price { get; set; }

    /// <summary>
    /// ["<c>wgt</c>"] Weight
    /// </summary>
    [JsonPropertyName("wgt")]
    public decimal Weight { get; set; }

    /// <summary>
    /// ["<c>cnvPx</c>"] Convert price
    /// </summary>
    [JsonPropertyName("cnvPx")]
    public decimal ConvertPrice { get; set; }

    /// <summary>
    /// ["<c>exch</c>"] Exchange
    /// </summary>
    [JsonPropertyName("exch")]
    public string Exchange { get; set; } = string.Empty;
}
