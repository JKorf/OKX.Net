using CryptoExchange.Net.Objects.Options;

namespace OKX.Net.Objects.Options;

/// <summary>
/// Rest client options
/// </summary>
public class OKXRestOptions : RestExchangeOptions<OKXEnvironment, OKXApiCredentials>
{
    /// <summary>
    /// Default options for new OKXRestClients
    /// </summary>
    public static OKXRestOptions Default { get; set; } = new OKXRestOptions()
    {
        Environment = OKXEnvironment.Live
    };

    /// <summary>
    /// Whether or not to sign public requests
    /// </summary>
    public bool SignPublicRequests { get; set; }

    /// <summary>
    /// Broker ID for earning rebates
    /// </summary>
    public string? BrokerId { get; set; }

    /// <summary>
    /// Options for the  unified API
    /// </summary>
    public RestApiOptions UnifiedOptions { get; private set; } = new RestApiOptions();

    internal OKXRestOptions Copy()
    {
        var options = Copy<OKXRestOptions>();
        options.SignPublicRequests = SignPublicRequests;
        options.BrokerId = BrokerId;
        options.UnifiedOptions = UnifiedOptions.Copy<RestApiOptions>();
        return options;
    }
}
