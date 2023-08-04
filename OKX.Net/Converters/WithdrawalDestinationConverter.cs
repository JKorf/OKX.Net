using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class WithdrawalDestinationConverter : BaseConverter<OKXWithdrawalDestination>
{
    public WithdrawalDestinationConverter() : this(true) { }
    public WithdrawalDestinationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXWithdrawalDestination, string>> Mapping => new List<KeyValuePair<OKXWithdrawalDestination, string>>
    {
        new KeyValuePair<OKXWithdrawalDestination, string>(OKXWithdrawalDestination.OKX, "3"),
        new KeyValuePair<OKXWithdrawalDestination, string>(OKXWithdrawalDestination.DigitalCurrencyAddress, "4"),

    };
}