namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketResponse
{
    [JsonProperty("op")]
    public string Op { get; set; } = string.Empty;
    [JsonProperty("arg")]
    public OKXSocketArgs Arg { get; set; } = null!;
    [JsonProperty("connId")]
    public string ConnectionId { get; set; } = string.Empty;

    [JsonProperty("event")]
    public string? Event { get; set; }
    [JsonProperty("msg")]
    public string? Message { get; set; }
    [JsonProperty("code")]
    public int? Code { get; set; }
}

internal class OKXSocketResponse<T> : OKXSocketResponse
{
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}
