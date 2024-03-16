using CryptoExchange.Net.Clients;
using OKX.Net.Clients.UnifiedApi;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;

namespace OKX.Net.Clients;

/// <inheritdoc />
public class OKXSocketClient : BaseSocketClient, IOKXSocketClient
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    public IOKXSocketClientUnifiedApi UnifiedApi { get; }

    #region ctor
    /// <summary>
    /// Create a new instance of the OKXSocketClient
    /// </summary>
    /// <param name="loggerFactory">The logger</param>
    public OKXSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
    {
    }

    /// <summary>
    /// Create a new instance of the OKXSocketClient
    /// </summary>
    /// <param name="optionsDelegate">Option configuration delegate</param>
    public OKXSocketClient(Action<OKXSocketOptions> optionsDelegate) : this(optionsDelegate, null)
    {
    }

    /// <summary>
    /// Create a new instance of the OKXSocketClient
    /// </summary>
    /// <param name="loggerFactory">The logger</param>
    /// <param name="optionsDelegate">Option configuration delegate</param>
    public OKXSocketClient(Action<OKXSocketOptions> optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "OKX")
    {
        var options = OKXSocketOptions.Default.Copy();
        optionsDelegate(options);
        Initialize(options);

        UnifiedApi = AddApiClient(new OKXSocketClientUnifiedApi(_logger, options));
    }
    #endregion

    #region Common Methods
    /// <summary>
    /// Set default options
    /// </summary>
    /// <param name="optionsDelegate"></param>
    public static void SetDefaultOptions(Action<OKXSocketOptions> optionsDelegate)
    {
        var options = OKXSocketOptions.Default.Copy();
        optionsDelegate(options);
        OKXSocketOptions.Default = options;
    }

    /// <inheritdoc />
    public virtual void SetApiCredentials(OKXApiCredentials credentials)
    {
        UnifiedApi.SetApiCredentials(credentials.Copy());
    }

    #endregion
}