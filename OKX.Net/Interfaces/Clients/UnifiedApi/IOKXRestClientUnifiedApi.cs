namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API endpoints
/// </summary>
public interface IOKXRestClientUnifiedApi : IRestApiClient
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
    /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
    /// </summary>
    IOKXRestClientUnifiedApiShared SharedClient { get; }

    /// <summary>
    /// Endpoints related to copy trading
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApiCopyTrading"/>
    IOKXRestClientUnifiedApiCopyTrading CopyTrading { get; }
}