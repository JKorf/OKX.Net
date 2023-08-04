using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class MarginModeConverter : BaseConverter<OKXMarginMode>
{
    public MarginModeConverter() : this(true) { }
    public MarginModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXMarginMode, string>> Mapping => new List<KeyValuePair<OKXMarginMode, string>>
    {
        new KeyValuePair<OKXMarginMode, string>(OKXMarginMode.Isolated, "isolated"),
        new KeyValuePair<OKXMarginMode, string>(OKXMarginMode.Cross, "cross"),
    };
}