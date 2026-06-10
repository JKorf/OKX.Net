using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Sockets.Models;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    private readonly SocketApiClient _client;

    public OKXQuery(SocketApiClient client, OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        _client = client;

        var routes = new List<MessageRoute>();
        foreach (var arg in request.Args)
        {
            var topic = arg.InstrumentType + arg.InstrumentFamily + arg.Symbol;
            routes.Add(MessageRoute.CreateForQuery<OKXSocketResponse>(request.Op + arg.Channel, string.IsNullOrEmpty(topic) ? null : topic, HandleMessage));
            routes.Add(MessageRoute.CreateForQuery<OKXSocketResponse>("error" + arg.Channel + topic, HandleMessage));
        }

        MessageRouter = MessageRouter.Create(routes.ToArray());

        RequiredResponses = request.Args.Count;
    }

    public OKXQuery(SocketApiClient client, OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        _client = client;
        MessageRouter = MessageRouter.CreateForQuery<OKXSocketResponse>(["login", "error"], HandleMessage);
    }

    public CallResult<OKXSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, OKXSocketResponse message)
    {
        if (string.Equals(message.Event, "error", StringComparison.Ordinal))
            return CallResult<OKXSocketResponse>.Fail(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Message!)), originalData);

        return CallResult<OKXSocketResponse>.Ok(message, originalData);
    }
}
