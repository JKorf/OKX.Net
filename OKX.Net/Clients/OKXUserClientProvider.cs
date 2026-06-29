using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using CryptoExchange.Net.Clients;

namespace OKX.Net.Clients
{
    /// <inheritdoc />
    public class OKXUserClientProvider : UserClientProvider<
        IOKXRestClient,
        IOKXSocketClient,
        OKXRestOptions,
        OKXSocketOptions,
        OKXCredentials,
        OKXEnvironment
        >, IOKXUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => OKXExchange.ExchangeName;

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
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IOKXRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<OKXRestOptions> options)
            => new OKXRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IOKXSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<OKXSocketOptions> options)
            => new OKXSocketClient(options, loggerFactory);
    }
}
