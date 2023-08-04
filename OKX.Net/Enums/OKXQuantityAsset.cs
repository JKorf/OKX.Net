using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quantity asset
/// </summary>
public enum OKXQuantityAsset
{
    /// <summary>
    /// Quantity in base asset
    /// </summary>
    [Map("base_ccy")]
    BaseAsset,
    /// <summary>
    /// Quantity in quote asset
    /// </summary>
    [Map("quote_ccy")]
    QuoteAsset
}
