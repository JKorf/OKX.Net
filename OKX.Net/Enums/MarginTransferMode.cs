using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<MarginTransferMode>))]
public enum MarginTransferMode
{
    [Map("automatic", "auto_transfers_ccy")]
    AutoTransfer,
    [Map("autonomy")]
    ManualTransfer
}
