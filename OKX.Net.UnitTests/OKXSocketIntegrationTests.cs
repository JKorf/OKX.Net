using OKX.Net.Clients;
using OKX.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using OKX.Net.Objects.Market;

namespace OKX.Net.UnitTests
{
    internal class OKXSocketIntegrationTests : SocketIntegrationTest<OKXSocketClient>
    {
        public override bool Run { get; set; } = false;

        public OKXSocketIntegrationTests()
        {
        }

        public override OKXSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null && pass != null;
            return new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec, pass) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            await RunAndCheckUpdate<OKXTicker>((client, updateHandler) => client.UnifiedApi.Account.SubscribeToAccountUpdatesAsync(default, default, default, default), false, true);
            await RunAndCheckUpdate<OKXTicker>((client, updateHandler) => client.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETH-USDT", updateHandler, default), true, false);
        } 
    }
}
