using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class DepositStateConverter : BaseConverter<OKXDepositState>
{
    public DepositStateConverter() : this(true) { }
    public DepositStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXDepositState, string>> Mapping => new List<KeyValuePair<OKXDepositState, string>>
    {
        new KeyValuePair<OKXDepositState, string>(OKXDepositState.WaitingForConfirmation, "1"),
        new KeyValuePair<OKXDepositState, string>(OKXDepositState.Credited, "2"),
        new KeyValuePair<OKXDepositState, string>(OKXDepositState.Successful, "3"),
        new KeyValuePair<OKXDepositState, string>(OKXDepositState.Pending, "4"),
    };
}