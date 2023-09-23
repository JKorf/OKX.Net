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
    /// Trading volume, with a unit of contract.
    /// If it is a derivatives contract, the value is the number of contracts.
    /// If it is SPOT/MARGIN, the value is the quantity in base currency.
    /// </summary>
    [ArrayProperty(5)]
    public decimal Volume { get; set; }

    /// <summary>
    /// Trading volume, with a unit of currency.
    /// If it is a derivatives contract, the value is the number of base currency.
    /// If it is SPOT/MARGIN, the value is the quantity in quote currency.
    /// </summary>
    [ArrayProperty(6)]
    public decimal VolumeCurrency { get; set; }
    
    /// <summary>
    /// Trading volume, the value is the quantity in quote currency
    /// e.g. The unit is USDT for BTC-USDT and BTC-USDT-SWAP;
    /// The unit is USD for BTC-USD-SWAP
    /// </summary>
    [ArrayProperty(7)]
    public decimal VolumeCurrencyQuote { get; set; }

    /// <summary>
    /// The state of candlesticks.
    /// false represents that it is uncompleted, true represents that it is completed.
    /// </summary>
    [ArrayProperty(8), JsonConverter(typeof(OKXBooleanConverter))]
    public bool Confirm { get; set; }

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
            && HighPrice == stick.HighPrice
            && LowPrice == stick.LowPrice
            && ClosePrice == stick.ClosePrice
            && Volume == stick.Volume
            && VolumeCurrency == stick.VolumeCurrency
            && VolumeCurrencyQuote == stick.VolumeCurrencyQuote
            && Confirm == stick.Confirm;
    }
}
