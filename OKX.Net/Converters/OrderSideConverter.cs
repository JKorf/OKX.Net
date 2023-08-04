using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OrderSideConverter : BaseConverter<OKXOrderSide>
{
    public OrderSideConverter() : this(true) { }
    public OrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOrderSide, string>> Mapping => new List<KeyValuePair<OKXOrderSide, string>>
    {
        new KeyValuePair<OKXOrderSide, string>(OKXOrderSide.Buy, "buy"),
        new KeyValuePair<OKXOrderSide, string>(OKXOrderSide.Sell, "sell"),
    };
}