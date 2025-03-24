using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Public
{
    /// <summary>
    /// Settlement info
    /// </summary>
    public record OKXSettlementInfo
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Price info
        /// </summary>
        [JsonPropertyName("details")]
        public OKXSettlementInfoDetails[] Details { get; set; } = [];
    }

    /// <summary>
    /// Settlement info details
    /// </summary>
    public record OKXSettlementInfoDetails
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonPropertyName("settlePx")]
        public decimal SettlementPrice { get; set; }
    }
}
