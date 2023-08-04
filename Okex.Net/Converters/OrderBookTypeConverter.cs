using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OrderBookTypeConverter : BaseConverter<OKXOrderBookType>
{
    public OrderBookTypeConverter() : this(true) { }
    public OrderBookTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOrderBookType, string>> Mapping => new List<KeyValuePair<OKXOrderBookType, string>>
    {
        new KeyValuePair<OKXOrderBookType, string>(OKXOrderBookType.OrderBook, "books"),
        new KeyValuePair<OKXOrderBookType, string>(OKXOrderBookType.OrderBook_5, "books5"),
        new KeyValuePair<OKXOrderBookType, string>(OKXOrderBookType.OrderBook_50_l2_TBT, "books50-l2-tbt"),
        new KeyValuePair<OKXOrderBookType, string>(OKXOrderBookType.OrderBook_l2_TBT, "books-l2-tbt"),
        new KeyValuePair<OKXOrderBookType, string>(OKXOrderBookType.BBO_TBT, "bbo-tbt"),
    };
}