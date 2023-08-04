namespace OKX.Net.Objects.Core;

/// <summary>
/// Rest API response
/// </summary>
/// <typeparam name="T"></typeparam>
public class OKXRestApiResponse<T>
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public int ErrorCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Response data
    /// </summary>
    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; set; }
}