using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Options;
using OKX.Net.Clients.UnifiedApi;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;

namespace OKX.Net.Clients;

/// <inheritdoc />
public class OKXRestClient : BaseRestClient, IOKXRestClient
{
    #region Internal Fields
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    public IOKXRestClientUnifiedApi UnifiedApi { get; }
    #endregion

    #region ctor
    /// <summary>
    /// Create a new instance of the OKXRestClient using provided options
    /// </summary>
    /// <param name="optionsDelegate">Option configuration delegate</param>
    public OKXRestClient(Action<OKXRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
    {
    }

    /// <summary>
    /// Create a new instance of the OKXRestClient
    /// </summary>
    /// <param name="options">Option configuration</param>
    /// <param name="loggerFactory">The logger factory</param>
    /// <param name="httpClient">Http client for this client</param>
    public OKXRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<OKXRestOptions> options)
        : base(loggerFactory, "OKX")
    {
        Initialize(options.Value);

        UnifiedApi = AddApiClient(new OKXRestClientUnifiedApi(_logger, httpClient, options.Value));
    }
    #endregion

    /// <inheritdoc />
    public void SetOptions(UpdateOptions options)
    {
        UnifiedApi.SetOptions(options);
    }

    /// <summary>
    /// Sets the default options to use for new clients
    /// </summary>
    /// <param name="optionsDelegate">Callback for setting the options</param>
    public static void SetDefaultOptions(Action<OKXRestOptions> optionsDelegate)
    {
        OKXRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
    }

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="credentials">API Credentials Object</param>
    public void SetApiCredentials(ApiCredentials credentials)
    {
        UnifiedApi.SetApiCredentials(credentials);
    }
}
