using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OrderCategoryConverter : BaseConverter<OKXOrderCategory>
{
    public OrderCategoryConverter() : this(true) { }
    public OrderCategoryConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOrderCategory, string>> Mapping => new List<KeyValuePair<OKXOrderCategory, string>>
    {
        new KeyValuePair<OKXOrderCategory, string>(OKXOrderCategory.TWAP, "twap"),
        new KeyValuePair<OKXOrderCategory, string>(OKXOrderCategory.ADL, "adl"),
        new KeyValuePair<OKXOrderCategory, string>(OKXOrderCategory.FullLiquidation, "full_liquidation"),
        new KeyValuePair<OKXOrderCategory, string>(OKXOrderCategory.PartialLiquidation, "partial_liquidation"),
        new KeyValuePair<OKXOrderCategory, string>(OKXOrderCategory.Delivery, "delivery"),
    };
}