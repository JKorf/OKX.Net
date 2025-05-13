using CryptoExchange.Net.Objects.Options;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;

namespace OKX.Net.Interfaces.Clients;

/// <summary>
/// Client for accessing the OKX Rest API. 
/// </summary>
public interface IOKXRestClient : IRestClient
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    /// <see cref="IOKXRestClientUnifiedApi"/>
    IOKXRestClientUnifiedApi UnifiedApi { get; }

    /// <summary>
    /// Update specific options
    /// </summary>
    /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
    void SetOptions(UpdateOptions options);

    /// <summary>
    /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
    /// </summary>
    /// <param name="credentials">The credentials to set</param>
    void SetApiCredentials(ApiCredentials credentials);
}