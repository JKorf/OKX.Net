namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXWithdrawalState
{
    PendingCancel,
    Canceled,
    Failed,
    Pending,
    Sending,
    Sent,
    AwaitingEmailVerification,
    AwaitingManualVerification,
    AwaitingIdentityVerification,
}