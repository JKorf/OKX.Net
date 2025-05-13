using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Market;

/// <summary>
/// Candlestick
/// </summary>
[JsonConverter(typeof(ArrayConverter<OKXKline>))]
[SerializationModel]
public record OKXKline
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
    [ArrayProperty(8), JsonConverter(typeof(BoolConverter))]
    public bool Confirm { get; set; }
}
