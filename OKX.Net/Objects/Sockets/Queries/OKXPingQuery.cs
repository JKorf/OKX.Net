using CryptoExchange.Net.Sockets;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXPingQuery : Query<string>
{
    public OKXPingQuery() : base("ping", false, 0)
    {
        RequestTimeout = TimeSpan.FromSeconds(5);
        MessageRouter = MessageRouter.CreateWithoutHandler<string>("pong");
    }
}
