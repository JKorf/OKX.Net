using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    public override HashSet<string> ListenerIdentifiers { get; set;  }

    public OKXQuery(OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        ListenerIdentifiers = new HashSet<string>(request.Args.Select(a => request.Op + a.Channel.ToLowerInvariant() + a.InstrumentType?.ToString().ToLowerInvariant() + a.InstrumentFamily?.ToString().ToLowerInvariant() + a.Symbol?.ToLowerInvariant()));
        _ = ListenerIdentifiers.Add("error");
    }

    public OKXQuery(OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        ListenerIdentifiers = new HashSet<string> { "login", "error" };
    }

    public override CallResult<OKXSocketResponse> HandleMessage(SocketConnection connection, DataEvent<OKXSocketResponse> message)
    {
        if (message.Data.Event == "error")
            return new CallResult<OKXSocketResponse>(new ServerError(message.Data.Code ?? 0, message.Data.Message!), message.OriginalData);

        return base.HandleMessage(connection, message);
    }
}
