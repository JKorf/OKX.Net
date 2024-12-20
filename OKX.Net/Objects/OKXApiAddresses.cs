namespace OKX.Net.Objects;

/// <summary>
/// Api addresses
/// </summary>
public class OKXApiAddresses
{
    /// <summary>
    /// Base rest address
    /// </summary>
    public string UnifiedRestAddress { get; set; } = string.Empty;
    /// <summary>
    /// Base socket address
    /// </summary>
    public string UnifiedSocketAddress { get; set; } = string.Empty;

    /// <summary>
    /// Default live addresses
    /// </summary>
    public static OKXApiAddresses Default = new OKXApiAddresses
    {
        UnifiedRestAddress = "https://www.okx.com",
        UnifiedSocketAddress = "wss://ws.okx.com:8443",
    };

    /// <summary>
    /// Europe customers addresses
    /// </summary>
    public static OKXApiAddresses Europe = new OKXApiAddresses
    {
        UnifiedRestAddress = "https://eea.okx.com",
        UnifiedSocketAddress = "wss://wseea.okx.com:8443",
    };

    /// <summary>
    /// Demo addresses
    /// </summary>
    public static OKXApiAddresses Demo = new OKXApiAddresses
    {
        UnifiedRestAddress = "https://www.okx.com",
        UnifiedSocketAddress = "wss://wspap.okx.com:8443",
    };
}
