﻿using CryptoExchange.Net.Interfaces.CommonClients;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API endpoints
/// </summary>
public interface IOKXRestClientUnifiedApi : IRestApiClient
{
    /// <summary>
    /// Endpoints related to account settings, info or actions
    /// </summary>
    IOKXRestClientUnifiedApiAccount Account { get; }

    /// <summary>
    /// Endpoints related to retrieving market and system data
    /// </summary>
    IOKXRestClientUnifiedApiExchangeData ExchangeData { get; }

    /// <summary>
    /// Endpoints related to subaccount mangement
    /// </summary>
    IOKXRestClientUnifiedApiSubAccounts SubAccounts { get; }

    /// <summary>
    /// Endpoints related to orders and trades
    /// </summary>
    IOKXRestClientUnifiedApiTrading Trading { get; }

    /// <summary>
    /// DEPRECATED; use <see cref="CryptoExchange.Net.SharedApis.ISharedClient" /> instead for common/shared functionality. See <see href="https://jkorf.github.io/CryptoExchange.Net/docs/index.html#shared" /> for more info.
    /// </summary>
    public ISpotClient CommonSpotClient { get; }

    /// <summary>
    /// Get the shared rest requests client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
    /// </summary>
    IOKXRestClientUnifiedApiShared SharedClient { get; }
}