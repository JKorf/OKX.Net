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
        /// Old funding rate formula
        /// </summary>
        [Map("noRate")]
        NoRate,
        /// <summary>
        /// New funding rate formula
        /// </summary>
        [Map("withRate")]
        WithRate
    }
}
