using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Funding;
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
        var subscription = new OKXSubscription<OKXAccountBalance[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "account",
                    Asset = asset,
                    ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
                }
            }, x =>
            {
                if (x.Data.Length == 0)
                    return;

                onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().UpdateTime));
            }, true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(
        Action<DataEvent<OKXPositionAndBalanceUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXPositionAndBalanceUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "balance_and_position"
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToDepositUpdatesAsync(
        Action<DataEvent<OKXDepositUpdate>> onData,
        CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXDepositUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "deposit-info"
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().PushTime)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToWithdrawalUpdatesAsync(Action<DataEvent<OKXWithdrawalUpdate>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXWithdrawalUpdate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "withdrawal-info"
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().PushTime)), true);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }
}
