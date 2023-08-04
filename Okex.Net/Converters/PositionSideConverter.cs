using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class PositionSideConverter : BaseConverter<OKXPositionSide>
{
    public PositionSideConverter() : this(true) { }
    public PositionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXPositionSide, string>> Mapping => new List<KeyValuePair<OKXPositionSide, string>>
    {
        new KeyValuePair<OKXPositionSide, string>(OKXPositionSide.Long, "long"),
        new KeyValuePair<OKXPositionSide, string>(OKXPositionSide.Short, "short"),
        new KeyValuePair<OKXPositionSide, string>(OKXPositionSide.Net, "net"),
    };
}