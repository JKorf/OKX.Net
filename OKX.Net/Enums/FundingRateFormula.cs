using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Funding rate calculation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FundingRateFormula>))]
    public enum FundingRateFormula
    {
        /// <summary>
        /// ["<c>noRate</c>"] Old funding rate formula
        /// </summary>
        [Map("noRate")]
        NoRate,
        /// <summary>
        /// ["<c>withRate</c>"] New funding rate formula
        /// </summary>
        [Map("withRate")]
        WithRate
    }
}
