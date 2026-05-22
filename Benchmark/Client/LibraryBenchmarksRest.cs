using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.Options;
using OKX.Net.Clients;
using OKX.Net.Objects.Options;

namespace OKX.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class LibraryBenchmarksRest
    {
        public OKXRestClient RestClient;

        [GlobalSetup(Targets = [nameof(RestServerTime), nameof(RestTicker)])]
        public void Setup()
        {
            CreateClient();
        }

        [Benchmark]
        public async Task RestServerTime()
        {
            for (var i = 0; i < 1000; i++)
                _ = await RestClient.UnifiedApi.ExchangeData.GetServerTimeAsync();
        }

        [Benchmark]
        public async Task RestTicker()
        {
            for (var i = 0; i < 1000; i++)
                _ = await RestClient.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            RestClient?.Dispose();
        }

        private void CreateClient()
        {
            var env = OKXEnvironment.CreateCustom("Benchmark", "http://localhost:" + Program.ServerPort, "ws://localhost:" + Program.ServerPort);
            RestClient = new OKXRestClient(null, null, Options.Create(new OKXRestOptions
            {
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }
}
