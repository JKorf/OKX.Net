using OKX.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class OKXSharedApiRestIntegrationTest : SharedApiRestIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            await ExecuteAsync();
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddOKX();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IOKXSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedRest client, CapabilityOptions options)
        {
            if (options.SupportedTradingModes.Contains(TradingMode.Spot))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");
            else
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");
        }

        protected override Task TestCapability<TCapability, TResult>(List<Exception> output, Func<SharedApiTestConfig, TCapability, Task<HttpResult<TResult>>> method)
        {
            if (typeof(TCapability) == typeof(IGetIndexPriceKlinesRest))
                return Task.CompletedTask;

            return base.TestCapability(output, method);
        }
    }

    [TestFixture]
    public class OKXSharedApiSocketIntegrationTest : SharedApiSocketIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            var result = await ExecuteAsync();
            foreach (var item in result)
                Assert.Warn(item.Message);
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddOKX();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IOKXSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedSocket client, CapabilityOptions options)
        {
            if (options.SupportedTradingModes.Contains(TradingMode.Spot))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");
            else
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");
        }
    }
}
