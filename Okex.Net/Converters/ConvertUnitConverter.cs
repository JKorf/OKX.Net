using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class ConvertUnitConverter : BaseConverter<OKXConvertUnit>
{
    public ConvertUnitConverter() : this(true) { }
    public ConvertUnitConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXConvertUnit, string>> Mapping => new List<KeyValuePair<OKXConvertUnit, string>>
    {
        new KeyValuePair<OKXConvertUnit, string>(OKXConvertUnit.Coin, "coin"),
        new KeyValuePair<OKXConvertUnit, string>(OKXConvertUnit.Usdt, "usdt"),
    };
}