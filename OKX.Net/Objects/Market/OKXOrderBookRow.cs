using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Market;

/// <summary>
/// Order book row
/// </summary>
[JsonConverter(typeof(ArrayConverter<OKXOrderBookRow>))]
[SerializationModel]
public record OKXOrderBookRow : ISymbolOrderBookEntry
{
    /// <summary>
    /// The price for this row
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity for this row
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }

    /// <summary>
    /// The number of liquidated orders at the price
    /// </summary>
    [ArrayProperty(2)]
    public decimal LiquidatedOrders { get; set; }

    /// <summary>
    /// The number of orders at the price
    /// </summary>
    [ArrayProperty(3)]
    public decimal OrdersCount { get; set; }
}
