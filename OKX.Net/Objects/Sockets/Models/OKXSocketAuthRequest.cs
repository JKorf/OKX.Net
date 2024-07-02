namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketAuthRequest
{
    [JsonPropertyName("op")]
    public string Op { get; set; } = string.Empty;
    [JsonPropertyName("args")]
    public List<OKXSocketAuthArgs> Args { get; set; } = new List<OKXSocketAuthArgs>();
}

internal class OKXSocketAuthArgs
{
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; } = string.Empty;
    [JsonPropertyName("passphrase")]
    public string Passphrase { get; set; } = string.Empty;
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; } = string.Empty;
    [JsonPropertyName("sign")]
    public string Sign { get; set; } = string.Empty;
}
