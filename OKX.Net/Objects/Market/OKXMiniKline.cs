using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Market;

/// <summary>
/// Candlestick/Kline data
/// </summary>
[JsonConverter(typeof(ArrayConverter<OKXMiniKline>))]
[SerializationModel]
public record OKXMiniKline
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Open price
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// Highest price
    /// </summary>
    [ArrayProperty(2)]
    public decimal HighPrice { get; set; }

    /// <summary>
    /// Lowest price
    /// </summary>
    [ArrayProperty(3)]
    public decimal LowPrice { get; set; }

    /// <summary>
    /// Close price
    /// </summary>
    [ArrayProperty(4)]
    public decimal ClosePrice { get; set; }

    /// <summary>
    /// The state of candlesticks.
    /// false represents that it is uncompleted, true represents that it is completed.
    /// </summary>
    [ArrayProperty(8), JsonConverter(typeof(BoolConverter))]
    public bool Confirm { get; set; }
}
