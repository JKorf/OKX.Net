using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class SavingActionSideConverter : BaseConverter<OKXSavingActionSide>
{
    public SavingActionSideConverter() : this(true) { }
    public SavingActionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXSavingActionSide, string>> Mapping => new List<KeyValuePair<OKXSavingActionSide, string>>
    {
        new KeyValuePair<OKXSavingActionSide, string>(OKXSavingActionSide.Purchase, "purchase"),
        new KeyValuePair<OKXSavingActionSide, string>(OKXSavingActionSide.Redempt, "redempt"),
    };
}