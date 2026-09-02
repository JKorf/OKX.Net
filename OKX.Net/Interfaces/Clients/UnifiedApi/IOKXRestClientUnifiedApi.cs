using CryptoExchange.Net.Interfaces.Clients;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API endpoints
/// </summary>
public interface IOKXRestClientUnifiedApi : IRestApiClient<OKXCredentials>
{
    /// <summary>
    /// Endpoints related to account settings, info or actions
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApiAccount"/>
    IOKXRestClientUnifiedApiAccount Account { get; }

    /// <summary>
    /// Endpoints related to retrieving market and system data
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApiExchangeData"/>
    IOKXRestClientUnifiedApiExchangeData ExchangeData { get; }

    /// <summary>
    /// Endpoints related to subaccount management
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApiSubAccounts"/>
    IOKXRestClientUnifiedApiSubAccounts SubAccounts { get; }

    /// <summary>
    /// Endpoints related to orders and trades
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApiTrading"/>
    IOKXRestClientUnifiedApiTrading Trading { get; }

    /// <summary>
    /// Endpoints related to copy trading
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApiCopyTrading"/>
    IOKXRestClientUnifiedApiCopyTrading CopyTrading { get; }

    /// <summary>
    /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
    /// </summary>
    IOKXRestClientUnifiedApiShared SharedClient { get; }
    /// <summary>
    /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    IOKXRestClientUnifiedSharedApi SharedApi { get; }
}