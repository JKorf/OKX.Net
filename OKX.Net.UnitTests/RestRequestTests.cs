using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Objects.Trade;

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
                opts.ApiCredentials = new OKXCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/Account", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetAccountBalanceAsync(), "GetAccountBalance", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetPositionsAsync(), "GetAccountPositions");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetPositionHistoryAsync(), "GetAccountPositionHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetPositionRiskAsync(), "GetAccountPositionRisk");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetBillHistoryAsync(), "GetBillHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetBillArchiveAsync(), "GetBillArchive");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetAccountConfigurationAsync(), "GetAccountConfiguration", useSingleArrayItem: true, ignoreProperties: ["traderInsts", "spotTraderInsts"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetPositionModeAsync(PositionMode.NetMode), "SetAccountPositionMode", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetLeverageAsync("123", MarginMode.Isolated), "GetAccountLeverage");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetLeverageAsync(1, MarginMode.Isolated, "ETH"), "SetAccountLeverage");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetMaximumAmountAsync("123", TradeMode.Cash), "GetMaximumAmount");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetMaximumAvailableAmountAsync("123", TradeMode.Cash), "GetMaximumAvailableAmount");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetMarginAmountAsync("123", PositionSide.Net, MarginAddReduce.Add, 1), "SetMarginAmount");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetMaximumLoanAmountAsync(MarginMode.Isolated, "123"), "GetMaximumLoanAmount");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetFeeRatesAsync(InstrumentType.Spot), "GetFeeRates", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetInterestAccruedAsync(), "GetInterestAccrued");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetInterestRateAsync(), "GetInterestRate");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetGreeksAsync(GreeksType.GreeksInCoins), "SetGreeks", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetMaximumWithdrawalsAsync(), "GetMaximumWithdrawals");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetFundingBalanceAsync(), "GetFundingBalance");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.TransferAsync("ETH", 1, TransferType.MasterAccountToSubAccount, AccountType.Funding, AccountType.Funding), "Transfer", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetFundingBillDetailsAsync("ETH"), "GetFundingBillDetails");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetFundingBillHistoryAsync("ETH"), "GetFundingBillHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetDepositAddressAsync("ETH"), "GetDepositAddress");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetDepositHistoryAsync("ETH"), "GetDepositHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.WithdrawAsync("ETH", 1, WithdrawalDestination.OKX, "123", 1), "Withdraw", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.CancelWithdrawalAsync("ETH"), "CancelWithdrawal", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetWithdrawalHistoryAsync("ETH"), "GetWithdrawalHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetSavingBalancesAsync("ETH"), "GetSavingBalances");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SavingPurchaseRedemptionAsync("ETH", 1, SavingActionSide.Redempt), "SavingPurchaseRedemption", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.EasyConvertDustAsync(["ETH"], "USD"), "ConvertDust");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetIsolatedMarginModeAsync(InstrumentType.Any, IsolatedMarginMode.Automatic), "SetIsolatedMarginMode", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetTransferAsync("123"), "GetTransfer", useSingleArrayItem: true, ignoreProperties: ["instId", "toInstId"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetAccountModeAsync(AccountLevel.Simple), "SetAccountMode", skipResponseValidation: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetAffiliateInviteeDetailsAsync("123"), "GetAffiliateInviteeDetails", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetAssetValuationAsync("123"), "GetAssetValuation", useSingleArrayItem: true, ignoreProperties: ["classic"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.ManualBorrowRepay("123", BorrowRepaySide.Repay, 0.1m), "ManualBorrowRepay", nestedJsonProperty: "data", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.SetAutoRepayAsync(true), "SetAutoRepay", nestedJsonProperty: "data", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetBorrowRepayHistoryAsync(), "GetBorrowRepayHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetEasyConvertDustAssetsAsync(), "GetEasyConvertDustAssets", nestedJsonProperty: "data", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetEasyConvertDustHistoryAsync(), "GetEasyConvertDustHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.PresetAccountModeSwitchAsync(AccountLevel.SingleCurrencyMargin), "PresetAccountModeSwitch", nestedJsonProperty: "data");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.PrecheckAccountModeSwitchAsync(AccountLevel.SingleCurrencyMargin), "PrecheckAccountModeSwitchAsync", nestedJsonProperty: "data");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetEstimatedLeverageInfoAsync(InstrumentType.Spot, MarginMode.Isolated, 10), "GetEstimatedLeverageInfo");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetAccountRiskStateAsync(), "GetAccountRiskState");
            await tester.ValidateAsync(c => c.UnifiedApi.Account.GetBorrowInterestLimitAsync(), "GetBorrowInterestLimit");
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/ExchangeData", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTickersAsync(InstrumentType.Spot), "GetTickers");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT"), "GetTicker", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetIndexTickersAsync(), "GetIndexTickers");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetOrderBookAsync("ETH-USDT"), "GetOrderBook", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetKlinesAsync("ETH-USDT", KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetKlineHistoryAsync("ETH-USDT", KlineInterval.OneDay), "GetKlineHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetIndexKlinesAsync("ETH-USDT", KlineInterval.OneDay), "GetIndexKlines");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", KlineInterval.OneDay), "GetMarkPriceKlines");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetRecentTradesAsync("ETH-USDT"), "GetRecentTrades");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT"), "GetTradeHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.Get24HourVolumeAsync(), "Get24HourVolume", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetIndexComponentsAsync("123"), "GetIndexComponents");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetBlockTickersAsync(InstrumentType.Swap), "GetBlockTickers");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetBlockTickerAsync("123"), "GetBlockTicker", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetBlockTradesAsync("123"), "GetBlockTrades");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetSymbolsAsync(InstrumentType.Spot), "GetSymbols");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetDeliveryExerciseHistoryAsync(InstrumentType.Futures), "GetDeliveryExerciseHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetOpenInterestsAsync(InstrumentType.Futures), "GetOpenInterests");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetFundingRatesAsync("ETH-USDT"), "GetFundingRates");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync("ETH-USDT"), "GetFundingRateHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetPriceLimitsAsync("ETH-USDT"), "GetLimitPrice", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetOptionMarketDataAsync("ETH-USDT"), "GetOptionMarketData");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetEstimatedPriceAsync("ETH-USDT"), "GetEstimatedPrice", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetDiscountInfoAsync(), "GetDiscountInfo");
            //await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", nestedJsonProperty: "data.0.time");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetMarkPricesAsync(InstrumentType.Futures), "GetMarkPrices");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetPositionTiersAsync(InstrumentType.Futures, MarginMode.Isolated, "ETH"), "GetPositionTiers");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetInterestRatesAsync(), "GetInterestRates", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetUnderlyingAsync(InstrumentType.Futures), "GetUnderlying");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetInsuranceFundAsync(InstrumentType.Futures), "GetInsuranceFund", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.UnitConvertAsync(ConvertType.ContractToCurrency, "123", 123), "UnitConvert", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsSupportedAssetsAsync(), "GetRubikSupportCoin");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsTakerVolumeAsync("ETH", InstrumentType.Contracts), "GetRubikTakerVolume");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsMarginLendingRatioAsync("ETH"), "GetRubikMarginLendingRatio");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsLongShortRatioAsync("ETH"), "GetRubikLongShortRatio");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsContractSummaryAsync("ETH"), "GetRubikContractSummary");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsOptionsSummaryAsync("ETH"), "GetRubikOptionsSummary");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsPutCallRatioAsync("ETH"), "GetRubikPutCallRatio");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeExpiryAsync("ETH"), "GetRubikInterestVolumeExpiry");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeStrikeAsync("ETH", "20210623"), "GetRubikInterestVolumeStrike");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetTradeStatsTakerFlowAsync("ETH"), "GetRubikTakerFlow", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetAnnouncementsAsync("123"), "GetAnnouncements", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetAnnouncementTypesAsync(), "GetAnnouncementTypes");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetEstimatedFuturesSettlementPriceAsync("XRP-USDT-250307"), "GetEstimatedFuturesSettlementPrice");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetSettlementHistoryAsync("XRP-USDT-250307"), "GetSettlementHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.ExchangeData.GetPremiumHistoryAsync("ETH-USDT-SWAP"), "GetPremiumHistory");
        }

        [Test]
        public async Task ValidateSubAccountCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/SubAccounts", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetSubAccountsAsync(), "GetSubAccounts");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.ResetSubAccountApiKeyAsync("123", "456"),  "ResetSubAccountApiKey", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetSubAccountTradingBalancesAsync("123"), "GetSubAccountTradingBalances", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetSubAccountFundingBalancesAsync("123"), "GetSubAccountFundingBalances");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetSubAccountBillsAsync("123"), "GetSubAccountBills");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.TransferBetweenSubAccountsAsync("ETH", 12m, AccountType.Funding, AccountType.Funding, "123", "456"), "TransferBetweenSubAccounts", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetSubAccountMaxWithdrawalsAsync("123"), "GetSubAccountMaxWithdrawals");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetManagedSubAccountBillsAsync(), "GetManagedSubAccountBills");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetEntrustSubAccountsAsync(), "GetEntrustSubAccounts");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.GetSubAccountApiKeysAsync("123"), "GetSubAccountApiKeys");
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.CreateSubAccountApiKeyAsync("123", "123", "123"), "CreateSubAccountApiKey", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.DeleteSubAccountApiKeyAsync("123", "123"), "DeleteSubAccountApiKey", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.SetSubAccountTransferOutAsync("123", true), "SetSubAccountTransferOut", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.SubAccounts.CreateSubAccountAsync("123"), "CreateSubAccount", useSingleArrayItem: true);
        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/Trading", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.PlaceOrderAsync("ETH-USDT", OrderSide.Buy, OrderType.Limit, 1), "PlaceOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.CheckOrderAsync("ETH-USDT", OrderSide.Buy, OrderType.Limit, 1), "CheckOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.PlaceMultipleOrdersAsync([new OKXOrderPlaceRequest()]), "PlaceMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.CancelOrderAsync("ETH-USDT", 123), "CancelOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.CancelMultipleOrdersAsync([new OKXOrderCancelRequest()]), "CancelMultipleOrders");
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.AmendOrderAsync("ETH-USDT", 123, newQuantity: 1), "AmendOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.AmendMultipleOrdersAsync([new OKXOrderAmendRequest()]), "AmendMultipleOrders");
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.ClosePositionAsync("ETH-USDT", MarginMode.Isolated), "ClosePosition", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetOrderDetailsAsync("ETH-USDT", 123), "GetOrderDetails", useSingleArrayItem: true, ignoreProperties: ["attachAlgoOrds", "linkedAlgoOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetOrdersAsync(), "GetOrders", ignoreProperties: ["attachAlgoOrds", "linkedAlgoOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetOrderHistoryAsync(InstrumentType.Contracts), "GetOrderHistory", ignoreProperties: ["attachAlgoOrds", "linkedAlgoOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetOrderArchiveAsync(InstrumentType.Contracts), "GetOrderArchive", ignoreProperties: ["attachAlgoOrds", "linkedAlgoOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetUserTradesAsync(InstrumentType.Contracts), "GetUserTrades", ignoreProperties: ["feeRate"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetUserTradesArchiveAsync(InstrumentType.Contracts), "GetUserTradesArchive", ignoreProperties: ["feeRate"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.PlaceAlgoOrderAsync("ETH-USDT", TradeMode.Isolated, OrderSide.Buy, AlgoOrderType.Conditional), "PlaceAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.CancelAlgoOrderAsync([new OKXAlgoOrderRequest()]), "CancelAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetAlgoOrderListAsync(AlgoOrderType.OCO), "GetAlgoOrderList", ignoreProperties: ["amendPxOnTriggerType", "attachAlgoOrds", "linkedOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetAlgoOrderHistoryAsync(AlgoOrderType.OCO), "GetAlgoOrderHistory", ignoreProperties: ["amendPxOnTriggerType", "attachAlgoOrds", "linkedOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetAlgoOrderAsync("123"), "GetAlgoOrder", useSingleArrayItem: true, ignoreProperties: ["amendPxOnTriggerType", "attachAlgoOrds", "linkedOrd"]);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.AmendAlgoOrderAsync("123", "123"), "AmendAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.CancelAllAfterAsync(TimeSpan.Zero), "CancelAllAfter", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetAccountRateLimitAsync(), "GetAccountRateLimit");
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetOneClickRepayCurrencyListAsync(), "GetOneClickRepayCurrencyList");
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.OneClickRepayAsync("BTC", "USDT"), "OneClickRepay");
            await tester.ValidateAsync(c => c.UnifiedApi.Trading.GetOneClickRepayHistoryAsync(), "GetOneClickRepayHistory");
        }

        [Test]
        public async Task ValidateCopyTradingCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new OKXCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/CopyTrading", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(c => c.UnifiedApi.CopyTrading.GetLeadPositionsAsync(), "GetLeadPositions");
            await tester.ValidateAsync(c => c.UnifiedApi.CopyTrading.GetLeadPositionHistoryAsync(), "GetLeadPositionHistory");
            await tester.ValidateAsync(c => c.UnifiedApi.CopyTrading.PlaceLeadStopOrderAsync("123", takeProfitTriggerPrice: 100), "PlaceLeadStopOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.CopyTrading.CloseLeadPositionAsync("123"), "CloseLeadPosition", useSingleArrayItem: true);
            await tester.ValidateAsync(c => c.UnifiedApi.CopyTrading.GetLeadingInstrumentsAsync(), "GetLeadingInstruments");
            await tester.ValidateAsync(c => c.UnifiedApi.CopyTrading.AmendLeadingInstrumentsAsync(["BTC-USDT"]), "AmendLeadingInstruments");
        }

        private static bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(h => h.Key == "OK-ACCESS-SIGN");
        }
    }
}
