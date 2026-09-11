using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace OKX.Net.Objects.Options;
/// <summary>
/// OKX options
/// </summary>
public class OKXOptions : LibraryOptions<OKXRestOptions, OKXSocketOptions, OKXCredentials, OKXEnvironment>
{
    /// <summary>
    /// Whether to use XPerps as perpetual linear contracts when using the Shared API's
    /// </summary>
    public bool SharedApiEuropeUseXPerps
    {
        get => SharedApi.EuropeUseXPerps;
        set => SharedApi.EuropeUseXPerps = value;
    }

    /// <summary>
    /// Options for Shared API usage
    /// </summary>
    public OKXSharedApiOptions SharedApi { get; set; } = new();
}
