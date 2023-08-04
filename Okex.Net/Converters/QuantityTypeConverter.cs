using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class QuantityTypeConverter : BaseConverter<OKXQuantityType>
{
    public QuantityTypeConverter() : this(true) { }
    public QuantityTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXQuantityType, string>> Mapping => new List<KeyValuePair<OKXQuantityType, string>>
    {
        new KeyValuePair<OKXQuantityType, string>(OKXQuantityType.BaseCurrency, "base_ccy"),
        new KeyValuePair<OKXQuantityType, string>(OKXQuantityType.QuoteCurrency, "quote_ccy"),
    };
}