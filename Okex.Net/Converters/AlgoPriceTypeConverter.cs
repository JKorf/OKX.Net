using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AlgoPriceTypeConverter : BaseConverter<OKXAlgoPriceType>
{
    public AlgoPriceTypeConverter() : this(true) { }
    public AlgoPriceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAlgoPriceType, string>> Mapping => new List<KeyValuePair<OKXAlgoPriceType, string>>
    {
        new KeyValuePair<OKXAlgoPriceType, string>(OKXAlgoPriceType.Last, "last"),
        new KeyValuePair<OKXAlgoPriceType, string>(OKXAlgoPriceType.Index, "index"),
        new KeyValuePair<OKXAlgoPriceType, string>(OKXAlgoPriceType.Mark, "mark"),
    };
}