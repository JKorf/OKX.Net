﻿namespace OKX.Net.Objects.Funding;

/// <summary>
/// Lightning withdrawal
/// </summary>
public record OKXLightningWithdrawal
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;
}