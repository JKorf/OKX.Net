using OKX.Net.Converters;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Candlestick
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class OKXCandlestick
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonIgnore]
    public string Symbol { get; set; } = string.Empty;

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
    /// Trading volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal Volume { get; set; }

    /// <summary>
    /// Quote volume
    /// </summary>
    [ArrayProperty(6)]
    public decimal QuoteVolume { get; set; }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        // Check for null and compare run-time types.
        if (obj == null || !GetType().Equals(obj.GetType()))
            return false;

        // Equal Check
        var stick = (OKXCandlestick)obj;
        return Time == stick.Time
            && Symbol == stick.Symbol
            && OpenPrice == stick.OpenPrice
            && QuoteVolume == stick.QuoteVolume
            && QuoteVolume == stick.QuoteVolume
            && QuoteVolume == stick.QuoteVolume
            && Volume == stick.Volume
            && QuoteVolume == stick.QuoteVolume;
    }
}
