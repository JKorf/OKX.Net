namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketRequest
{
    [JsonPropertyName("op")]
    public string Op { get; set; } = string.Empty;
    [JsonPropertyName("args")]
    public List<OKXSocketArgs> Args { get; set; } = new List<OKXSocketArgs>();
}

internal class OKXSocketIdRequest
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("op")]
    public string Op { get; set; } = string.Empty;
    [JsonPropertyName("args")]
    public object[] Args { get; set; } = Array.Empty<object>();
}
