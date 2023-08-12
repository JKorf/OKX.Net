using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Type of maintenance
/// </summary>
public enum OKXMaintenanceType
{
    /// <summary>
    /// Scheduled
    /// </summary>
    [Map("1")]
    ScheduledMaintenance,
    /// <summary>
    /// Unscheduled
    /// </summary>
    [Map("2")]
    UnscheduledMaintenance,
    /// <summary>
    /// Disruption
    /// </summary>
    [Map("3")]
    SystemDisruption
}
