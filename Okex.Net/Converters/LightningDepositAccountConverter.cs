using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class LightningDepositAccountConverter : BaseConverter<OKXLightningDepositAccount>
{
    public LightningDepositAccountConverter() : this(true) { }
    public LightningDepositAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXLightningDepositAccount, string>> Mapping => new List<KeyValuePair<OKXLightningDepositAccount, string>>
    {
        new KeyValuePair<OKXLightningDepositAccount, string>(OKXLightningDepositAccount.Spot, "1"),
        new KeyValuePair<OKXLightningDepositAccount, string>(OKXLightningDepositAccount.Funding, "6"),
    };
}