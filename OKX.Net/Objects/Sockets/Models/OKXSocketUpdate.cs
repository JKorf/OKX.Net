namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketUpdate<T>
{
    [JsonProperty("arg")]
    public OKXSocketArgs Arg { get; set; } = default!;
    [JsonProperty("action")]
    public string? Action { get; set; } = string.Empty;
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}
