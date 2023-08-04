using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AccountConverter : BaseConverter<OKXAccount>
{
    public AccountConverter() : this(true) { }
    public AccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAccount, string>> Mapping => new()
    {
        //new KeyValuePair<OkexAccount, string>(OkexAccount.Spot, "1"),
        //new KeyValuePair<OkexAccount, string>(OkexAccount.Futures, "3"),
        //new KeyValuePair<OkexAccount, string>(OkexAccount.Margin, "5"),
        new KeyValuePair<OKXAccount, string>(OKXAccount.Funding, "6"),
        //new KeyValuePair<OkexAccount, string>(OkexAccount.Swap, "9"),
        //new KeyValuePair<OkexAccount, string>(OkexAccount.Option, "12"),
        new KeyValuePair<OKXAccount, string>(OKXAccount.Trading, "18"),
    };
}