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
    /// <param name="optionsDelegate">Option configuration delegate</param>
    public OKXSocketClient(Action<OKXSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
    {
    }

    /// <summary>
    /// Create a new instance of the OKXSocketClient
    /// </summary>
    /// <param name="loggerFactory">The logger</param>
    /// <param name="options">Option configuration delegate</param>
    public OKXSocketClient(IOptions<OKXSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "OKX")
    {
        Initialize(options.Value);

        UnifiedApi = AddApiClient(new OKXSocketClientUnifiedApi(_logger, options.Value));
    }
    #endregion

    /// <inheritdoc />
    public void SetOptions(UpdateOptions options)
    {
        UnifiedApi.SetOptions(options);
    }

    /// <summary>
    /// Set default options
    /// </summary>
    /// <param name="optionsDelegate"></param>
    public static void SetDefaultOptions(Action<OKXSocketOptions> optionsDelegate)
    {
        OKXSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
    }

    /// <inheritdoc />
    public virtual void SetApiCredentials(ApiCredentials credentials)
    {
        UnifiedApi.SetApiCredentials(credentials.Copy());
    }
}