using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OrderTypeConverter : BaseConverter<OKXOrderType>
{
    public OrderTypeConverter() : this(true) { }
    public OrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOrderType, string>> Mapping => new List<KeyValuePair<OKXOrderType, string>>
    {
        new KeyValuePair<OKXOrderType, string>(OKXOrderType.MarketOrder, "market"),
        new KeyValuePair<OKXOrderType, string>(OKXOrderType.LimitOrder, "limit"),
        new KeyValuePair<OKXOrderType, string>(OKXOrderType.PostOnly, "post_only"),
        new KeyValuePair<OKXOrderType, string>(OKXOrderType.FillOrKill, "fok"),
        new KeyValuePair<OKXOrderType, string>(OKXOrderType.ImmediateOrCancel, "ioc"),
        new KeyValuePair<OKXOrderType, string>(OKXOrderType.OptimalLimitOrder, "optimal_limit_ioc"),
    };
}