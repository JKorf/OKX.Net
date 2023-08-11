
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using OKX.Net.UnitTests;
using CryptoExchange.Net.Authentication;
using OKX.Net.Objects;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IOKXRestClient> _comparer = new JsonToObjectComparer<IOKXRestClient>((json) => TestHelpers.CreateResponseClient(json, x =>
        {
            x.ApiCredentials = new OKXApiCredentials("1234", "1234", "11");
            x.UnifiedOptions.RateLimiters = new List<IRateLimiter>();
            x.UnifiedOptions.OutputOriginalData = false;
        }));

        [Test]
        public async Task ValidateSpotAccountCalls()
        {   
            await _comparer.ProcessSubject("Unified/Account", c => c.UnifiedApi.Account,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
                takeFirstItemForCompare: new List<string>
               {
                   "CancelWithdrawalAsync",
                   "GetAccountBalanceAsync",
                   "GetAccountConfigurationAsync",
                   "GetFeeRatesAsync",
                   "GetLightningWithdrawalsAsync",
                   "SavingPurchaseRedemptionAsync",
                   "SetAccountPositionModeAsync",
                   "SetGreeksAsync",
                   "TransferAsync",
                   "WithdrawAsync"
                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetAccountConfigurationAsync", new List<string>{ "traderInsts" } },
                    { "GetAccountPositionsAsync", new List<string>{ "closeOrderAlgo" } }
                }
                );
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            await _comparer.ProcessSubject("Unified/ExchangeData", c => c.UnifiedApi.ExchangeData,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               takeFirstItemForCompare: new List<string>
               {
                   "Get24HourVolumeAsync",
                   "GetBlockTickerAsync",
                   "GetEstimatedPriceAsync",
                   "GetIndexComponentsAsync",
                   "GetInsuranceFundAsync",
                   "GetInterestRatesAsync",
                   "GetLimitPriceAsync",
                   "GetOrderBookAsync",
                   "GetTickerAsync",
                   "UnitConvertAsync"
               }
                );
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            await _comparer.ProcessSubject("Unified/Trading", c => c.UnifiedApi.Trading,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>>
               {
               }
                );
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            await _comparer.ProcessSubject("Unified/SubAccounts", c => c.UnifiedApi.Account,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>> {                
               }
                );
        }
    }
}
