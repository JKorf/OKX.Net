using CryptoExchange.Net.Objects.Options;
using OKX.Net;

namespace OKX.Net.Objects.Options;

/// <summary>
/// Socket client options
/// </summary>
public class OKXSocketOptions : SocketExchangeOptions<OKXEnvironment>
{
    /// <summary>
    /// Default options for new OKXRestClients
    /// </summary>
    public static OKXSocketOptions Default { get; set; } = new OKXSocketOptions()
    {
        SocketSubscriptionsCombineTarget = 10,
        Environment = OKXEnvironment.Live
    };

    /// <summary>
    /// Broker ID for earning rebates
    /// </summary>
    public string? BrokerId { get; set; }

    /// <summary>
    /// Options for the Unified API
    /// </summary>
    public SocketApiOptions UnifiedOptions { get; private set; } = new SocketApiOptions();

    internal OKXSocketOptions Copy()
    {
        var options = Copy<OKXSocketOptions>();
        options.BrokerId = BrokerId;
        options.UnifiedOptions = UnifiedOptions.Copy<SocketApiOptions>();
        return options;
    }
}
