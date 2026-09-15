using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;
using OKX.Net.SymbolOrderBooks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IOKXRestClient and IOKXSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/OKX.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddOKX(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = OKXOptions.CreateFromConfiguration(configuration);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddOKXCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IOKXRestClient and IOKXSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the OKX services</param>
        /// <returns></returns>
        public static IServiceCollection AddOKX(
            this IServiceCollection services,
            Action<OKXOptions>? optionsDelegate = null)
        {
            var options = OKXOptions.Create(optionsDelegate);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddOKXCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddOKXCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IOKXRestClient, OKXRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<OKXRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new OKXRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<OKXRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<OKXRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IOKXSocketClient), x => { return new OKXSocketClient(x.GetRequiredService<IOptions<OKXSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IOKXOrderBookFactory, OKXOrderBookFactory>();
            services.AddTransient<IOKXTrackerFactory, OKXTrackerFactory>();
            services.AddTransient<ITrackerFactory, OKXTrackerFactory>();
            services.AddSingleton<IOKXUserClientProvider, OKXUserClientProvider>(x =>
            new OKXUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IOKXRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<OKXRestOptions>>(),
                x.GetRequiredService<IOptions<OKXSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IOKXRestClient>().UnifiedApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IOKXSocketClient>().UnifiedApi.SharedClient);

            services.RegisterSharedApiClient<
                IOKXSharedApiClient,
                OKXSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.Rest)
                    .Add(client => client.Socket)
                    );

            return services;
        }
    }
}
