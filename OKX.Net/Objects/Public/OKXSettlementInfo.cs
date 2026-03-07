namespace OKX.Net.Objects.Public
{
    /// <summary>
    /// Settlement info
    /// </summary>
    public record OKXSettlementInfo
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>details</c>"] Price info
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
        /// ["<c>instId</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settlePx</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("settlePx")]
        public decimal SettlementPrice { get; set; }
    }
}
