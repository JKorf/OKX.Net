using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AlgoOrderTypeConverter : BaseConverter<OKXAlgoOrderType>
{
    public AlgoOrderTypeConverter() : this(true) { }
    public AlgoOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAlgoOrderType, string>> Mapping => new List<KeyValuePair<OKXAlgoOrderType, string>>
    {
        new KeyValuePair<OKXAlgoOrderType, string>(OKXAlgoOrderType.Conditional, "conditional"),
        new KeyValuePair<OKXAlgoOrderType, string>(OKXAlgoOrderType.OCO, "oco"),
        new KeyValuePair<OKXAlgoOrderType, string>(OKXAlgoOrderType.Trigger, "trigger"),
        new KeyValuePair<OKXAlgoOrderType, string>(OKXAlgoOrderType.TrailingOrder, "move_order_stop"),
        new KeyValuePair<OKXAlgoOrderType, string>(OKXAlgoOrderType.Iceberg, "iceberg"),
        new KeyValuePair<OKXAlgoOrderType, string>(OKXAlgoOrderType.TWAP, "twap"),
    };
}