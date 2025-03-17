using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Greeks type
/// </summary>
[SerializationModel]
public record OKXAccountGreeksType
{
    /// <summary>
    /// Greeks type
    /// </summary>
    [JsonPropertyName("greeksType")]
    public GreeksType GreeksType { get; set; }
}
