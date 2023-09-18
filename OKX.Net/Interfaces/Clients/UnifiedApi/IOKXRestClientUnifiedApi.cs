using CryptoExchange.Net.Interfaces.CommonClients;

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
    /// Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
    /// </summary>
    /// <returns></returns>
    public ISpotClient CommonSpotClient { get; }
}