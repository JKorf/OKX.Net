using CryptoExchange.Net.Objects.Options;

namespace OKX.Net.Objects.Options;

/// <summary>
/// Socket client options
/// </summary>
public class OKXSocketOptions : SocketExchangeOptions<OKXEnvironment, ApiCredentials>
{
    /// <summary>
    /// Default options for new OKXRestClients
    /// </summary>
    internal static OKXSocketOptions Default { get; set; } = new OKXSocketOptions()
    {
        SocketSubscriptionsCombineTarget = 10,
        Environment = OKXEnvironment.Live
    };

    /// <summary>
    /// ctor
    /// </summary>
    public OKXSocketOptions()
    {
        Default?.Set(this);
    }

    /// <summary>
    /// Broker id
    /// </summary>
    public string? BrokerId { get; set; }

    /// <summary>
    /// Options for the Unified API
    /// </summary>
    public SocketApiOptions UnifiedOptions { get; private set; } = new SocketApiOptions();

    internal OKXSocketOptions Set(OKXSocketOptions targetOptions)
    {
        targetOptions = base.Set<OKXSocketOptions>(targetOptions);
        targetOptions.BrokerId = BrokerId;
        targetOptions.UnifiedOptions = UnifiedOptions.Set(targetOptions.UnifiedOptions);
        return targetOptions;
    }
}
