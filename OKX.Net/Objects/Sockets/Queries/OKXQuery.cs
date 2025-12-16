using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    private readonly SocketApiClient _client;

    public OKXQuery(SocketApiClient client, OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        _client = client;

        var ids = new List<string> { "error" };
        var routes = new List<MessageRoute>();
        foreach (var arg in request.Args)
        {
            ids.Add(request.Op + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
            ids.Add("error" + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());

            routes.Add(MessageRoute<OKXSocketResponse>.CreateWithTopicFilter(request.Op + arg.Channel, arg.InstrumentType + arg.InstrumentFamily + arg.Symbol, HandleMessage));
            routes.Add(MessageRoute<OKXSocketResponse>.CreateWithoutTopicFilter("error" + arg.Channel + arg.InstrumentType + arg.InstrumentFamily + arg.Symbol, HandleMessage));
        }

        MessageMatcher = MessageMatcher.Create<OKXSocketResponse>(ids, HandleMessage);
        MessageRouter = MessageRouter.Create(routes.ToArray());

        RequiredResponses = request.Args.Count;
    }

    public OKXQuery(SocketApiClient client, OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        _client = client;
        MessageMatcher = MessageMatcher.Create<OKXSocketResponse>(["login", "error"], HandleMessage);
        MessageRouter = MessageRouter.CreateWithoutTopicFilter<OKXSocketResponse>(["login", "error"], HandleMessage);
    }

    public CallResult<OKXSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, OKXSocketResponse message)
    {
        if (string.Equals(message.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse>(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Message!)), originalData);

        return new CallResult<OKXSocketResponse>(message, originalData, null);
    }
}
