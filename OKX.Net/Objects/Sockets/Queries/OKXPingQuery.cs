using CryptoExchange.Net.Sockets;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXPingQuery : Query<string>
{
    public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string> { "pong" };

    public OKXPingQuery() : base("ping", false, 0)
    {
    }
}
