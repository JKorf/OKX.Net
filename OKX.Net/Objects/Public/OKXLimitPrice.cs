using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Limit price
/// </summary>
[SerializationModel]
public record OKXLimitPrice
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
    /// Buy limit
    /// </summary>
    [JsonPropertyName("buyLmt")]
    public decimal BuyLimit { get; set; }

    /// <summary>
    /// Sell limit
    /// </summary>
    [JsonPropertyName("sellLmt")]
    public decimal SellLimit { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Whether price limit is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool IsEnabled { get; set; }
}
