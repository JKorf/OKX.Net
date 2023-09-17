using CryptoExchange.Net.Sockets;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Funding;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApiAccount : IOKXSocketClientUnifiedApiAccount
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
        Action<OKXAccountBalance> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXAccountBalance>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "account",
            Asset = asset,
            ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(
        Action<OKXPositionAndBalanceUpdate> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXPositionAndBalanceUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "balance_and_position"
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToDepositUpdatesAsync(
        Action<OKXDepositHistory> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXDepositHistory>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "deposit-info"
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToWithdrawalUpdatesAsync(Action<OKXWithdrawalHistory> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXWithdrawalHistory>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "withdrawal-info"
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }
}
