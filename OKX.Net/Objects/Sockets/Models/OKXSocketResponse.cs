namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketResponse
{
    [JsonPropertyName("op")]
    public string Op { get; set; } = string.Empty;
    [JsonPropertyName("arg")]
    public OKXSocketArgs Arg { get; set; } = null!;
    [JsonPropertyName("connId")]
    public string ConnectionId { get; set; } = string.Empty;

    [JsonPropertyName("event")]
    public string? Event { get; set; }
    [JsonPropertyName("msg")]
    public string? Message { get; set; }
    [JsonPropertyName("code")]
    public int? Code { get; set; }
}

internal class OKXSocketResponse<T> : OKXSocketResponse
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;
}
