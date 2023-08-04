using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AlgoOrderStateConverter : BaseConverter<OKXAlgoOrderState>
{
    public AlgoOrderStateConverter() : this(true) { }
    public AlgoOrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAlgoOrderState, string>> Mapping => new List<KeyValuePair<OKXAlgoOrderState, string>>
    {
        new KeyValuePair<OKXAlgoOrderState, string>(OKXAlgoOrderState.Live, "live"),
        new KeyValuePair<OKXAlgoOrderState, string>(OKXAlgoOrderState.Pause, "pause"),
        new KeyValuePair<OKXAlgoOrderState, string>(OKXAlgoOrderState.Effective, "effective"),
        new KeyValuePair<OKXAlgoOrderState, string>(OKXAlgoOrderState.PartiallyEffective, "partially_effective"),
        new KeyValuePair<OKXAlgoOrderState, string>(OKXAlgoOrderState.Canceled, "canceled"),
        new KeyValuePair<OKXAlgoOrderState, string>(OKXAlgoOrderState.Failed, "order_failed"),
    };
}