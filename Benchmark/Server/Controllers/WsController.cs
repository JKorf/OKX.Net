using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OKX.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("")]
    public class WsController : ControllerBase
    {
        [HttpGet("ws/v5/public")]
        public async Task Get()
        {
            var webSocket = await Request.HttpContext.WebSockets.AcceptWebSocketAsync();

            var cts = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    var buffer = new byte[8096];
                    var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        cts.Cancel();

                        if (webSocket.State == WebSocketState.CloseReceived)
                            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", default).ConfigureAwait(false);
                    }
                    else
                    {
                        var msg = JsonSerializer.Deserialize<SubscribeMessage>(Encoding.UTF8.GetString(buffer, 0, result.Count))!;

                        foreach (var arg in msg.Args)
                        {
                            var response = "{\"event\":\"subscribe\",\"arg\":{\"channel\":\"" + arg.Channel + "\",\"instId\":\"" + arg.Symbol + "\"},\"connId\":\"benchmark\"}";
                            await SendAsync(webSocket, response);
                        }

                        if (msg.Args.Any(x => string.Equals(x.Channel, "trades", StringComparison.OrdinalIgnoreCase)
                            && string.Equals(x.Symbol, "ETH-USDT", StringComparison.OrdinalIgnoreCase)))
                        {
                            _ = PushTradeUpdates(webSocket, cts.Token);
                        }
                    }
                }
            });

            try
            {
                await Task.Delay(-1, cts.Token);
            }
            catch (Exception) { }
        }

        private async Task SendAsync(WebSocket webSocket, string message)
        {
            await webSocket.SendAsync(Encoding.UTF8.GetBytes(message),
                WebSocketMessageType.Text,
                endOfMessage: true,
                CancellationToken.None);
        }

        private async Task SendAsync(WebSocket webSocket, byte[] data)
        {
            await webSocket.SendAsync(data,
                WebSocketMessageType.Text,
                endOfMessage: true,
                CancellationToken.None);
        }

        private async Task PushTradeUpdates(WebSocket webSocket, CancellationToken ct)
        {
            var time = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            for (var i = 0; i < 1_000_000; i++)
            {
                if (ct.IsCancellationRequested)
                    break;

                var trade = "{\"arg\":{\"channel\":\"trades\",\"instId\":\"ETH-USDT\"},\"data\":[{\"instId\":\"ETH-USDT\",\"tradeId\":\"" + (5000000000L + i) + "\",\"px\":\"3187.96000000\",\"sz\":\"0.00170000\",\"side\":\"buy\",\"ts\":\"" + time + "\",\"count\":\"1\"}]}";
                await SendAsync(webSocket, Encoding.UTF8.GetBytes(trade));
            }

            try
            {
                await Task.Delay(5000, ct);
            }
            catch (Exception) { }
        }
    }

    public record SubscribeMessage
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;

        [JsonPropertyName("args")]
        public SocketArgs[] Args { get; set; } = [];
    }

    public record SocketArgs
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;

        [JsonPropertyName("instId")]
        public string Symbol { get; set; } = string.Empty;
    }
}
