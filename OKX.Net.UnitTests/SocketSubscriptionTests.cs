using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Objects;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Funding;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.System;
using OKX.Net.Objects.Trade;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateExchangeDataSubscriptions()
        {
            var client = new OKXSocketClient(opts =>
            {
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/ExchangeData", "wss://ws.okx.com:8443", "data");
            await tester.ValidateAsync<OKXInstrument[]>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToSymbolUpdatesAsync(Enums.InstrumentType.Spot, handler), "Symbol");
            await tester.ValidateAsync<OKXTicker>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETH-USDT", handler), "Ticker", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOpenInterest>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToOpenInterestUpdatesAsync("ETH-USDT", handler), "Interest", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXKline>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "Klines", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXTrade>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync("ETH-USDT", handler), "Trades", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXEstimatedPrice>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToEstimatedPriceUpdatesAsync(Enums.InstrumentType.Futures, "BTC-USD", null, handler), "EstimatedPrice", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXMarkPrice>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToMarkPriceUpdatesAsync("ETH-USDT", handler), "MarkPrice", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXMiniKline[]>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToMarkPriceKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "MarkPriceKlines");
            await tester.ValidateAsync<OKXLimitPrice>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToPriceLimitUpdatesAsync("ETH-USDT", handler), "PriceLimit", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOrderBook>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToOrderBookUpdatesAsync("ETH-USDT", Enums.OrderBookType.OrderBook, handler), "OrderBook", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOptionSummary[]>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToOptionSummaryUpdatesAsync("ETH-USDT", handler), "OptionSummary");
            await tester.ValidateAsync<OKXFundingRate>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToFundingRateUpdatesAsync("ETH-USDT", handler), "FundingRate", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXMiniKline[]>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToIndexKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "IndexKlines");
            await tester.ValidateAsync<OKXIndexTicker>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToIndexTickerUpdatesAsync("ETH-USDT", handler), "IndexTicker", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXStatus>((client, handler) => client.UnifiedApi.ExchangeData.SubscribeToSystemStatusUpdatesAsync(handler), "SystemStatus", useFirstUpdateItem: true);
        }

        [Test]
        public async Task ValidateAccountSubscriptions()
        {
            var client = new OKXSocketClient(opts =>
            {
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/Account", "wss://ws.okx.com:8443", "data");
            //await tester.ValidateAsync<OKXAccountBalance>((client, handler) => client.UnifiedApi.Account.SubscribeToAccountUpdatesAsync(null, true, handler), "Balance", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXPositionAndBalanceUpdate>((client, handler) => client.UnifiedApi.Account.SubscribeToBalanceAndPositionUpdatesAsync(handler), "BalanceAndPosition", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXDepositUpdate>((client, handler) => client.UnifiedApi.Account.SubscribeToDepositUpdatesAsync(handler), "Deposit", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXWithdrawalUpdate>((client, handler) => client.UnifiedApi.Account.SubscribeToWithdrawalUpdatesAsync(handler), "Withdrawal", useFirstUpdateItem: true, ignoreProperties: new List<string> { "addrEx" });
        }

        [Test]
        public async Task ValidateTradingSubscriptions()
        {
            var client = new OKXSocketClient(opts =>
            {
                opts.ApiCredentials = new ApiCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/Trading", "wss://ws.okx.com:8443", "data");
            await tester.ValidateAsync<OKXPosition[]>((client, handler) => client.UnifiedApi.Trading.SubscribeToPositionUpdatesAsync(Enums.InstrumentType.Futures, null, null, true, handler), "Position");
            await tester.ValidateAsync<OKXPosition>((client, handler) => client.UnifiedApi.Trading.SubscribeToLiquidationWarningUpdatesAsync(Enums.InstrumentType.Futures, null, handler), "LiquidationWarning", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOrderUpdate>((client, handler) => client.UnifiedApi.Trading.SubscribeToOrderUpdatesAsync(Enums.InstrumentType.Futures, null, null, handler), "Order", useFirstUpdateItem: true, ignoreProperties: new List<string> { "msg", "code", "attachAlgoOrds" });
            await tester.ValidateAsync<OKXUserTradeUpdate>((client, handler) => client.UnifiedApi.Trading.SubscribeToUserTradeUpdatesAsync(null, handler), "UserTrade", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXAlgoOrderUpdate>((client, handler) => client.UnifiedApi.Trading.SubscribeToAlgoOrderUpdatesAsync(Enums.InstrumentType.Futures, null, null, handler), "AlgoOrder", useFirstUpdateItem: true, ignoreProperties: new List<string> { "attachAlgoOrds", "linkedOrd" });
            await tester.ValidateAsync<OKXAlgoOrderUpdate>((client, handler) => client.UnifiedApi.Trading.SubscribeToAdvanceAlgoOrderUpdatesAsync(Enums.InstrumentType.Futures, null, null, handler), "AdvancedAlgoOrder", useFirstUpdateItem: true, ignoreProperties: new List<string> { "attachAlgoOrds", "linkedOrd", "reduceOnly" });
        }
    }
}
