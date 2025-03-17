using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.System;

/// <summary>
/// Status info
/// </summary>
[SerializationModel]
public record OKXStatus
{
    /// <summary>
    /// The title of system maintenance instructions
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// System maintenance status
    /// </summary>
    [JsonPropertyName("state")]
    public MaintenanceState Status { get; set; }

    /// <summary>
    /// Begin time of system maintenance
    /// </summary>
    [JsonPropertyName("begin"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Begin { get; set; }

    /// <summary>
    /// The time of pre_open. Canceling orders, placing Post Only orders, and transferring funds to trading accounts are back after PreOpenBegin
    /// </summary>
    [JsonPropertyName("preOpenBegin"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? PreOpenBegin { get; set; }

    /// <summary>
    /// End time of system maintenance
    /// </summary>
    [JsonPropertyName("end"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? End { get; set; }

    /// <summary>
    /// Hyperlink for system maintenance details, if there is no return value, the default value will be empty. e.g. “”
    /// </summary>
    [JsonPropertyName("href")]
    public string Link { get; set; } = string.Empty;

    /// <summary>
    /// Service type, 0：WebSocket ; 1：Spot/Margin ; 2：Futures ; 3：Perpetual ; 4：Options ; 5：Trading service
    /// </summary>
    [JsonPropertyName("serviceType")]
    public MaintenanceService Product { get; set; }

    /// <summary>
    /// Service type, 0：WebSocket ; 1：Spot/Margin ; 2：Futures ; 3：Perpetual ; 4：Options ; 5：Trading service
    /// </summary>
    [JsonPropertyName("system")]
    public MaintenanceSystem System { get; set; }

    /// <summary>
    /// Rescheduled description，e.g. Rescheduled from 2021-01-26T16:30:00.000Z to 2021-01-28T16:30:00.000Z
    /// </summary>
    [JsonPropertyName("scheDesc")]
    public string RescheduledDescription { get; set; } = string.Empty;

    /// <summary>
    /// Maintenance type
    /// </summary>
    [JsonPropertyName("maintType")]
    public MaintenanceType MaintenanceType { get; set; }

    /// <summary>
    /// Environment. 1: Production Trading, 2: Demo Trading
    /// </summary>
    [JsonPropertyName("env")]
    public string Environment { get; set; } = string.Empty;

    /// <summary>
    /// Push timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
