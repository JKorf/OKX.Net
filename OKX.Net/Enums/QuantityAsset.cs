using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quantity asset
/// </summary>
[JsonConverter(typeof(EnumConverter<QuantityAsset>))]
public enum QuantityAsset
{
    /// <summary>
    /// ["<c>base_ccy</c>"] Quantity in base asset
    /// </summary>
    [Map("base_ccy")]
    BaseAsset,
    /// <summary>
    /// ["<c>quote_ccy</c>"] Quantity in quote asset
    /// </summary>
    [Map("quote_ccy")]
    QuoteAsset
}
