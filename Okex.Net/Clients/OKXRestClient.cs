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
    public OKXRestClient(Action<OKXRestOptions> optionsDelegate) : this(null, null, optionsDelegate)
    {
    }

    /// <summary>
    /// Create a new instance of the OKXRestClient using default options
    /// </summary>
    public OKXRestClient(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) : this(httpClient, loggerFactory, null)
    {
    }

    /// <summary>
    /// Create a new instance of the OKXRestClient
    /// </summary>
    /// <param name="optionsDelegate">Option configuration delegate</param>
    /// <param name="loggerFactory">The logger factory</param>
    /// <param name="httpClient">Http client for this client</param>
    public OKXRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<OKXRestOptions>? optionsDelegate = null)
        : base(loggerFactory, "OKX")
    {
        var options = OKXRestOptions.Default.Copy();
        if (optionsDelegate != null)
            optionsDelegate(options);
        Initialize(options);

        UnifiedApi = AddApiClient(new OKXRestClientUnifiedApi(_logger, httpClient, options));
    }
    #endregion

    #region Common Methods
    /// <summary>
    /// Sets the default options to use for new clients
    /// </summary>
    /// <param name="optionsDelegate">Callback for setting the options</param>
    public static void SetDefaultOptions(Action<OKXRestOptions> optionsDelegate)
    {
        var options = OKXRestOptions.Default.Copy();
        optionsDelegate(options);
        OKXRestOptions.Default = options;
    }

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="credentials">API Credentials Object</param>
    public void SetApiCredentials(OKXApiCredentials credentials)
    {
        UnifiedApi.SetApiCredentials(credentials);
    }
    #endregion
}
