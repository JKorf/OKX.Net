using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Limit price
/// </summary>
public class OKXLimitPrice
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
    /// Buy limit
    /// </summary>
    [JsonProperty("buyLmt")]
    public decimal BuyLimit { get; set; }

    /// <summary>
    /// Sell limit
    /// </summary>
    [JsonProperty("sellLmt")]
    public decimal SellLimit { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
