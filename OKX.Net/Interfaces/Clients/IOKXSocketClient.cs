using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;

namespace OKX.Net.Interfaces.Clients;

/// <summary>
/// Client for accessing the OKX websocket API. 
/// </summary>
public interface IOKXSocketClient : ISocketClient
{
    /// <summary>
    /// Unified API
    /// </summary>
    IOKXSocketClientUnifiedApi UnifiedApi { get; }

    /// <summary>
    /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
    /// </summary>
    /// <param name="credentials">The credentials to set</param>
    void SetApiCredentials(OKXApiCredentials credentials);
}