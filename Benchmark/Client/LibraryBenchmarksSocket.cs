using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Options;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Objects.Options;

namespace OKX.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class LibraryBenchmarksSocket
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                var baseJob = Job.Default;

                AddJob(
                    baseJob
                        .WithId("NET10_0")
                        .WithIterationCount(20)
                        .WithRuntime(CoreRuntime.Core10_0));
                AddJob(
                    baseJob
                        .WithId("NET481")
                        .WithIterationCount(20)
                        .WithRuntime(ClrRuntime.Net48));
            }
        }

        public OKXSocketClient SocketClient;

        private const int _socketUpdateReceiveTarget = 1_000_000;

        [GlobalSetup]
        public void Setup()
        {
            CreateClient();
        }

        [IterationSetup(Target = nameof(Socket100Topics))]
        public void IterationSetupMultiTopic()
        {
            Task.Run(async () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    var subTopics = new string[10];
                    for (var j = 0; j < 10; j++)
                        subTopics[j] = "DUMMY-" + i;

                    _ = await SocketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync(subTopics, x => { }, CancellationToken.None);
                    _ = await SocketClient.UnifiedApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(subTopics, OrderBookType.OrderBook, x => { }, CancellationToken.None);
                }
            }).Wait();
        }

        [IterationCleanup(Target = nameof(Socket100Topics))]
        public void IterationCleanUpMultiTopic()
        {
            SocketClient.UnifiedApi.UnsubscribeAllAsync().Wait();
        }

        [Benchmark]
        public async Task Socket1Topic()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync("ETH-USDT", x =>
            {
                received++;
                if (received >= _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark]
        public async Task Socket100Topics()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var topics = new string[100];
            for (var i = 0; i < 100; i++)
                topics[i] = "DUMMY";

            topics[50] = "ETH-USDT";

            var result = await SocketClient.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync(topics, x =>
            {
                received++;
                if (received >= _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            SocketClient?.Dispose();
        }

        private void CreateClient()
        {
            var env = OKXEnvironment.CreateCustom("Benchmark", "http://localhost:" + Program.ServerPort, "ws://localhost:" + Program.ServerPort);
            SocketClient = new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }
}
