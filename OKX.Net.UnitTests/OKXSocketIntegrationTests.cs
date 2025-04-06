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
        public override bool Run { get; set; } = true;

        public OKXSocketIntegrationTests()
        {
        }

        public override OKXSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
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
