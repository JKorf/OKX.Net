using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Public
{
    /// <summary>
    /// Estimated settlement price
    /// </summary>
    public record OKXSettlementPrice
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Estimated settlement price
        /// </summary>
        [JsonPropertyName("estSettlePx")]
        public decimal EstimatedSettlementPrice { get; set; }
        /// <summary>
        /// Next settlement time
        /// </summary>
        [JsonPropertyName("nextSettleTime")]
        public DateTime NextSettlementTime { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
