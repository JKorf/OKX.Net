using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class WithdrawalStateConverter : BaseConverter<OKXWithdrawalState>
{
    public WithdrawalStateConverter() : this(true) { }
    public WithdrawalStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXWithdrawalState, string>> Mapping => new List<KeyValuePair<OKXWithdrawalState, string>>
    {
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.PendingCancel, "-3"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.Canceled, "-2"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.Failed, "-1"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.Pending, "0"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.Sending, "1"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.Sent, "2"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.AwaitingEmailVerification, "3"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.AwaitingManualVerification, "4"),
        new KeyValuePair<OKXWithdrawalState, string>(OKXWithdrawalState.AwaitingIdentityVerification, "5"),
    };
}