using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ccxt.pro;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using OKX.Api;
using OKX.Net.Clients;
using OKX.Net.Objects.Options;

namespace OKX.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class LibraryComparisonBenchmarksSocket
    {
        private static readonly int _socketUpdateReceiveTarget = 100_000;

        private OKXSocketClient _okxNetClient;
        private ccxt.pro.okx _ccxtClient;
        private OkxWebSocketApiClient _okxApiClient;

        [GlobalSetup]
        public void Setup()
        {
            var env = OKXEnvironment.CreateCustom(
                "Benchmark",
                "http://localhost:" + Program.ServerPort,
                "ws://localhost:" + Program.ServerPort);

            _okxNetClient = new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled, 
                RateLimiterEnabled = false,
                Environment = env
            }), null);

            _okxApiClient = new OkxWebSocketApiClient(new OkxWebSocketApiOptions
            {
                    
            });
            OkxAddress.Default.WebSocketPublicAddress = "ws://localhost:" + Program.ServerPort + "/ws/v5/public";

            _ccxtClient = new okx(new Dictionary<string, object>
            {
                ["urls"] = new Dictionary<string, object>
                {
                    ["api"] = new Dictionary<string, object>
                    {
                        ["ws"] = "ws://localhost:" + Program.ServerPort + "/ws/v5"
                    }
                }
            });
            _ccxtClient.enableRateLimit = false;
        }

        [Benchmark(Baseline = true), IterationCount(25)]
        public async Task OKXNet_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await _okxNetClient.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync("ETH-USDT", x =>
            {
                received++;
                if (received >= _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task OKXApi_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await _okxApiClient.Public.SubscribeToTradesAsync(x =>
            {
                received++;
                if (received >= _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, "ETH-USDT");

            await waitEvent.WaitAsync();
            await _okxApiClient.UnsubscribeAllAsync(); // UnsubscribeAll to prevent sending unsub message and waiting for it
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    var trades = await _ccxtClient.WatchTrades("ETH/USDT");
                    received += trades.Count;

                    if (received >= _socketUpdateReceiveTarget)
                        break;
                }

                waitEvent.Set();
            });

            await waitEvent.WaitAsync();
            await _ccxtClient.Close();
        }

        [Benchmark, IterationCount(25)]
        public async Task OKXNetShared_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var request = new SubscribeTradeRequest(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"));
            var result = await _okxNetClient.UnifiedApi.SharedClient.SubscribeToTradeUpdatesAsync(request, x =>
            {
                received += x.Data.Length;
                if (received >= _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _okxNetClient?.Dispose();
        }
    }
}
