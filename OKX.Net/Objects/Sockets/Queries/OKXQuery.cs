using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>();

    public OKXQuery(OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        ListenerIdentifiers.Add("error");
        foreach (var arg in request.Args)
        {
            ListenerIdentifiers.Add(request.Op + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
            ListenerIdentifiers.Add("error" + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
        }
    }

    public OKXQuery(OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        ListenerIdentifiers = new HashSet<string> { "login", "error" };
    }

    public override CallResult<OKXSocketResponse> HandleMessage(SocketConnection connection, DataEvent<OKXSocketResponse> message)
    {
        if (string.Equals(message.Data.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse>(new ServerError(message.Data.Code ?? 0, message.Data.Message!), message.OriginalData);

        return base.HandleMessage(connection, message);
    }
}
