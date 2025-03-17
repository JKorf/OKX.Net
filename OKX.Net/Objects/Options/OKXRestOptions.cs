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
    /// Whether to allow the client to adjust the clientOrderId parameter send by the user when placing orders to include a client reference. This reference is used by the exchange to allocate a small percentage of the paid trading fees to developer of this library. Defaults to false.<br />
    /// Note that:<br />
    /// * It does not impact the amount of fees a user pays in any way<br />
    /// * It does not impact functionality. The reference is added just before sending the request and removed again during data deserialization<br />
    /// * It does respect client order id field limitations. For example if the user provided client order id parameter is too long to fit the reference it will not be added<br />
    /// * Toggling this option might fail operations using a clientOrderId parameter for pre-existing orders which were placed before the toggle. Operations on orders placed after the toggle will work as expected. It's adviced to toggle when there are no open orders
    /// </summary>
    public bool AllowAppendingClientOrderId { get; set; } = false;

    /// <summary>
    /// Options for the  unified API
    /// </summary>
    public RestApiOptions UnifiedOptions { get; private set; } = new RestApiOptions();

    internal OKXRestOptions Set(OKXRestOptions targetOptions)
    {
        targetOptions = base.Set<OKXRestOptions>(targetOptions);
        targetOptions.SignPublicRequests = SignPublicRequests;
        targetOptions.AllowAppendingClientOrderId = AllowAppendingClientOrderId;
        targetOptions.UnifiedOptions = UnifiedOptions.Set(targetOptions.UnifiedOptions);
        return targetOptions;
    }
}
