using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Funding;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API
/// </summary>
public interface IOKXSocketClientUnifiedApiAccount
{
    /// <summary>
    /// Subscribe to account information updates. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-websocket-account-channel" /></para>
    /// </summary>
    /// <param name="asset">Only receive updates for this asset, for example `BTC`</param>
    /// <param name="regularUpdates">If true will send updates regularly even if nothing has changed. If false only send update on change</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string? asset, bool regularUpdates, Action<DataEvent<OKXAccountBalance>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to account balance and position information updates. Data will be pushed when triggered by events such as filled order, funding transfer.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-websocket-balance-and-position-channel" /></para>
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(Action<DataEvent<OKXPositionAndBalanceUpdate>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to deposit updates
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-websocket-deposit-info-channel" /></para>
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToDepositUpdatesAsync(Action<DataEvent<OKXDepositUpdate>> onData, CancellationToken ct = default);

    /// <summary>
    /// Subscribe to withdrawal updates
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-websocket-withdrawal-info-channel" /></para>
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<CallResult<UpdateSubscription>> SubscribeToWithdrawalUpdatesAsync(Action<DataEvent<OKXWithdrawalUpdate>> onData, CancellationToken ct = default);
}