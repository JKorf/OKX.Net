
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using OKX.Net.Objects;

namespace OKX.Net.UnitTests
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
        public async Task ValidateAccountCalls()
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
                   "ConvertDustAsync",
                   "WithdrawAsync",
                   "GetTransferAsync",
                   "SetIsolatedMarginModeAsync",
                   "GetAffiliateInviteeDetailsAsync"
                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetAccountConfigurationAsync", new List<string>{ "traderInsts" } }
                }
                );
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
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
        public async Task ValidateTradingCalls()
        {
            await _comparer.ProcessSubject("Unified/Trading", c => c.UnifiedApi.Trading,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity", "clientAlgoId" },
               ignoreProperties: new Dictionary<string, List<string>>
               {
               },
               takeFirstItemForCompare: new List<string>
               {
                   "AmendOrderAsync",
                   "CancelAdvanceAlgoOrderAsync",
                   "CancelAlgoOrderAsync",
                   "ClosePositionAsync",
                   "CancelOrderAsync",
                   "GetOrderDetailsAsync",
                   "PlaceAlgoOrderAsync",
                   "PlaceOrderAsync",
                   "GetAlgoOrderAsync"
               }
                );
        }

        [Test]
        public async Task ValidateSubAccountCalls()
        {
            await _comparer.ProcessSubject("Unified/SubAccounts", c => c.UnifiedApi.SubAccounts,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>>
               {
               },
               takeFirstItemForCompare: new List<string>
               {
                   "GetSubAccountTradingBalancesAsync",
                   "ResetSubAccountApiKeyAsync",
                   "TransferBetweenSubAccountsAsync",
               }
                );
        }
    }
}
