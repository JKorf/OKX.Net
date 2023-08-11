using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;

namespace OKX.Net.Interfaces.Clients;

/// <summary>
/// Client for accessing the OKX Rest API. 
/// </summary>
public interface IOKXRestClient: IRestClient
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    IOKXRestClientUnifiedApi UnifiedApi { get; }

    /// <summary>
    /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
    /// </summary>
    /// <param name="credentials">The credentials to set</param>
    void SetApiCredentials(OKXApiCredentials credentials);
}