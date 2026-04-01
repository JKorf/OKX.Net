using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Funding;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Options;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.System;
using OKX.Net.Objects.Trade;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateConcurrentFuturesSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                OutputOriginalData = true,

            }), logger);

            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/ExchangeData", "wss://ws.okx.com:8443", "data");
            await tester.ValidateConcurrentAsync<OKXKline>(
                (c, handler) => c.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync("ETH-USDT", KlineInterval.OneDay, handler),
                (c, handler) => c.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync("ETH-USDT", KlineInterval.OneHour, handler),
                "Concurrent");
        }

        [Test]
        public async Task ValidateExchangeDataSubscriptions()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());

            var client = new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = new OKXCredentials("123", "456", "789")
            }), loggerFactory);
            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/ExchangeData", "wss://ws.okx.com:8443", "data");
            await tester.ValidateAsync<OKXInstrument[]>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToSymbolUpdatesAsync(InstrumentType.Spot, handler), "Symbol");
            await tester.ValidateAsync<OKXTicker>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETH-USDT", handler), "Ticker", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOpenInterest>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToOpenInterestUpdatesAsync("ETH-USDT", handler), "Interest", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXKline>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync("ETH-USDT", KlineInterval.OneDay, handler), "Klines", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXTrade>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync("ETH-USDT", handler), "Trades", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXEstimatedPrice>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToEstimatedPriceUpdatesAsync(InstrumentType.Futures, "BTC-USD", null, handler), "EstimatedPrice", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXMarkPrice>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToMarkPriceUpdatesAsync("ETH-USDT", handler), "MarkPrice", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXMiniKline[]>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToMarkPriceKlineUpdatesAsync("ETH-USDT", KlineInterval.OneDay, handler), "MarkPriceKlines");
            await tester.ValidateAsync<OKXLimitPrice>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToPriceLimitUpdatesAsync("ETH-USDT", handler), "PriceLimit", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOrderBook>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToOrderBookUpdatesAsync("ETH-USDT", OrderBookType.OrderBook, handler), "OrderBook", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOptionSummary[]>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToOptionSummaryUpdatesAsync("ETH-USDT", handler), "OptionSummary");
            await tester.ValidateAsync<OKXFundingRate>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToFundingRateUpdatesAsync("ETH-USDT", handler), "FundingRate", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXMiniKline[]>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToIndexKlineUpdatesAsync("ETH-USDT", KlineInterval.OneDay, handler), "IndexKlines");
            await tester.ValidateAsync<OKXIndexTicker>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToIndexTickerUpdatesAsync("ETH-USDT", handler), "IndexTicker", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXStatus>((c, handler) => c.UnifiedApi.ExchangeData.SubscribeToSystemStatusUpdatesAsync(handler), "SystemStatus", useFirstUpdateItem: true);
        }

        [Test]
        public async Task ValidateAccountSubscriptions()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());

            var client = new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                ApiCredentials = new OKXCredentials("123", "456", "789")
            }), loggerFactory);
            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/Account", "wss://ws.okx.com:8443", "data");
            //await tester.ValidateAsync<OKXAccountBalance>((c, handler) => c.UnifiedApi.Account.SubscribeToAccountUpdatesAsync(null, true, handler), "Balance", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXPositionAndBalanceUpdate>((c, handler) => c.UnifiedApi.Account.SubscribeToBalanceAndPositionUpdatesAsync(handler), "BalanceAndPosition", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXDepositUpdate>((c, handler) => c.UnifiedApi.Account.SubscribeToDepositUpdatesAsync(handler), "Deposit", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXWithdrawalUpdate>((c, handler) => c.UnifiedApi.Account.SubscribeToWithdrawalUpdatesAsync(handler), "Withdrawal", useFirstUpdateItem: true, ignoreProperties: ["addrEx"]);
        }

        [Test]
        public async Task ValidateTradingSubscriptions()
        {
            var client = new OKXSocketClient(opts =>
            {
                opts.ApiCredentials = new OKXCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<OKXSocketClient>(client, "Subscriptions/Unified/Trading", "wss://ws.okx.com:8443", "data");
            await tester.ValidateAsync<OKXPosition[]>((c, handler) => c.UnifiedApi.Trading.SubscribeToPositionUpdatesAsync(InstrumentType.Futures, null, null, true, handler), "Position");
            await tester.ValidateAsync<OKXPosition>((c, handler) => c.UnifiedApi.Trading.SubscribeToLiquidationWarningUpdatesAsync(InstrumentType.Futures, null, handler), "LiquidationWarning", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXOrderUpdate>((c, handler) => c.UnifiedApi.Trading.SubscribeToOrderUpdatesAsync(InstrumentType.Futures, null, null, handler), "Order", useFirstUpdateItem: true, ignoreProperties: ["msg", "code", "attachAlgoOrds"]);
            await tester.ValidateAsync<OKXUserTradeUpdate>((c, handler) => c.UnifiedApi.Trading.SubscribeToUserTradeUpdatesAsync(null, handler), "UserTrade", useFirstUpdateItem: true);
            await tester.ValidateAsync<OKXAlgoOrderUpdate>((c, handler) => c.UnifiedApi.Trading.SubscribeToAlgoOrderUpdatesAsync(InstrumentType.Futures, null, null, handler), "AlgoOrder", useFirstUpdateItem: true, ignoreProperties: ["attachAlgoOrds", "linkedOrd"]);
            await tester.ValidateAsync<OKXAlgoOrderUpdate>((c, handler) => c.UnifiedApi.Trading.SubscribeToAdvanceAlgoOrderUpdatesAsync(InstrumentType.Futures, null, null, handler), "AdvancedAlgoOrder", useFirstUpdateItem: true, ignoreProperties: ["attachAlgoOrds", "linkedOrd", "reduceOnly"]);
        }
    }
}
