using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class TransferTypeConverter : BaseConverter<OKXTransferType>
{
    public TransferTypeConverter() : this(true) { }
    public TransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXTransferType, string>> Mapping => new List<KeyValuePair<OKXTransferType, string>>
    {
        new KeyValuePair<OKXTransferType, string>(OKXTransferType.TransferWithinAccount, "0"),
        new KeyValuePair<OKXTransferType, string>(OKXTransferType.MasterAccountToSubAccount, "1"),
        new KeyValuePair<OKXTransferType, string>(OKXTransferType.SubAccountToMasterAccount, "2"),
    };
}