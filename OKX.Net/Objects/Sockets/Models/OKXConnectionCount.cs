namespace OKX.Net.Objects.Sockets.Models;
internal record OKXConnectionCount
{
    [JsonPropertyName("event")]
    public string Event { get; set; } = string.Empty;
    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;
    [JsonPropertyName("connCount")]
    public int ConnectionCount { get; set; }
    [JsonPropertyName("connId")]
    public string ConnectionId { get; set; } = string.Empty;
}
