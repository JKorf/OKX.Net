using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OrderFlowTypeConverter : BaseConverter<OKXOrderFlowType>
{
    public OrderFlowTypeConverter() : this(true) { }
    public OrderFlowTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOrderFlowType, string>> Mapping => new List<KeyValuePair<OKXOrderFlowType, string>>
    {
        new KeyValuePair<OKXOrderFlowType, string>(OKXOrderFlowType.Taker, "T"),
        new KeyValuePair<OKXOrderFlowType, string>(OKXOrderFlowType.Maker, "M"),
    };
}