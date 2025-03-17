using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Rule type
/// </summary>
[JsonConverter(typeof(EnumConverter<SymbolRuleType>))]
public enum SymbolRuleType
{
    /// <summary>
    /// Normal trading
    /// </summary>
    [Map("normal")]
    Normal,
    /// <summary>
    /// Pre-market trading
    /// </summary>
    [Map("pre_market")]
    PreMarket,
}
