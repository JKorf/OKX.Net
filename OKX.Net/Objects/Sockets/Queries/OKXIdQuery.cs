using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXIdQuery<T> : Query<OKXSocketResponse<T[]>>
{
    private readonly SocketApiClient _client;

    public OKXIdQuery(SocketApiClient client, string op, object[] args, bool authenticated, int weight = 1) : base(new OKXSocketIdRequest() { Id = ExchangeHelpers.NextId().ToString(), Op = op, Args = args }, authenticated, weight)
    {
        _client = client;
        MessageMatcher = MessageMatcher.Create<OKXSocketResponse<T[]>>(((OKXSocketIdRequest)Request).Id, HandleMessage);
    }

    public CallResult<OKXSocketResponse<T[]>> HandleMessage(SocketConnection connection, DataEvent<OKXSocketResponse<T[]>> message)
    {
        if (string.Equals(message.Data.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse<T[]>>(new ServerError(message.Data.Code!.Value, _client.GetErrorInfo(message.Data.Code.Value, message.Data.Message!)), message.OriginalData);

        return new CallResult<OKXSocketResponse<T[]>>(message.Data, message.OriginalData, null);
    }
}
