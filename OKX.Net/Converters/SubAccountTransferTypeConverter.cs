using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class SubAccountTransferTypeConverter : BaseConverter<OKXSubAccountTransferType>
{
    public SubAccountTransferTypeConverter() : this(true) { }
    public SubAccountTransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXSubAccountTransferType, string>> Mapping => new List<KeyValuePair<OKXSubAccountTransferType, string>>
    {
        new KeyValuePair<OKXSubAccountTransferType, string>(OKXSubAccountTransferType.FromMasterAccountToSubAccout, "0s"),
        new KeyValuePair<OKXSubAccountTransferType, string>(OKXSubAccountTransferType.FromSubAccountToMasterAccout, "1"),
    };
}