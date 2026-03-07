namespace OKX.Net.Objects.Public
{
    /// <summary>
    /// Estimated settlement price
    /// </summary>
    public record OKXSettlementPrice
    {
        /// <summary>
        /// ["<c>instId</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>estSettlePx</c>"] Estimated settlement price
        /// </summary>
        [JsonPropertyName("estSettlePx")]
        public decimal EstimatedSettlementPrice { get; set; }
        /// <summary>
        /// ["<c>nextSettleTime</c>"] Next settlement time
        /// </summary>
        [JsonPropertyName("nextSettleTime")]
        public DateTime NextSettlementTime { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Data timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
