using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXConnCountSubscription : SystemSubscription
{
    public OKXConnCountSubscription(ILogger logger) : base(logger, false)
    {
        MessageMatcher = MessageMatcher.Create<OKXConnectionCount>("channel-conn-count");
    }
}
