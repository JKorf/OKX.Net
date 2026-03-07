using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Greeks type
/// </summary>
[SerializationModel]
public record OKXAccountGreeksType
{
    /// <summary>
    /// ["<c>greeksType</c>"] Greeks type
    /// </summary>
    [JsonPropertyName("greeksType")]
    public GreeksType GreeksType { get; set; }
}
