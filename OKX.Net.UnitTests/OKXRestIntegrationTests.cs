using OKX.Net.Clients;
using OKX.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OKX.Net.Objects.Options;
using OKX.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects.Errors;

namespace OKX.Net.UnitTests
{
    [NonParallelizable]
    internal class OKXRestIntegrationTests : RestIntegrationTest<OKXRestClient>
    {
        public override bool Run { get; set; }

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
                ApiCredentials = Authenticated ? new ApiCredentials(key, sec, pass) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().UnifiedApi.ExchangeData.GetKlinesAsync("TSTTST", Enums.KlineInterval.OneDay, default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("51001"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestAccount()
        {
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetAccountBalanceAsync(default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetPositionsAsync(default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetPositionHistoryAsync(default, default, default, default, default, default, default, 100, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetPositionRiskAsync(default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetBillHistoryAsync(default, default, default, default, default, default, default, default, 100, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetBillArchiveAsync(default, default, default, default, default, default, default, default, 100, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetAccountConfigurationAsync(default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetLeverageAsync("ETH-USDT", Enums.MarginMode.Isolated, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetMaximumAmountAsync("ETH-USDT", Enums.TradeMode.Cash, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetMaximumAvailableAmountAsync("ETH-USDT", Enums.TradeMode.Cash, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetFeeRatesAsync(Enums.InstrumentType.Spot, "ETH-USDT", default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetInterestAccruedAsync(default, default, default, default, default, 100, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetInterestRateAsync(default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetMaximumWithdrawalsAsync(default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetAssetsAsync(default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetFundingBalanceAsync(default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetFundingBillDetailsAsync(default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetDepositHistoryAsync(default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetWithdrawalHistoryAsync(default, default, default, default, default, 100, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Account.GetAssetValuationAsync(default, default), true);
        }

        [Test]
        public async Task TestExchangeData()
        {
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Futures, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetIndexTickersAsync("BTC", default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetOrderBookAsync("ETH-USDT", 1, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetKlineHistoryAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetIndexKlinesAsync("ETH-USD", Enums.KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT", default, default, default, 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.Get24HourVolumeAsync(default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetIndexComponentsAsync("ETH-USD", default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetBlockTickersAsync(Enums.InstrumentType.Swap, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetBlockTickerAsync("ETH-USD-SWAP", default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetBlockTradesAsync("ETH-USD-SWAP", default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetSymbolsAsync(Enums.InstrumentType.Futures, default, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetDeliveryExerciseHistoryAsync(Enums.InstrumentType.Futures, "BTC-USD", default, default, 100, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetOpenInterestsAsync(Enums.InstrumentType.Futures, default, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetFundingRatesAsync("ETH-USDT-SWAP", default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync("ETH-USDT-SWAP", default, default, 100, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetPriceLimitsAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetOptionMarketDataAsync("BTC-USD", default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetDiscountInfoAsync(default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetMarkPricesAsync(Enums.InstrumentType.Futures, default, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetPositionTiersAsync(Enums.InstrumentType.Futures, Enums.MarginMode.Cross, "BTC-USD", default, default, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetInterestRatesAsync(default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetVIPInterestRatesAsync(default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetUnderlyingAsync(Enums.InstrumentType.Futures, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetInsuranceFundAsync(Enums.InstrumentType.Futures, Enums.InsuranceType.All, "BTC-USD", default, default, default, 100, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.UnitConvertAsync(Enums.ConvertType.CurrencyToContract, "BTC-USD-SWAP", 1, Enums.ConvertUnit.Coin, 50000m, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsTakerVolumeAsync("ETH", Enums.InstrumentType.Spot, Enums.KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsMarginLendingRatioAsync("ETH", Enums.KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsLongShortRatioAsync("ETH", Enums.KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsContractSummaryAsync("ETH", Enums.KlineInterval.FiveMinutes, default, default, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsOptionsSummaryAsync("ETH", Enums.KlineInterval.OneDay, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsPutCallRatioAsync("ETH", Enums.KlineInterval.OneDay, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeExpiryAsync("ETH", Enums.KlineInterval.OneDay, default), false);
            await RunAndCheckResult(client => client.UnifiedApi.ExchangeData.GetTradeStatsTakerFlowAsync("ETH", Enums.KlineInterval.OneDay, default), false);
        }

        [Test]
        public async Task TestTrading()
        {
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, 100, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetOrderHistoryAsync(Enums.InstrumentType.Spot, default, default, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetOrderArchiveAsync(Enums.InstrumentType.Spot, default, default, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetUserTradesAsync(Enums.InstrumentType.Spot, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetUserTradesArchiveAsync(Enums.InstrumentType.Spot, default, default, default, default, default, 100, default, default, default, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetAlgoOrderListAsync(Enums.AlgoOrderType.OCO, default, default, default, default, default, 100, default), true);
            await RunAndCheckResult(client => client.UnifiedApi.Trading.GetAlgoOrderHistoryAsync(Enums.AlgoOrderType.OCO, Enums.AlgoOrderState.Effective, default, default, default, default, default, 100, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new OKXSymbolOrderBook("ETH-USDT"));
        }
    }
}
