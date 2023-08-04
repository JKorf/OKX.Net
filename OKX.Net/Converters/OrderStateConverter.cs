using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OrderStateConverter : BaseConverter<OKXOrderState>
{
    public OrderStateConverter() : this(true) { }
    public OrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOrderState, string>> Mapping => new List<KeyValuePair<OKXOrderState, string>>
    {
        new KeyValuePair<OKXOrderState, string>(OKXOrderState.Live, "live"),
        new KeyValuePair<OKXOrderState, string>(OKXOrderState.Canceled, "canceled"),
        new KeyValuePair<OKXOrderState, string>(OKXOrderState.PartiallyFilled, "partially_filled"),
        new KeyValuePair<OKXOrderState, string>(OKXOrderState.Filled, "filled"),
    };
}