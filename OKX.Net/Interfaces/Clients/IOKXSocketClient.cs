using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using OKX.Net.Interfaces.Clients.UnifiedApi;

namespace OKX.Net.Interfaces.Clients;

/// <summary>
/// Client for accessing the OKX websocket API. 
/// </summary>
public interface IOKXSocketClient : ISocketClient<OKXCredentials>
{
    /// <summary>
    /// Unified API
    /// </summary>
    /// <see cref="IOKXSocketClientUnifiedApi"/>
    IOKXSocketClientUnifiedApi UnifiedApi { get; }
}