using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class SubAccountTypeConverter : BaseConverter<OKXSubAccountType>
{
    public SubAccountTypeConverter() : this(true) { }
    public SubAccountTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXSubAccountType, string>> Mapping => new List<KeyValuePair<OKXSubAccountType, string>>
    {
        new KeyValuePair<OKXSubAccountType, string>(OKXSubAccountType.Standard, "1"),
        new KeyValuePair<OKXSubAccountType, string>(OKXSubAccountType.Custody, "2"),
    };
}