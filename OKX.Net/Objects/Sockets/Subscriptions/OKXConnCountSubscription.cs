using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXConnCountSubscription : SystemSubscription
{
    public OKXConnCountSubscription(ILogger logger) : base(logger, false)
    {
        MessageRouter = MessageRouter.CreateWithoutHandler<OKXConnectionCount>("channel-conn-count");
    }
}
