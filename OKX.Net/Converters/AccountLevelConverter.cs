using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AccountLevelConverter : BaseConverter<OKXAccountLevel>
{
    public AccountLevelConverter() : this(true) { }
    public AccountLevelConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAccountLevel, string>> Mapping => new()
    {
        new KeyValuePair<OKXAccountLevel, string>(OKXAccountLevel.Simple, "1"),
        new KeyValuePair<OKXAccountLevel, string>(OKXAccountLevel.SingleCurrencyMargin, "2"),
        new KeyValuePair<OKXAccountLevel, string>(OKXAccountLevel.MultiCurrencyMargin, "3"),
        new KeyValuePair<OKXAccountLevel, string>(OKXAccountLevel.PortfolioMargin, "4"),
    };
}