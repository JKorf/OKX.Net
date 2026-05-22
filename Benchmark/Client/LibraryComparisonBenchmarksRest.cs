using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ccxt;
using CryptoExchange.Net.SharedApis;
using OKX.Api;
using OKX.Net.Clients;

namespace OKX.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class LibraryComparisonBenchmarksRest
    {
        private OKXRestClient _okxNetClient;
        private OkxRestApiClient _okxApiClient;
        private okx _ccxtClient;

        [GlobalSetup]
        public void Setup()
        {
            var env = OKXEnvironment.CreateCustom(
                "Benchmark",
                "http://localhost:" + Program.ServerPort,
                "ws://localhost:" + Program.ServerPort);

            _okxNetClient = new OKXRestClient(x =>
            {
                x.RateLimiterEnabled = false;
                x.Environment = env;
            });

#pragma warning disable CS0612 // Type or member is obsolete
            _okxApiClient = new OkxRestApiClient(new OkxRestApiOptions
            {
                BaseAddress = "http://localhost:" + Program.ServerPort,
                RateLimiterEnabled = false,
                RateLimiters = []
            });
#pragma warning restore CS0612 // Type or member is obsolete
            OkxAddress.Default.RestApiAddress = "http://localhost:" + Program.ServerPort;

            _ccxtClient = new okx(new Dictionary<string, object>
            {
                ["urls"] = new Dictionary<string, object>
                {
                    ["api"] = new Dictionary<string, object>
                    {
                        ["rest"] = "http://localhost:" + Program.ServerPort
                    }
                }
            });
            _ccxtClient.enableRateLimit = false;
        }

        [Benchmark(Baseline = true), IterationCount(25)]
        public async Task OKXNet_ServerTime()
        {
            for (var i = 0; i < 1; i++)
                _ = await _okxNetClient.UnifiedApi.ExchangeData.GetServerTimeAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task OKXApi_ServerTime()
        {
            for (var i = 0; i < 1; i++)
                _ = await _okxApiClient.Public.GetServerTimeAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_ServerTime()
        {
            for (var i = 0; i < 1; i++)
                _ = await _ccxtClient.publicGetPublicTime();
        }

        [Benchmark, IterationCount(25)]
        public async Task OKXNet_Ticker()
        {
            for (var i = 0; i < 1; i++)
                _ = await _okxNetClient.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
        }

        [Benchmark, IterationCount(25)]
        public async Task OKXApi_Ticker()
        {
            for (var i = 0; i < 1; i++)
                _ = await _okxApiClient.Public.GetTickerAsync("ETH-USDT");
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_Ticker()
        {
            for (var i = 0; i < 1; i++)
                _ = await _ccxtClient.publicGetMarketTicker(new Dictionary<string, object>
                {
                    ["instId"] = "ETH-USDT"
                });
        }

        [Benchmark, IterationCount(25)]
        public async Task OKXNetShared_Ticker()
        {
            var request = new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"));
            for (var i = 0; i < 1; i++)
                _ = await _okxNetClient.UnifiedApi.SharedClient.GetSpotTickerAsync(request);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _okxNetClient?.Dispose();
        }
    }
}
