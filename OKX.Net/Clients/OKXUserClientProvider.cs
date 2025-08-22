using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Collections.Generic;

namespace OKX.Net.Clients
{
    /// <inheritdoc />
    public class OKXUserClientProvider : IOKXUserClientProvider
    {
        private static ConcurrentDictionary<string, IOKXRestClient> _restClients = new ConcurrentDictionary<string, IOKXRestClient>();
        private static ConcurrentDictionary<string, IOKXSocketClient> _socketClients = new ConcurrentDictionary<string, IOKXSocketClient>();

        private readonly IOptions<OKXRestOptions> _restOptions;
        private readonly IOptions<OKXSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public OKXUserClientProvider(Action<OKXOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public OKXUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<OKXRestOptions> restOptions,
            IOptions<OKXSocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, OKXEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IOKXRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, OKXEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IOKXSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, OKXEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IOKXRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, OKXEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new OKXRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOKXSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, OKXEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new OKXSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<OKXRestOptions> SetRestEnvironment(OKXEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new OKXRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<OKXSocketOptions> SetSocketEnvironment(OKXEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new OKXSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
