using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXBookSubscription : Subscription
{
    private readonly SocketApiClient _client;
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<OKXOrderBook>> _handler;

    public OKXBookSubscription(ILogger logger, SocketApiClient client, List<OKXSocketArgs> args, Action<DataEvent<OKXOrderBook>> handler, bool authenticated) : base(logger, authenticated)
    {
        _client = client;
        _args = args;
        _handler = handler;

        IndividualSubscriptionCount = args.Count;

        MessageRouter = MessageRouter.CreateWithTopicFilters<OKXSocketUpdate<OKXOrderBook[]>>(args.First().Channel, args.Select(x => x.InstrumentType + x.InstrumentFamily + x.Symbol), DoHandleMessage);
    }

    protected override Query? GetSubQuery(SocketConnection connection)
    {
        return new OKXQuery(_client, new OKXSocketRequest
        {
            Op = "subscribe",
            Args = _args
        }, false);
    }

    protected override Query? GetUnsubQuery(SocketConnection connection)
    {
        return new OKXQuery(_client, new OKXSocketRequest
        {
            Op = "unsubscribe",
            Args = _args
        }, false);
    }

    public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, OKXSocketUpdate<OKXOrderBook[]> message)
    {
        foreach (var item in message.Data)
            item.Action = message.Action!;

        var book = message.Data.Single();
        _client.UpdateTimeOffset(book.Time);

        _handler.Invoke(
                new DataEvent<OKXOrderBook>(OKXExchange.ExchangeName, book, receiveTime, originalData)
                    .WithStreamId(message.Arg.Channel)
                    .WithSymbol(message.Arg.Symbol)
                    .WithDataTimestamp(book.Time, _client.GetTimeOffset())
                    .WithSequenceNumber(book.SequenceId)
                    .WithUpdateType(string.Equals(message.Action, "snapshot", StringComparison.Ordinal) || message.Action == null ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
            );
        return CallResult.SuccessResult;
    }
}
