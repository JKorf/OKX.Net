using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<MaintenanceService>))]
public enum MaintenanceService
{
    [Map("0")]
    WebSocket,
    [Map("1")]
    SpotMargin,
    [Map("2")]
    Futures,
    [Map("3")]
    Perpetual,
    [Map("4")]
    Options,
    [Map("5")]
    Trading,
    [Map("10")]
    SpreadTrading,
    [Map("11")]
    CopyTrading,
    [Map("6")]
    BlockTrading,
    [Map("7")]
    TradingBot,
    [Map("8")]
    TradingServiceAccounts,
    [Map("9")]
    TradingServiceProducts,
    [Map("99")]
    Other
}
