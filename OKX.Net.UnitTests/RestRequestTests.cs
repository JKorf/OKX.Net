﻿using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateAccountCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/Account", "https://www.okx.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountBalanceAsync(), "GetAccountBalance", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountPositionsAsync(), "GetAccountPositions");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountPositionHistoryAsync(), "GetAccountPositionHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountPositionRiskAsync(), "GetAccountPositionRisk");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetBillHistoryAsync(), "GetBillHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetBillArchiveAsync(), "GetBillArchive");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountConfigurationAsync(), "GetAccountConfiguration", useSingleArrayItem: true, ignoreProperties: new List<string> { "traderInsts", "spotTraderInsts" });
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetAccountPositionModeAsync(Enums.OKXPositionMode.NetMode), "SetAccountPositionMode", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountLeverageAsync("123", Enums.OKXMarginMode.Isolated), "GetAccountLeverage");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetAccountLeverageAsync(1, Enums.OKXMarginMode.Isolated, "ETH"), "SetAccountLeverage");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumAmountAsync("123", Enums.OKXTradeMode.Cash), "GetMaximumAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumAvailableAmountAsync("123", Enums.OKXTradeMode.Cash), "GetMaximumAvailableAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetMarginAmountAsync("123", Enums.OKXPositionSide.Net, Enums.OKXMarginAddReduce.Add, 1), "SetMarginAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumLoanAmountAsync("123", Enums.OKXMarginMode.Isolated), "GetMaximumLoanAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetFeeRatesAsync(Enums.OKXInstrumentType.Spot), "GetFeeRates", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetInterestAccruedAsync(), "GetInterestAccrued");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetInterestRateAsync(), "GetInterestRate");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetGreeksAsync(Enums.OKXGreeksType.GreeksInCoins), "SetGreeks");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumWithdrawalsAsync(), "GetMaximumWithdrawals");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetFundingBalanceAsync(), "GetFundingBalance");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.TransferAsync("ETH", 1, Enums.OKXTransferType.MasterAccountToSubAccount, Enums.OKXAccount.Funding, Enums.OKXAccount.Funding), "Transfer", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetFundingBillDetailsAsync("ETH"), "GetFundingBillDetails");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetLightningDepositsAsync("ETH", 1), "GetLightningDeposits");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetDepositAddressAsync("ETH"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetDepositHistoryAsync("ETH"), "GetDepositHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.WithdrawAsync("ETH", 1, Enums.OKXWithdrawalDestination.OKX, "123", 1), "Withdraw", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetLightningWithdrawalsAsync("ETH", "123"), "GetLightningWithdrawals");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.CancelWithdrawalAsync("ETH"), "CancelWithdrawal", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetWithdrawalHistoryAsync("ETH"), "GetWithdrawalHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetSavingBalancesAsync("ETH"), "GetSavingBalances");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SavingPurchaseRedemptionAsync("ETH", 1, Enums.OKXSavingActionSide.Redempt), "SavingPurchaseRedemption", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.ConvertDustAsync(new[] { "ETH" }), "ConvertDust", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetIsolatedMarginModeAsync(Enums.OKXInstrumentType.Any, Enums.OKXIsolatedMarginMode.Automatic), "SetIsolatedMarginMode", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetTransferAsync("123"), "GetTransfer", useSingleArrayItem: true, ignoreProperties: new List<string> { "instId", "toInstId" });
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetAccountModeAsync(Enums.OKXAccountLevel.Simple), "SetAccountMode", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAffiliateInviteeDetailsAsync("123"), "GetAffiliateInviteeDetails", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAssetValuationAsync("123"), "GetAssetValuation", useSingleArrayItem: true, ignoreProperties: new List<string> { "classic" });
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/ExchangeData", "https://www.okx.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.OKXInstrumentType.Spot), "GetTickers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT"), "GetTicker", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetIndexTickersAsync(), "GetIndexTickers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOrderBookAsync("ETH-USDT"), "GetOrderBook", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.OKXPeriod.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetKlineHistoryAsync("ETH-USDT", Enums.OKXPeriod.OneDay), "GetKlineHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetIndexKlinesAsync("ETH-USDT", Enums.OKXPeriod.OneDay), "GetIndexKlines");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.OKXPeriod.OneDay), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRecentTradesAsync("ETH-USDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.Get24HourVolumeAsync(), "Get24HourVolume");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOracleAsync(), "GetOracle");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetIndexComponentsAsync("123"), "GetIndexComponents");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetBlockTickersAsync(Enums.OKXInstrumentType.Swap), "GetBlockTickers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetBlockTickerAsync("123"), "GetBlockTicker", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetBlockTradesAsync("123"), "GetBlockTrades");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetSymbolsAsync(Enums.OKXInstrumentType.Spot), "GetSymbols");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetDeliveryExerciseHistoryAsync(Enums.OKXInstrumentType.Futures), "GetDeliveryExerciseHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOpenInterestsAsync(Enums.OKXInstrumentType.Futures), "GetOpenInterests");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetFundingRatesAsync("ETH-USDT"), "GetFundingRates");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync("ETH-USDT"), "GetFundingRateHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetLimitPriceAsync("ETH-USDT"), "GetLimitPrice");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOptionMarketDataAsync("ETH-USDT"), "GetOptionMarketData");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetEstimatedPriceAsync("ETH-USDT"), "GetEstimatedPrice");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetDiscountInfoAsync(), "GetDiscountInfo");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetServerTimeAsync(), "GetServerTime");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetMarkPricesAsync(Enums.OKXInstrumentType.Futures), "GetMarkPrices");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetPositionTiersAsync(Enums.OKXInstrumentType.Futures, Enums.OKXMarginMode.Isolated, "ETH"), "GetPositionTiers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetInterestRatesAsync(), "GetInterestRates");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetVIPInterestRatesAsync(), "GetVIPInterestRates");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetUnderlyingAsync(Enums.OKXInstrumentType.Futures), "GetUnderlying");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetInsuranceFundAsync(Enums.OKXInstrumentType.Futures), "GetInsuranceFund", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.UnitConvertAsync(Enums.OKXConvertType.ContractToCurrency, "123", 123), "UnitConvert", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikSupportCoinAsync(), "GetRubikSupportCoin");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikTakerVolumeAsync("ETH", Enums.OKXInstrumentType.Contracts), "GetRubikTakerVolume");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikMarginLendingRatioAsync("ETH"), "GetRubikMarginLendingRatio");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikLongShortRatioAsync("ETH"), "GetRubikLongShortRatio");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikContractSummaryAsync("ETH"), "GetRubikContractSummary");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikOptionsSummaryAsync("ETH"), "GetRubikOptionsSummary");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikPutCallRatioAsync("ETH"), "GetRubikPutCallRatio");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikInterestVolumeExpiryAsync("ETH"), "GetRubikInterestVolumeExpiry");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikInterestVolumeStrikeAsync("ETH", "20210623"), "GetRubikInterestVolumeStrike");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRubikTakerFlowAsync("ETH"), "GetRubikTakerFlow");
        }

        [Test]
        public async Task ValidateSubAccountCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/SubAccounts", "https://www.okx.com", IsAuthenticated, "data", stjCompare: false);

        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/Trading", "https://www.okx.com", IsAuthenticated, "data", stjCompare: false);

        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(h => h.Key == "OK-ACCESS-SIGN");
        }
    }
}
