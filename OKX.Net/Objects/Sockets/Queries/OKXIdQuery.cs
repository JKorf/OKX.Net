using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXIdQuery<T> : Query<OKXSocketResponse<T[]>>
{
    private readonly SocketApiClient _client;

    public OKXIdQuery(SocketApiClient client, string op, object[] args, bool authenticated, int weight = 1) : base(new OKXSocketIdRequest() { Id = ExchangeHelpers.NextId().ToString(), Op = op, Args = args }, authenticated, weight)
    {
        _client = client;
        MessageMatcher = MessageMatcher.Create<OKXSocketResponse<T[]>>(((OKXSocketIdRequest)Request).Id, HandleMessage);
        MessageRouter = MessageRouter.CreateWithoutTopicFilter<OKXSocketResponse<T[]>>(((OKXSocketIdRequest)Request).Id, HandleMessage);
    }

    public CallResult<OKXSocketResponse<T[]>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, OKXSocketResponse<T[]> message)
    {
        if (string.Equals(message.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse<T[]>>(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Message!)), originalData);

        return new CallResult<OKXSocketResponse<T[]>>(message, originalData, null);
    }
}
