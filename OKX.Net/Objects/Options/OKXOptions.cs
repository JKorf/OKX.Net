using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using System;

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
    /// <summary>
    /// Create OKXOptions instance using the provided configuration action
    /// </summary>
    public static OKXOptions Create(Action<OKXOptions>? configure = null)
    {
        var options = CreateUnconfigured();
        configure?.Invoke(options);
        return Normalize(options);
    }

    /// <summary>
    /// Create OKXOptions using the provided IConfiguration
    /// </summary>
    public static OKXOptions CreateFromConfiguration(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var options = CreateUnconfigured();
        try
        {
            configuration.Bind(options);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Invalid OKX configuration provided", ex);
        }

        if (options.Environment != null)
            options.Environment = OKXEnvironment.GetEnvironmentByName(options.Environment.Name) ?? options.Environment;
        if (options.Rest?.Environment != null)
            options.Rest.Environment = OKXEnvironment.GetEnvironmentByName(options.Rest.Environment.Name) ?? options.Rest.Environment;
        if (options.Socket?.Environment != null)
            options.Socket.Environment = OKXEnvironment.GetEnvironmentByName(options.Socket.Environment.Name) ?? options.Socket.Environment;

        return Normalize(options);
    }

    private static OKXOptions CreateUnconfigured()
    {
        var options = new OKXOptions();
        options.Rest.Environment = null!;
        options.Socket.Environment = null!;
        return options;
    }

    private static OKXOptions Normalize(OKXOptions options)
    {
        if (options.Rest == null)
            throw new ArgumentException("REST options cannot be null", nameof(options));
        if (options.Socket == null)
            throw new ArgumentException("Socket options cannot be null", nameof(options));

        options.Rest.Environment ??= options.Environment ?? OKXEnvironment.Live;
        options.Rest.ApiCredentials ??= options.ApiCredentials;
        options.Rest.SharedApiEuropeUseXPerps |= options.SharedApiEuropeUseXPerps;
        options.Socket.Environment ??= options.Environment ?? OKXEnvironment.Live;
        options.Socket.ApiCredentials ??= options.ApiCredentials;
        options.Socket.SharedApiEuropeUseXPerps |= options.SharedApiEuropeUseXPerps;
        return options;
    }
}
