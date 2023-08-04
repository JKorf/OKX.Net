using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AlgoActualSideConverter : BaseConverter<OKXAlgoActualSide>
{
    public AlgoActualSideConverter() : this(true) { }
    public AlgoActualSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAlgoActualSide, string>> Mapping => new List<KeyValuePair<OKXAlgoActualSide, string>>
    {
        new KeyValuePair<OKXAlgoActualSide, string>(OKXAlgoActualSide.StopLoss, "sl"),
        new KeyValuePair<OKXAlgoActualSide, string>(OKXAlgoActualSide.TakeProfit, "tp"),
    };
}