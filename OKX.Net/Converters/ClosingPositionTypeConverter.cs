using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class ClosingPositionTypeConverter : BaseConverter<OKXClosingPositionType>
{
    public ClosingPositionTypeConverter() : this(true) { }
    public ClosingPositionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXClosingPositionType, string>> Mapping => new List<KeyValuePair<OKXClosingPositionType, string>>
        {
            new KeyValuePair<OKXClosingPositionType, string>(OKXClosingPositionType.ClosePartially, "1"),
            new KeyValuePair<OKXClosingPositionType, string>(OKXClosingPositionType.CloseAll, "2"),
            new KeyValuePair<OKXClosingPositionType, string>(OKXClosingPositionType.Liquidation, "3"),
            new KeyValuePair<OKXClosingPositionType, string>(OKXClosingPositionType.PartialLiquidation, "4"),
            new KeyValuePair<OKXClosingPositionType, string>(OKXClosingPositionType.ADL, "5"),
        };
}