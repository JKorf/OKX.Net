using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Objects.Options;
using OKX.Net.SymbolOrderBooks;

namespace OKX.Net.UnitTests
{
    [NonParallelizable]
    internal class OKXRestIntegrationTests : RestIntegrationTest<OKXRestClient>
    {
        public override bool Run { get; set; } = false;

        public OKXRestIntegrationTests()
        {
        }

        public override OKXRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new OKXRestClient(null, loggerFactory, Options.Create(new OKXRestOptions
            {
                OutputOriginalData = true,
                Environment = Authenticated ? OKXEnvironment.Europe : OKXEnvironment.Live,
                ApiCredentials = Authenticated ? new OKXCredentials(key, sec, pass) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().UnifiedApi.ExchangeData.GetKlinesAsync("TSTTST", KlineInterval.OneDay);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("51001"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestAccount()
        {
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetAccountBalanceAsync(default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetPositionsAsync(default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetPositionHistoryAsync(default, default, default, default, default, default, default, 100, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetPositionRiskAsync(default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetBillHistoryAsync(default, default, default, default, default, default, default, default, 100, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetBillArchiveAsync(default, default, default, default, default, default, default, default, 100, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetAccountConfigurationAsync(default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetLeverageAsync("ETH-USDT", MarginMode.Isolated, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetMaximumAmountAsync("ETH-USDT", TradeMode.Cash, default, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetMaximumAvailableAmountAsync("ETH-USDT", TradeMode.Cash, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetFeeRatesAsync(InstrumentType.Spot, "ETH-USDT", default, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetInterestAccruedAsync(default, default, default, default, default, 100, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetInterestRateAsync(default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetMaximumWithdrawalsAsync(default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetAssetsAsync(default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetFundingBalanceAsync(default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetFundingBillDetailsAsync(default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetFundingBillHistoryAsync(default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetDepositHistoryAsync(default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetWithdrawalHistoryAsync(default, default, default, default, default, 100, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Account.GetAssetValuationAsync(default, default), true);
        }

        [Test]
        public async Task TestExchangeData()
        {
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTickersAsync(InstrumentType.Futures, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT", default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetIndexTickersAsync("BTC", default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetOrderBookAsync("ETH-USDT", 1, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetKlinesAsync("ETH-USDT", KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetKlineHistoryAsync("ETH-USDT", KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetIndexKlinesAsync("ETH-USD", KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT", default, default, default, 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.Get24HourVolumeAsync(default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetIndexComponentsAsync("ETH-USD", default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetBlockTickersAsync(InstrumentType.Swap, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetBlockTickerAsync("ETH-USD-SWAP", default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetBlockTradesAsync("ETH-USD-SWAP", default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetSymbolsAsync(InstrumentType.Futures, default, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetDeliveryExerciseHistoryAsync(InstrumentType.Futures, "BTC-USD", default, default, 100, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetOpenInterestsAsync(InstrumentType.Futures, default, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetFundingRatesAsync("ETH-USDT-SWAP", default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync("ETH-USDT-SWAP", default, default, 100, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetPriceLimitsAsync("ETH-USDT", default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetOptionMarketDataAsync("BTC-USD", default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetDiscountInfoAsync(default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetMarkPricesAsync(InstrumentType.Futures, default, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetPositionTiersAsync(InstrumentType.Futures, MarginMode.Cross, "BTC-USD", default, default, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetInterestRatesAsync(default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetUnderlyingAsync(InstrumentType.Futures, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetInsuranceFundAsync(InstrumentType.Futures, InsuranceType.All, "BTC-USD", default, default, default, 100, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.UnitConvertAsync(ConvertType.CurrencyToContract, "BTC-USD-SWAP", 1, ConvertUnit.Coin, 50000m, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsTakerVolumeAsync("ETH", InstrumentType.Spot, KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsMarginLendingRatioAsync("ETH", KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsLongShortRatioAsync("ETH", KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsContractSummaryAsync("ETH", KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsOptionsSummaryAsync("ETH", KlineInterval.OneDay, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsPutCallRatioAsync("ETH", KlineInterval.OneDay, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeExpiryAsync("ETH", KlineInterval.OneDay, default), false);
            await RunAndCheckResult(c => c.UnifiedApi.ExchangeData.GetTradeStatsTakerFlowAsync("ETH", KlineInterval.OneDay, default), false);
        }

        [Test]
        public async Task TestTrading()
        {
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, 100, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetOrderHistoryAsync(InstrumentType.Spot, default, default, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetOrderArchiveAsync(InstrumentType.Spot, default, default, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetUserTradesAsync(InstrumentType.Spot, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetUserTradesArchiveAsync(InstrumentType.Spot, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetAlgoOrderListAsync(AlgoOrderType.OCO, default, default, default, default, default, 100, default), true);
            await RunAndCheckResult(c => c.UnifiedApi.Trading.GetAlgoOrderHistoryAsync(AlgoOrderType.OCO, AlgoOrderState.Effective, default, default, default, default, default, 100, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new OKXSymbolOrderBook("ETH-USDT"));
        }
    }
}
