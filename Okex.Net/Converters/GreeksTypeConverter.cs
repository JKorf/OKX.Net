using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class GreeksTypeConverter : BaseConverter<OKXGreeksType>
{
    public GreeksTypeConverter() : this(true) { }
    public GreeksTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXGreeksType, string>> Mapping => new List<KeyValuePair<OKXGreeksType, string>>
    {
        new KeyValuePair<OKXGreeksType, string>(OKXGreeksType.GreeksInCoins, "PA"),
        new KeyValuePair<OKXGreeksType, string>(OKXGreeksType.BlackScholesGreeksInDollars, "BS"),
    };
}