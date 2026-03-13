using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using OKX.Net.Interfaces.Clients.UnifiedApi;

namespace OKX.Net.Interfaces.Clients;

/// <summary>
/// Client for accessing the OKX Rest API. 
/// </summary>
public interface IOKXRestClient : IRestClient<OKXCredentials>
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApi"/>
    IOKXRestClientUnifiedApi UnifiedApi { get; }
}