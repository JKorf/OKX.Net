using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<WithdrawalState>))]
public enum WithdrawalState
{
    [Map("-3")]
    Canceling,
    [Map("-2")]
    Canceled,
    [Map("-1")]
    Failed,
    [Map("0")]
    Pending,
    [Map("1")]
    Withdrawing,
    [Map("2")]
    Success,
    [Map("7")]
    Approved,
    [Map("10")]
    AwaitingTransfer,
    [Map("4", "5", "6", "8", "9", "12")]
    AwaitingManualReview,
    [Map("15", "16")]
    PendingTransactionValidation,
    [Map("17")]
    PendingTravelRule,
    [Map("19")]
    InsufficientHotWalletBalance
}
