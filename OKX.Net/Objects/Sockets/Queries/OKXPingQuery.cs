using CryptoExchange.Net.Sockets;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXPingQuery : Query<string>
{
    public OKXPingQuery() : base("ping", false, 0)
    {
        RequestTimeout = TimeSpan.FromSeconds(5);
        MessageMatcher = MessageMatcher.Create<string>("pong");
    }
}
