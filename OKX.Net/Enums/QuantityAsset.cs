using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quantity asset
/// </summary>
[JsonConverter(typeof(EnumConverter<QuantityAsset>))]
public enum QuantityAsset
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
