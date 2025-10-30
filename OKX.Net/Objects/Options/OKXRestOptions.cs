using CryptoExchange.Net.Objects.Options;

namespace OKX.Net.Objects.Options;

/// <summary>
/// Rest client options
/// </summary>
public class OKXRestOptions : RestExchangeOptions<OKXEnvironment, ApiCredentials>
{
    /// <summary>
    /// Default options for new OKXRestClients
    /// </summary>
    internal static OKXRestOptions Default { get; set; } = new OKXRestOptions()
    {
        Environment = OKXEnvironment.Live
    };

    /// <summary>
    /// ctor
    /// </summary>
    public OKXRestOptions()
    {
        Default?.Set(this);
    }

    /// <summary>
    /// Whether or not to sign public requests
    /// </summary>
    public bool SignPublicRequests { get; set; }

    /// <summary>
    /// Broker id
    /// </summary>
    public string? BrokerId { get; set; }

    /// <summary>
    /// Options for the  unified API
    /// </summary>
    public RestApiOptions UnifiedOptions { get; private set; } = new RestApiOptions();

    internal OKXRestOptions Set(OKXRestOptions targetOptions)
    {
        targetOptions = base.Set<OKXRestOptions>(targetOptions);
        targetOptions.SignPublicRequests = SignPublicRequests;
        targetOptions.BrokerId = BrokerId;
        targetOptions.UnifiedOptions = UnifiedOptions.Set(targetOptions.UnifiedOptions);
        return targetOptions;
    }
}
