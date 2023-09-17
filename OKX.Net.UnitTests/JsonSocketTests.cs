
using NUnit.Framework;
using Newtonsoft.Json;
using System.Diagnostics;
using OKX.Net.Interfaces.Clients;
using static OKX.Net.UnitTests.TestHelpers;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.System;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Trade;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class JsonSocketTests
    {
        [Test]
        public async Task ValidateSymbolUpdateStreamJson()
        {
            await TestFileToObject<OKXInstrument>(@"JsonResponses/Unified/Socket/SymbolUpdate.txt");
        }

        [Test]
        public async Task ValidateTickerUpdateStreamJson()
        {
            await TestFileToObject<OKXTicker>(@"JsonResponses/Unified/Socket/TickerUpdate.txt");
        }

        [Test]
        public async Task ValidateOpenInterestUpdateStreamJson()
        {
            await TestFileToObject<OKXOpenInterest>(@"JsonResponses/Unified/Socket/OpenInterestUpdate.txt");
        }

        [Test]
        public async Task ValidateKlineUpdateStreamJson()
        {
            await TestFileToObject<OKXCandlestick>(@"JsonResponses/Unified/Socket/KlineUpdate.txt");
        }

        [Test]
        public async Task ValidateTradeUpdateStreamJson()
        {
            await TestFileToObject<OKXTrade>(@"JsonResponses/Unified/Socket/TradeUpdate.txt");
        }

        [Test]
        public async Task ValidateEstimatedPriceUpdateStreamJson()
        {
            await TestFileToObject<OKXEstimatedPrice>(@"JsonResponses/Unified/Socket/EstimatedPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateMarkPriceUpdateStreamJson()
        {
            await TestFileToObject<OKXMarkPrice>(@"JsonResponses/Unified/Socket/MarkPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateMarkPriceKlineUpdateStreamJson()
        {
            await TestFileToObject<OKXCandlestick>(@"JsonResponses/Unified/Socket/MarkPriceKlineUpdate.txt");
        }

        [Test]
        public async Task ValidatePriceLimitUpdateStreamJson()
        {
            await TestFileToObject<OKXLimitPrice>(@"JsonResponses/Unified/Socket/PriceLimitUpdate.txt");
        }

        [Test]
        public async Task ValidateOrderBookUpdateStreamJson()
        {
            await TestFileToObject<OKXOrderBook>(@"JsonResponses/Unified/Socket/OrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateOptionSummaryUpdateStreamJson()
        {
            await TestFileToObject<OKXOptionSummary>(@"JsonResponses/Unified/Socket/OptionSummaryUpdate.txt");
        }

        [Test]
        public async Task ValidateFundingRateUpdateStreamJson()
        {
            await TestFileToObject<OKXFundingRate>(@"JsonResponses/Unified/Socket/FundingRateUpdate.txt");
        }

        [Test]
        public async Task ValidateIndexKlineUpdateStreamJson()
        {
            await TestFileToObject<OKXCandlestick>(@"JsonResponses/Unified/Socket/IndexKlineUpdate.txt");
        }

        [Test]
        public async Task ValidateIndexTickerUpdateStreamJson()
        {
            await TestFileToObject<OKXIndexTicker>(@"JsonResponses/Unified/Socket/IndexTickerUpdate.txt");
        }

        [Test]
        public async Task ValidateStatusUpdateStreamJson()
        {
            await TestFileToObject<OKXStatus>(@"JsonResponses/Unified/Socket/SystemStatusUpdate.txt");
        }

        [Test]
        public async Task ValidateAccountUpdateStreamJson()
        {
            await TestFileToObject<OKXAccountBalance>(@"JsonResponses/Unified/Socket/AccountUpdate.txt");
        }

        [Test]
        public async Task ValidatePositionUpdateStreamJson()
        {
            await TestFileToObject<OKXPosition>(@"JsonResponses/Unified/Socket/PositionUpdate.txt", ignoreProperties: new List<string> { "closeOrderAlgo" });
        }

        [Test]
        public async Task ValidateBalanceAndPositionUpdateStreamJson()
        {
            await TestFileToObject<OKXPositionAndBalanceUpdate>(@"JsonResponses/Unified/Socket/BalanceAndPositionUpdate.txt");
        }

        [Test]
        public async Task ValidateOrderUpdateStreamJson()
        {
            await TestFileToObject<OKXOrderUpdate>(@"JsonResponses/Unified/Socket/OrderUpdate.txt", ignoreProperties: new List<string> { "pTime", "eventType", "code", "msg" });
        }

        [Test]
        public async Task ValidateAlgoOrderUpdateStreamJson()
        {
            await TestFileToObject<OKXAlgoOrderUpdate>(@"JsonResponses/Unified/Socket/AlgoOrderUpdate.txt", ignoreProperties: new List<string> { "pTime", "eventType", "code", "msg" });
        }

        [Test]
        public async Task ValidateAdvancedAlgoOrderUpdateStreamJson()
        {
            await TestFileToObject<OKXAlgoOrderUpdate>(@"JsonResponses/Unified/Socket/AdvancedAlgoOrderUpdate.txt", ignoreProperties: new List<string> { "pTime", "eventType", "code", "msg" });
        }

        [Test]
        public async Task ValidateLiquidationWarningUpdateStreamJson()
        {
            await TestFileToObject<OKXPosition>(@"JsonResponses/Unified/Socket/LiquidationWarningUpdate.txt", ignoreProperties: new List<string> { "pTime", "eventType", "code", "msg" });
        }

        private static async Task TestFileToObject<T>(string filePath, List<string> ignoreProperties = null)
        {
            var listener = new EnumValueTraceListener();
            Trace.Listeners.Add(listener);
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string json;
            try
            {
                var file = File.OpenRead(Path.Combine(path, filePath));
                using var reader = new StreamReader(file);
                json = await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            var result = JsonConvert.DeserializeObject<T>(json);
            JsonToObjectComparer<IOKXSocketClient>.ProcessData("", result, json, ignoreProperties: new Dictionary<string, List<string>>
            {
                { "", ignoreProperties ?? new List<string>() }
            });
            Trace.Listeners.Remove(listener);
        }
    }
}
