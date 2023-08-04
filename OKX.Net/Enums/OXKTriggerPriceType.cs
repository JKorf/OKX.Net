using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Trigger price tpye
    /// </summary>
    public enum OXKTriggerPriceType
    {
        /// <summary>
        /// Last price
        /// </summary>
        [Map("last")]
        Last,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("index")]
        Index,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("mark")]
        Mark
    }
}
