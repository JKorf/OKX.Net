﻿using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;
using OKX.Net.SymbolOrderBooks;
using System.Net;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IOKXRestClient and IOKXSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddOKX(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new OKXOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? OKXEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? OKXEnvironment.Live.Name;
            options.Rest.Environment = OKXEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Rest.AllowAppendingClientOrderId = options.Rest.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;
            options.Socket.Environment = OKXEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;
            options.Socket.AllowAppendingClientOrderId = options.Socket.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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
            var options = new OKXOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? OKXEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Rest.AllowAppendingClientOrderId = options.Rest.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? OKXEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;
            options.Socket.AllowAppendingClientOrderId = options.Socket.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddOKXCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// DEPRECATED; use <see cref="AddOKX(IServiceCollection, Action{OKXOptions}?)" /> instead
        /// </summary>
        public static IServiceCollection AddOKX(
            this IServiceCollection services,
            Action<OKXRestOptions> restDelegate,
            Action<OKXSocketOptions>? socketDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.Configure<OKXRestOptions>((x) => { restDelegate?.Invoke(x); });
            services.Configure<OKXSocketOptions>((x) => { socketDelegate?.Invoke(x); });

            return AddOKXCore(services, socketClientLifeTime);
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
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var handler = new HttpClientHandler();
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                catch (PlatformNotSupportedException)
                { }

                var options = serviceProvider.GetRequiredService<IOptions<OKXRestOptions>>().Value;
                if (options.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{options.Proxy.Host}:{options.Proxy.Port}"),
                        Credentials = options.Proxy.Password == null ? null : new NetworkCredential(options.Proxy.Login, options.Proxy.Password)
                    };
                }
                return handler;
            });
            services.Add(new ServiceDescriptor(typeof(IOKXSocketClient), x => { return new OKXSocketClient(x.GetRequiredService<IOptions<OKXSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));


            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddTransient<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IOKXOrderBookFactory, OKXOrderBookFactory>();
            services.AddTransient<IOKXTrackerFactory, OKXTrackerFactory>();
            services.AddTransient(x => x.GetRequiredService<IOKXRestClient>().UnifiedApi.CommonSpotClient);

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IOKXRestClient>().UnifiedApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IOKXSocketClient>().UnifiedApi.SharedClient);

            return services;
        }
    }
}
