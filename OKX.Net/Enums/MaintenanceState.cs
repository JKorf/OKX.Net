using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum MaintenanceState
{
    [Map("scheduled")]
    Scheduled,
    [Map("ongoing")]
    Ongoing,
    [Map("completed")]
    Completed,
    [Map("canceled")]
    Canceled,
}