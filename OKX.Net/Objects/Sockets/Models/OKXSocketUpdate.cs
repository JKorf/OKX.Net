namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketUpdate<T>
{
    [JsonPropertyName("arg")]
    public OKXSocketArgs Arg { get; set; } = default!;
    [JsonPropertyName("action")]
    public string? Action { get; set; } = string.Empty;
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;
}
