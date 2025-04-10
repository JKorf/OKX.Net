using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Objects;
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
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/Account", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountBalanceAsync(), "GetAccountBalance", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetPositionsAsync(), "GetAccountPositions");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetPositionHistoryAsync(), "GetAccountPositionHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetPositionRiskAsync(), "GetAccountPositionRisk");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetBillHistoryAsync(), "GetBillHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetBillArchiveAsync(), "GetBillArchive");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAccountConfigurationAsync(), "GetAccountConfiguration", useSingleArrayItem: true, ignoreProperties: new List<string> { "traderInsts", "spotTraderInsts" });
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetPositionModeAsync(Enums.PositionMode.NetMode), "SetAccountPositionMode", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetLeverageAsync("123", Enums.MarginMode.Isolated), "GetAccountLeverage");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetLeverageAsync(1, Enums.MarginMode.Isolated, "ETH"), "SetAccountLeverage");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumAmountAsync("123", Enums.TradeMode.Cash), "GetMaximumAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumAvailableAmountAsync("123", Enums.TradeMode.Cash), "GetMaximumAvailableAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetMarginAmountAsync("123", Enums.PositionSide.Net, Enums.MarginAddReduce.Add, 1), "SetMarginAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumLoanAmountAsync(Enums.MarginMode.Isolated, "123"), "GetMaximumLoanAmount");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetFeeRatesAsync(Enums.InstrumentType.Spot), "GetFeeRates", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetInterestAccruedAsync(), "GetInterestAccrued");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetInterestRateAsync(), "GetInterestRate");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetGreeksAsync(Enums.GreeksType.GreeksInCoins), "SetGreeks", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetMaximumWithdrawalsAsync(), "GetMaximumWithdrawals");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetFundingBalanceAsync(), "GetFundingBalance");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.TransferAsync("ETH", 1, Enums.TransferType.MasterAccountToSubAccount, Enums.AccountType.Funding, Enums.AccountType.Funding), "Transfer", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetFundingBillDetailsAsync("ETH"), "GetFundingBillDetails");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetLightningDepositsAsync("ETH", 1), "GetLightningDeposits");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetDepositAddressAsync("ETH"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetDepositHistoryAsync("ETH"), "GetDepositHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.WithdrawAsync("ETH", 1, Enums.WithdrawalDestination.OKX, "123", 1), "Withdraw", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetLightningWithdrawalAsync("ETH", "123"), "GetLightningWithdrawals", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.CancelWithdrawalAsync("ETH"), "CancelWithdrawal", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetWithdrawalHistoryAsync("ETH"), "GetWithdrawalHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetSavingBalancesAsync("ETH"), "GetSavingBalances");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SavingPurchaseRedemptionAsync("ETH", 1, Enums.SavingActionSide.Redempt), "SavingPurchaseRedemption", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.EasyConvertDustAsync(new[] { "ETH" }, "USD"), "ConvertDust");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetIsolatedMarginModeAsync(Enums.InstrumentType.Any, Enums.IsolatedMarginMode.Automatic), "SetIsolatedMarginMode", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetTransferAsync("123"), "GetTransfer", useSingleArrayItem: true, ignoreProperties: new List<string> { "instId", "toInstId" });
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetAccountModeAsync(Enums.AccountLevel.Simple), "SetAccountMode", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAffiliateInviteeDetailsAsync("123"), "GetAffiliateInviteeDetails", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetAssetValuationAsync("123"), "GetAssetValuation", useSingleArrayItem: true, ignoreProperties: new List<string> { "classic" });
            await tester.ValidateAsync(client => client.UnifiedApi.Account.ManualBorrowRepay("123", BorrowRepaySide.Repay, 0.1m), "ManualBorrowRepay", nestedJsonProperty: "data", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.SetAutoRepayAsync(true), "SetAutoRepay", nestedJsonProperty: "data", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetBorrowRepayHistoryAsync(), "GetBorrowRepayHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetEasyConvertDustAssetsAsync(), "GetEasyConvertDustAssets", nestedJsonProperty: "data", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Account.GetEasyConvertDustHistoryAsync(), "GetEasyConvertDustHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.PresetAccountModeSwitchAsync(AccountLevel.SingleCurrencyMargin), "PresetAccountModeSwitch", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UnifiedApi.Account.PrecheckAccountModeSwitchAsync(AccountLevel.SingleCurrencyMargin), "PrecheckAccountModeSwitchAsync", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/ExchangeData", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Spot), "GetTickers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT"), "GetTicker", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetIndexTickersAsync(), "GetIndexTickers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOrderBookAsync("ETH-USDT"), "GetOrderBook", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetKlineHistoryAsync("ETH-USDT", Enums.KlineInterval.OneDay), "GetKlineHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetIndexKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay), "GetIndexKlines");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetRecentTradesAsync("ETH-USDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.Get24HourVolumeAsync(), "Get24HourVolume", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetIndexComponentsAsync("123"), "GetIndexComponents");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetBlockTickersAsync(Enums.InstrumentType.Swap), "GetBlockTickers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetBlockTickerAsync("123"), "GetBlockTicker", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetBlockTradesAsync("123"), "GetBlockTrades");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetSymbolsAsync(Enums.InstrumentType.Spot), "GetSymbols");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetDeliveryExerciseHistoryAsync(Enums.InstrumentType.Futures), "GetDeliveryExerciseHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOpenInterestsAsync(Enums.InstrumentType.Futures), "GetOpenInterests");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetFundingRatesAsync("ETH-USDT"), "GetFundingRates");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync("ETH-USDT"), "GetFundingRateHistory");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetPriceLimitsAsync("ETH-USDT"), "GetLimitPrice", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetOptionMarketDataAsync("ETH-USDT"), "GetOptionMarketData");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetEstimatedPriceAsync("ETH-USDT"), "GetEstimatedPrice", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetDiscountInfoAsync(), "GetDiscountInfo");
            //await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", nestedJsonProperty: "data.0.time");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetMarkPricesAsync(Enums.InstrumentType.Futures), "GetMarkPrices");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetPositionTiersAsync(Enums.InstrumentType.Futures, Enums.MarginMode.Isolated, "ETH"), "GetPositionTiers");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetInterestRatesAsync(), "GetInterestRates", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetVIPInterestRatesAsync(), "GetVIPInterestRates");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetUnderlyingAsync(Enums.InstrumentType.Futures), "GetUnderlying");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetInsuranceFundAsync(Enums.InstrumentType.Futures), "GetInsuranceFund", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.UnitConvertAsync(Enums.ConvertType.ContractToCurrency, "123", 123), "UnitConvert", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsSupportedAssetsAsync(), "GetRubikSupportCoin");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsTakerVolumeAsync("ETH", Enums.InstrumentType.Contracts), "GetRubikTakerVolume");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsMarginLendingRatioAsync("ETH"), "GetRubikMarginLendingRatio");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsLongShortRatioAsync("ETH"), "GetRubikLongShortRatio");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsContractSummaryAsync("ETH"), "GetRubikContractSummary");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsOptionsSummaryAsync("ETH"), "GetRubikOptionsSummary");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsPutCallRatioAsync("ETH"), "GetRubikPutCallRatio");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeExpiryAsync("ETH"), "GetRubikInterestVolumeExpiry");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeStrikeAsync("ETH", "20210623"), "GetRubikInterestVolumeStrike");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetTradeStatsTakerFlowAsync("ETH"), "GetRubikTakerFlow", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetAnnouncementsAsync("123"), "GetAnnouncements", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetAnnouncementTypesAsync(), "GetAnnouncementTypes");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetEstimatedFuturesSettlementPriceAsync("XRP-USDT-250307"), "GetEstimatedFuturesSettlementPrice");
            await tester.ValidateAsync(client => client.UnifiedApi.ExchangeData.GetSettlementHistoryAsync("XRP-USDT-250307"), "GetSettlementHistory");
        }

        [Test]
        public async Task ValidateSubAccountCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/SubAccounts", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(client => client.UnifiedApi.SubAccounts.GetSubAccountsAsync(), "GetSubAccounts");
            await tester.ValidateAsync(client => client.UnifiedApi.SubAccounts.ResetSubAccountApiKeyAsync("123", "456"),  "ResetSubAccountApiKey", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.SubAccounts.GetSubAccountTradingBalancesAsync("123"), "GetSubAccountTradingBalances", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.SubAccounts.GetSubAccountFundingBalancesAsync("123"), "GetSubAccountFundingBalances");
            await tester.ValidateAsync(client => client.UnifiedApi.SubAccounts.GetSubAccountBillsAsync("123"), "GetSubAccountBills");
            await tester.ValidateAsync(client => client.UnifiedApi.SubAccounts.TransferBetweenSubAccountsAsync("ETH", 12m, Enums.AccountType.Funding, Enums.AccountType.Funding, "123", "456"), "TransferBetweenSubAccounts", useSingleArrayItem: true);
        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            var client = new OKXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<OKXRestClient>(client, "Endpoints/UnifiedApi/Trading", "https://www.okx.com", IsAuthenticated, "data");
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.PlaceOrderAsync("ETH-USDT", Enums.OrderSide.Buy, Enums.OrderType.Limit, 1), "PlaceOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.CheckOrderAsync("ETH-USDT", Enums.OrderSide.Buy, Enums.OrderType.Limit, 1), "CheckOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.PlaceMultipleOrdersAsync(new[] { new OKXOrderPlaceRequest() }), "PlaceMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.CancelOrderAsync("ETH-USDT", 123), "CancelOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.CancelMultipleOrdersAsync(new[] { new OKXOrderCancelRequest() } ), "CancelMultipleOrders");
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.AmendOrderAsync("ETH-USDT", 123, newQuantity: 1), "AmendOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.AmendMultipleOrdersAsync(new[] { new OKXOrderAmendRequest() }), "AmendMultipleOrders");
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.ClosePositionAsync("ETH-USDT", Enums.MarginMode.Isolated), "ClosePosition", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetOrderDetailsAsync("ETH-USDT", 123), "GetOrderDetails", useSingleArrayItem: true, ignoreProperties: new List<string> { "attachAlgoOrds", "linkedAlgoOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetOrdersAsync(), "GetOrders", ignoreProperties: new List<string> { "attachAlgoOrds", "linkedAlgoOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetOrderHistoryAsync(Enums.InstrumentType.Contracts), "GetOrderHistory", ignoreProperties: new List<string> { "attachAlgoOrds", "linkedAlgoOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetOrderArchiveAsync(Enums.InstrumentType.Contracts), "GetOrderArchive", ignoreProperties: new List<string> { "attachAlgoOrds", "linkedAlgoOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetUserTradesAsync(Enums.InstrumentType.Contracts), "GetUserTrades", ignoreProperties: new List<string> { "feeRate" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetUserTradesArchiveAsync(Enums.InstrumentType.Contracts), "GetUserTradesArchive", ignoreProperties: new List<string> { "feeRate" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.PlaceAlgoOrderAsync("ETH-USDT", Enums.TradeMode.Isolated, Enums.OrderSide.Buy, Enums.AlgoOrderType.Conditional), "PlaceAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.CancelAlgoOrderAsync(new[] { new OKXAlgoOrderRequest() } ), "CancelAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.CancelAdvanceAlgoOrderAsync(new[] { new OKXAlgoOrderRequest() } ), "CancelAdvanceAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetAlgoOrderListAsync(Enums.AlgoOrderType.OCO), "GetAlgoOrderList", ignoreProperties: new List<string> { "amendPxOnTriggerType", "attachAlgoOrds", "linkedOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetAlgoOrderHistoryAsync(Enums.AlgoOrderType.OCO), "GetAlgoOrderHistory", ignoreProperties: new List<string> { "amendPxOnTriggerType", "attachAlgoOrds", "linkedOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.GetAlgoOrderAsync("123"), "GetAlgoOrder", useSingleArrayItem: true, ignoreProperties: new List<string> { "amendPxOnTriggerType", "attachAlgoOrds", "linkedOrd" });
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.AmendAlgoOrderAsync("123", "123"), "AmendAlgoOrder", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.UnifiedApi.Trading.CancelAllAfterAsync(TimeSpan.Zero), "CancelAllAfter", useSingleArrayItem: true);

        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(h => h.Key == "OK-ACCESS-SIGN");
        }
    }
}
