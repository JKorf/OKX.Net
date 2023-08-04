using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class TradeModeConverter : BaseConverter<OKXTradeMode>
{
    public TradeModeConverter() : this(true) { }
    public TradeModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXTradeMode, string>> Mapping => new List<KeyValuePair<OKXTradeMode, string>>
    {
        new KeyValuePair<OKXTradeMode, string>(OKXTradeMode.Cash, "cash"),
        new KeyValuePair<OKXTradeMode, string>(OKXTradeMode.Cross, "cross"),
        new KeyValuePair<OKXTradeMode, string>(OKXTradeMode.Isolated, "isolated"),
    };
}