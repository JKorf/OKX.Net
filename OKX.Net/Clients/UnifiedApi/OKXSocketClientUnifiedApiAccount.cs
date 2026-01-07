using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Funding;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Subscriptions;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
internal class OKXSocketClientUnifiedApiAccount : IOKXSocketClientUnifiedApiAccount
{
    private readonly OKXSocketClientUnifiedApi _client;

    private readonly ILogger _logger;

    #region ctor

    internal OKXSocketClientUnifiedApiAccount(ILogger logger, OKXSocketClientUnifiedApi client)
    {
        _client = client;
        _logger = logger;
    }
    #endregion

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(
        string? asset,
        bool regularUpdates,
        Action<DataEvent<OKXAccountBalance>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXAccountBalance[]>>((receiveTime, originalData, data) =>
        {
            if (!data.Data.Any())
                return;

            var item = data.Data.First();
            _client.UpdateTimeOffset(item.UpdateTime);

            onData(
                new DataEvent<OKXAccountBalance>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(item.UpdateTime, _client.GetTimeOffset())
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXAccountBalance[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "account",
                    Asset = asset,
                    ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(
        Action<DataEvent<OKXPositionAndBalanceUpdate>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXPositionAndBalanceUpdate[]>>((receiveTime, originalData, data) =>
        {
            var item = data.Data.First();
            _client.UpdateTimeOffset(item.Time);

            onData(
                new DataEvent<OKXPositionAndBalanceUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(item.Time, _client.GetTimeOffset())
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXPositionAndBalanceUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "balance_and_position"
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToDepositUpdatesAsync(
        Action<DataEvent<OKXDepositUpdate>> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXDepositUpdate[]>>((receiveTime, originalData, data) =>
        {
            var item = data.Data.First();
            _client.UpdateTimeOffset(item.PushTime);

            onData(
                new DataEvent<OKXDepositUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(item.PushTime, _client.GetTimeOffset())
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXDepositUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "deposit-info"
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToWithdrawalUpdatesAsync(Action<DataEvent<OKXWithdrawalUpdate>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXWithdrawalUpdate[]>>((receiveTime, originalData, data) =>
        {
            var item = data.Data.First();
            _client.UpdateTimeOffset(item.PushTime);

            onData(
                new DataEvent<OKXWithdrawalUpdate>(OKXExchange.ExchangeName, item, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(item.PushTime, _client.GetTimeOffset())
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXWithdrawalUpdate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "withdrawal-info"
                }
            }, internalHandler, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }
}
