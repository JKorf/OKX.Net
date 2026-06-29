using CryptoExchange.Net.Objects.Options;

namespace OKX.Net.Objects.Options;
/// <summary>
/// OKX options
/// </summary>
public class OKXOptions : LibraryOptions<OKXRestOptions, OKXSocketOptions, OKXCredentials, OKXEnvironment>
{
    /// <summary>
    /// Whether to use XPerps as perpetual linear contracts when using the Shared API's
    /// </summary>
    public bool SharedApiEuropeUseXPerps { get; set; }
}
