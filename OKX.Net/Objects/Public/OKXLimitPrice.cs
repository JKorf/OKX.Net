using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Limit price
/// </summary>
[SerializationModel]
public record OKXLimitPrice
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
    /// ["<c>buyLmt</c>"] Buy limit
    /// </summary>
    [JsonPropertyName("buyLmt")]
    public decimal BuyLimit { get; set; }

    /// <summary>
    /// ["<c>sellLmt</c>"] Sell limit
    /// </summary>
    [JsonPropertyName("sellLmt")]
    public decimal SellLimit { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>enabled</c>"] Whether price limit is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool IsEnabled { get; set; }
}
