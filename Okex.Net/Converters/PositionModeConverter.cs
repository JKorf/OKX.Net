using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class PositionModeConverter : BaseConverter<OKXPositionMode>
{
    public PositionModeConverter() : this(true) { }
    public PositionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXPositionMode, string>> Mapping => new List<KeyValuePair<OKXPositionMode, string>>
    {
        new KeyValuePair<OKXPositionMode, string>(OKXPositionMode.LongShortMode, "long_short_mode"),
        new KeyValuePair<OKXPositionMode, string>(OKXPositionMode.NetMode, "net_mode"),
    };
}