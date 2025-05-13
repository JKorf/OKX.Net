using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Core;

/// <summary>
/// Rest API response
/// </summary>
[SerializationModel]
public record OKXRestApiResponse
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int ErrorCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("msg"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Rest API response
/// </summary>
/// <typeparam name="T"></typeparam>
[SerializationModel]
public record OKXRestApiResponse<T> : OKXRestApiResponse
{
    /// <summary>
    /// Response data
    /// </summary>
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; set; }
}
