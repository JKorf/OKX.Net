using Microsoft.Extensions.DependencyInjection;
using OKX.Net.Clients;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;
using OKX.Net.SymbolOrderBooks;
using System.Net;

namespace OKX.Net;

/// <summary>
/// Helper methods
/// </summary>
public static class OKXHelpers
{
    internal static bool IsIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    internal static bool IsNotIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    /// Add the IOKXClient and IOKXSocketClient to the sevice collection so they can be injected
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
    /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
    /// <param name="socketClientLifeTime">The lifetime of the IOKXSocketClient for the service collection. Defaults to Singleton.</param>
    /// <returns></returns>
    public static IServiceCollection AddOKX(
        this IServiceCollection services,
        Action<OKXRestOptions>? defaultRestOptionsDelegate = null,
        Action<OKXSocketOptions>? defaultSocketOptionsDelegate = null,
        ServiceLifetime? socketClientLifeTime = null)
    {
        var restOptions = OKXRestOptions.Default.Copy();

        if (defaultRestOptionsDelegate != null)
        {
            defaultRestOptionsDelegate(restOptions);
            OKXRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
        }

        if (defaultSocketOptionsDelegate != null)
            OKXSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

        services.AddHttpClient<IOKXRestClient, OKXRestClient>(options =>
        {
            options.Timeout = restOptions.RequestTimeout;
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            if (restOptions.Proxy != null)
            {
                handler.Proxy = new WebProxy
                {
                    Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                    Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                };
            }
            return handler;
        });

        services.AddSingleton<IOKXOrderBookFactory, OKXOrderBookFactory>();
        services.AddTransient<IOKXRestClient, OKXRestClient>();
        services.AddTransient(x => x.GetRequiredService<IOKXRestClient>().UnifiedApi.CommonSpotClient);
        if (socketClientLifeTime == null)
            services.AddSingleton<IOKXSocketClient, OKXSocketClient>();
        else
            services.Add(new ServiceDescriptor(typeof(IOKXSocketClient), typeof(OKXSocketClient), socketClientLifeTime.Value));
        return services;
    }
}