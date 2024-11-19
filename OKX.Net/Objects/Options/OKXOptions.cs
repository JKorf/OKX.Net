using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Options;
/// <summary>
/// OKX options
/// </summary>
public class OKXOptions
{
    /// <summary>
    /// Rest client options
    /// </summary>
    public OKXRestOptions Rest { get; set; } = new OKXRestOptions();

    /// <summary>
    /// Socket client options
    /// </summary>
    public OKXSocketOptions Socket { get; set; } = new OKXSocketOptions();

    /// <summary>
    /// Trade environment. Contains info about URL's to use to connect to the API. Use `OKXEnvironment` to swap environment, for example `Environment = OKXEnvironment.Live`
    /// </summary>
    public OKXEnvironment? Environment { get; set; }

    /// <summary>
    /// The api credentials used for signing requests.
    /// </summary>
    public OKXApiCredentials? ApiCredentials { get; set; }

    /// <summary>
    /// The DI service lifetime for the IOKXSocketClient
    /// </summary>
    public ServiceLifetime? SocketClientLifeTime { get; set; }
}
