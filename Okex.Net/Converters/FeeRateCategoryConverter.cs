using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class FeeRateCategoryConverter : BaseConverter<OKXFeeRateCategory>
{
    public FeeRateCategoryConverter() : this(true) { }
    public FeeRateCategoryConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXFeeRateCategory, string>> Mapping => new List<KeyValuePair<OKXFeeRateCategory, string>>
    {
        new KeyValuePair<OKXFeeRateCategory, string>(OKXFeeRateCategory.ClassA, "1"),
        new KeyValuePair<OKXFeeRateCategory, string>(OKXFeeRateCategory.ClassB, "2"),
        new KeyValuePair<OKXFeeRateCategory, string>(OKXFeeRateCategory.ClassC, "3"),
        new KeyValuePair<OKXFeeRateCategory, string>(OKXFeeRateCategory.ClassD, "4"),
    };
}