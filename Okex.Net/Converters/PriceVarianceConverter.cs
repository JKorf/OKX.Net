using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class PriceVarianceConverter : BaseConverter<OKXPriceVariance>
{
    public PriceVarianceConverter() : this(true) { }
    public PriceVarianceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXPriceVariance, string>> Mapping => new List<KeyValuePair<OKXPriceVariance, string>>
    {
        new KeyValuePair<OKXPriceVariance, string>(OKXPriceVariance.Spread, "pxSpread"),
        new KeyValuePair<OKXPriceVariance, string>(OKXPriceVariance.Variance, "pxVar"),
    };
}