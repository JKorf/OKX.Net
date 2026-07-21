# OKX.Net AI API Quick Map

Use this file to route common user intents to the correct OKX.Net client member. If a method name or parameter is not listed here, inspect `OKX.Net/Interfaces/Clients/UnifiedApi/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new OKXRestClient()` |
| WebSocket streams and socket API requests | `new OKXSocketClient()` |
| API key authentication | `options.ApiCredentials = new OKXCredentials("key", "secret", "passphrase")` |
| Live environment | `OKXEnvironment.Live` |
| Demo / test environment | `OKXEnvironment.Demo` |
| Europe environment | `OKXEnvironment.Europe` |
| Dependency injection | `services.AddOKX(options => { ... })` |

## Unified REST Roots

| User intent | OKX.Net member |
|---|---|
| Public market data | `client.UnifiedApi.ExchangeData` |
| Account, balances, funding, withdrawals, leverage | `client.UnifiedApi.Account` |
| Orders, fills, algo orders, order history | `client.UnifiedApi.Trading` |
| Sub-account management | `client.UnifiedApi.SubAccounts` |
| Copy trading | `client.UnifiedApi.CopyTrading` |
| Shared REST abstraction | `client.UnifiedApi.SharedClient` |

## Market Data REST

| User intent | OKX.Net member |
|---|---|
| Get server time | `client.UnifiedApi.ExchangeData.GetServerTimeAsync()` |
| Get one ticker | `client.UnifiedApi.ExchangeData.GetTickerAsync("BTC-USDT")` |
| Get tickers by product type | `client.UnifiedApi.ExchangeData.GetTickersAsync(InstrumentType.Spot)` |
| Get order book | `client.UnifiedApi.ExchangeData.GetOrderBookAsync("BTC-USDT", depth: 20)` |
| Get recent trades | `client.UnifiedApi.ExchangeData.GetRecentTradesAsync("BTC-USDT")` |
| Get trade history | `client.UnifiedApi.ExchangeData.GetTradeHistoryAsync("BTC-USDT")` |
| Get klines/candles | `client.UnifiedApi.ExchangeData.GetKlinesAsync("BTC-USDT", KlineInterval.OneMinute)` |
| Get historical klines | `client.UnifiedApi.ExchangeData.GetKlineHistoryAsync("BTC-USDT", KlineInterval.OneMinute)` |
| Get instruments/symbols | `client.UnifiedApi.ExchangeData.GetSymbolsAsync(InstrumentType.Spot)` |
| Get underlying values | `client.UnifiedApi.ExchangeData.GetUnderlyingAsync(InstrumentType.Futures)` |
| Get mark prices | `client.UnifiedApi.ExchangeData.GetMarkPricesAsync(InstrumentType.Swap, symbol: "BTC-USDT-SWAP")` |
| Get mark price klines | `client.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync("BTC-USDT-SWAP", KlineInterval.OneMinute)` |
| Get index tickers | `client.UnifiedApi.ExchangeData.GetIndexTickersAsync(symbol: "BTC-USD")` |
| Get index klines | `client.UnifiedApi.ExchangeData.GetIndexKlinesAsync("BTC-USD", KlineInterval.OneMinute)` |
| Get funding rates | `client.UnifiedApi.ExchangeData.GetFundingRatesAsync("BTC-USDT-SWAP")` |
| Get funding rate history | `client.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync("BTC-USDT-SWAP")` |
| Get open interest | `client.UnifiedApi.ExchangeData.GetOpenInterestsAsync(InstrumentType.Swap, symbol: "BTC-USDT-SWAP")` |
| Get price limits | `client.UnifiedApi.ExchangeData.GetPriceLimitsAsync("BTC-USDT-SWAP")` |
| Get position tiers | `client.UnifiedApi.ExchangeData.GetPositionTiersAsync(InstrumentType.Swap, MarginMode.Cross, "BTC-USDT")` |
| Get insurance fund | `client.UnifiedApi.ExchangeData.GetInsuranceFundAsync(InstrumentType.Swap)` |
| Get 24h platform volume | `client.UnifiedApi.ExchangeData.Get24HourVolumeAsync()` |
| Get announcements | `client.UnifiedApi.ExchangeData.GetAnnouncementsAsync()` |
| Get announcement types | `client.UnifiedApi.ExchangeData.GetAnnouncementTypesAsync()` |

## Trading Statistics REST

| User intent | OKX.Net member |
|---|---|
| Contract open interest and volume | `client.UnifiedApi.ExchangeData.GetTradeStatsContractSummaryAsync("BTC")` |
| Long/short account ratio | `client.UnifiedApi.ExchangeData.GetTradeStatsLongShortRatioAsync("BTC")` |
| Margin lending ratio | `client.UnifiedApi.ExchangeData.GetTradeStatsMarginLendingRatioAsync("BTC")` |
| Options open interest and volume | `client.UnifiedApi.ExchangeData.GetTradeStatsOptionsSummaryAsync("BTC")` |
| Options expiry open interest/volume | `client.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeExpiryAsync("BTC")` |
| Options strike open interest/volume | `client.UnifiedApi.ExchangeData.GetTradeStatsInterestVolumeStrikeAsync("BTC", "20260626")` |
| Put/call ratio | `client.UnifiedApi.ExchangeData.GetTradeStatsPutCallRatioAsync("BTC")` |
| Taker volume | `client.UnifiedApi.ExchangeData.GetTradeStatsTakerVolumeAsync("BTC", InstrumentType.Spot)` |
| Taker flow | `client.UnifiedApi.ExchangeData.GetTradeStatsTakerFlowAsync("BTC")` |
| Supported trading-stat assets | `client.UnifiedApi.ExchangeData.GetTradeStatsSupportedAssetsAsync()` |

## Account And Funding REST

| User intent | OKX.Net member |
|---|---|
| Get account balance | `client.UnifiedApi.Account.GetAccountBalanceAsync()` |
| Get balance for one asset | `client.UnifiedApi.Account.GetAccountBalanceAsync("USDT")` |
| Get account configuration | `client.UnifiedApi.Account.GetAccountConfigurationAsync()` |
| Get positions | `client.UnifiedApi.Account.GetPositionsAsync(InstrumentType.Swap, "BTC-USDT-SWAP")` |
| Get position history | `client.UnifiedApi.Account.GetPositionHistoryAsync(...)` |
| Get position risk | `client.UnifiedApi.Account.GetPositionRiskAsync(...)` |
| Get account risk state | `client.UnifiedApi.Account.GetAccountRiskStateAsync()` |
| Set leverage | `client.UnifiedApi.Account.SetLeverageAsync(leverage, MarginMode.Cross, symbol: "BTC-USDT-SWAP")` |
| Get leverage | `client.UnifiedApi.Account.GetLeverageAsync("BTC-USDT-SWAP", MarginMode.Cross)` |
| Set position mode | `client.UnifiedApi.Account.SetPositionModeAsync(PositionMode.LongShortMode)` |
| Set account mode | `client.UnifiedApi.Account.SetAccountModeAsync(AccountLevel.Simple)` |
| Precheck account mode switch | `client.UnifiedApi.Account.PrecheckAccountModeSwitchAsync(AccountLevel.Simple)` |
| Transfer funds | `client.UnifiedApi.Account.TransferAsync(...)` |
| Get transfer | `client.UnifiedApi.Account.GetTransferAsync(...)` |
| Get funding balance | `client.UnifiedApi.Account.GetFundingBalanceAsync()` |
| Get assets | `client.UnifiedApi.Account.GetAssetsAsync()` |
| Get deposit address | `client.UnifiedApi.Account.GetDepositAddressAsync("USDT")` |
| Get deposit history | `client.UnifiedApi.Account.GetDepositHistoryAsync(...)` |
| Get withdrawal history | `client.UnifiedApi.Account.GetWithdrawalHistoryAsync(...)` |
| Withdraw asset | `client.UnifiedApi.Account.WithdrawAsync(...)` |
| Cancel withdrawal | `client.UnifiedApi.Account.CancelWithdrawalAsync(withdrawalId)` |
| Get fee rates | `client.UnifiedApi.Account.GetFeeRatesAsync(...)` |
| Get maximum amount | `client.UnifiedApi.Account.GetMaximumAmountAsync(...)` |
| Get maximum available amount | `client.UnifiedApi.Account.GetMaximumAvailableAmountAsync(...)` |
| Get maximum loan amount | `client.UnifiedApi.Account.GetMaximumLoanAmountAsync(...)` |
| Get maximum withdrawals | `client.UnifiedApi.Account.GetMaximumWithdrawalsAsync()` |
| Get saving balances | `client.UnifiedApi.Account.GetSavingBalancesAsync()` |
| Purchase/redeem savings | `client.UnifiedApi.Account.SavingPurchaseRedemptionAsync(...)` |
| Manual borrow/repay | `client.UnifiedApi.Account.ManualBorrowRepay(...)` |
| Set auto repay | `client.UnifiedApi.Account.SetAutoRepayAsync(true)` |
| Get borrow/repay history | `client.UnifiedApi.Account.GetBorrowRepayHistoryAsync(...)` |

## Trading REST

| User intent | OKX.Net member |
|---|---|
| Place spot order | `client.UnifiedApi.Trading.PlaceOrderAsync("BTC-USDT", OrderSide.Buy, OrderType.Limit, quantity, price, tradeMode: TradeMode.Cash)` |
| Place swap/futures order | `client.UnifiedApi.Trading.PlaceOrderAsync("BTC-USDT-SWAP", OrderSide.Buy, OrderType.Market, quantity, positionSide: PositionSide.Long, tradeMode: TradeMode.Cross)` |
| Place multiple orders | `client.UnifiedApi.Trading.PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest>)` |
| Precheck order | `client.UnifiedApi.Trading.CheckOrderAsync(...)` |
| Get open orders | `client.UnifiedApi.Trading.GetOrdersAsync(...)` |
| Get order details | `client.UnifiedApi.Trading.GetOrderDetailsAsync(symbol, orderId)` |
| Get order history | `client.UnifiedApi.Trading.GetOrderHistoryAsync(InstrumentType.Spot, symbol: "BTC-USDT")` |
| Get order archive | `client.UnifiedApi.Trading.GetOrderArchiveAsync(InstrumentType.Spot, symbol: "BTC-USDT")` |
| Cancel order | `client.UnifiedApi.Trading.CancelOrderAsync(symbol, orderId)` |
| Cancel multiple orders | `client.UnifiedApi.Trading.CancelMultipleOrdersAsync(...)` |
| Amend order | `client.UnifiedApi.Trading.AmendOrderAsync(...)` |
| Amend multiple orders | `client.UnifiedApi.Trading.AmendMultipleOrdersAsync(...)` |
| Close position | `client.UnifiedApi.Trading.ClosePositionAsync("BTC-USDT-SWAP", MarginMode.Cross, PositionSide.Long)` |
| Get user trades | `client.UnifiedApi.Trading.GetUserTradesAsync(...)` |
| Get user trade archive | `client.UnifiedApi.Trading.GetUserTradesArchiveAsync(...)` |
| Place algo order | `client.UnifiedApi.Trading.PlaceAlgoOrderAsync(...)` |
| Get algo order | `client.UnifiedApi.Trading.GetAlgoOrderAsync(...)` |
| Get algo order list | `client.UnifiedApi.Trading.GetAlgoOrderListAsync(...)` |
| Get algo order history | `client.UnifiedApi.Trading.GetAlgoOrderHistoryAsync(...)` |
| Cancel algo order | `client.UnifiedApi.Trading.CancelAlgoOrderAsync(...)` |
| Amend algo order | `client.UnifiedApi.Trading.AmendAlgoOrderAsync(...)` |
| Cancel all after timeout | `client.UnifiedApi.Trading.CancelAllAfterAsync(timeout)` |
| Get account rate limits | `client.UnifiedApi.Trading.GetAccountRateLimitAsync()` |

## Sub-Accounts REST

| User intent | OKX.Net member |
|---|---|
| Get sub-accounts | `client.UnifiedApi.SubAccounts.GetSubAccountsAsync()` |
| Create sub-account | `client.UnifiedApi.SubAccounts.CreateSubAccountAsync(...)` |
| Get sub-account bills | `client.UnifiedApi.SubAccounts.GetSubAccountBillsAsync(...)` |
| Get sub-account funding balances | `client.UnifiedApi.SubAccounts.GetSubAccountFundingBalancesAsync(...)` |
| Get sub-account trading balances | `client.UnifiedApi.SubAccounts.GetSubAccountTradingBalancesAsync(...)` |
| Transfer between sub-accounts | `client.UnifiedApi.SubAccounts.TransferBetweenSubAccountsAsync(...)` |
| Create sub-account API key | `client.UnifiedApi.SubAccounts.CreateSubAccountApiKeyAsync(...)` |
| Get sub-account API keys | `client.UnifiedApi.SubAccounts.GetSubAccountApiKeysAsync(...)` |
| Reset sub-account API key | `client.UnifiedApi.SubAccounts.ResetSubAccountApiKeyAsync(...)` |
| Delete sub-account API key | `client.UnifiedApi.SubAccounts.DeleteSubAccountApiKeyAsync(...)` |
| Set sub-account transfer out | `client.UnifiedApi.SubAccounts.SetSubAccountTransferOutAsync(...)` |

## Copy Trading REST

| User intent | OKX.Net member |
|---|---|
| Get copy trading account configuration | `client.UnifiedApi.CopyTrading.GetAccountConfigurationAsync()` |
| Get lead positions | `client.UnifiedApi.CopyTrading.GetLeadPositionsAsync(...)` |
| Get lead position history | `client.UnifiedApi.CopyTrading.GetLeadPositionHistoryAsync(...)` |
| Get lead trader current positions | `client.UnifiedApi.CopyTrading.GetLeadTraderCurrentLeadPositionsAsync(...)` |
| Place lead stop order | `client.UnifiedApi.CopyTrading.PlaceLeadStopOrderAsync(...)` |
| Close lead position | `client.UnifiedApi.CopyTrading.CloseLeadPositionAsync(...)` |
| Get leading instruments | `client.UnifiedApi.CopyTrading.GetLeadingInstrumentsAsync(...)` |
| Amend leading instruments | `client.UnifiedApi.CopyTrading.AmendLeadingInstrumentsAsync(...)` |

## WebSocket Exchange Data

| User intent | OKX.Net member |
|---|---|
| Subscribe ticker updates | `socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many ticker updates | `socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe trades | `socketClient.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe many trades | `socketClient.UnifiedApi.ExchangeData.SubscribeToTradeUpdatesAsync(symbols, handler)` |
| Subscribe klines | `socketClient.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe many klines | `socketClient.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbols, interval, handler)` |
| Subscribe order book | `socketClient.UnifiedApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(symbol, OrderBookType.OrderBook, handler)` |
| Subscribe many order books | `socketClient.UnifiedApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(symbols, OrderBookType.OrderBook, handler)` |
| Subscribe funding rates | `socketClient.UnifiedApi.ExchangeData.SubscribeToFundingRateUpdatesAsync(symbol, handler)` |
| Subscribe mark prices | `socketClient.UnifiedApi.ExchangeData.SubscribeToMarkPriceUpdatesAsync(symbol, handler)` |
| Subscribe mark price klines | `socketClient.UnifiedApi.ExchangeData.SubscribeToMarkPriceKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe index ticker | `socketClient.UnifiedApi.ExchangeData.SubscribeToIndexTickerUpdatesAsync(symbol, handler)` |
| Subscribe index klines | `socketClient.UnifiedApi.ExchangeData.SubscribeToIndexKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe open interest | `socketClient.UnifiedApi.ExchangeData.SubscribeToOpenInterestUpdatesAsync(symbol, handler)` |
| Subscribe price limits | `socketClient.UnifiedApi.ExchangeData.SubscribeToPriceLimitUpdatesAsync(symbol, handler)` |
| Subscribe symbol updates | `socketClient.UnifiedApi.ExchangeData.SubscribeToSymbolUpdatesAsync(InstrumentType.Spot, handler)` |
| Subscribe system status | `socketClient.UnifiedApi.ExchangeData.SubscribeToSystemStatusUpdatesAsync(handler)` |
| Subscribe option summary | `socketClient.UnifiedApi.ExchangeData.SubscribeToOptionSummaryUpdatesAsync(instrumentFamily, handler)` |

## WebSocket Account And Trading

| User intent | OKX.Net member |
|---|---|
| Subscribe account balance updates | `socketClient.UnifiedApi.Account.SubscribeToAccountUpdatesAsync(asset, regularUpdates, handler)` |
| Subscribe balance and position updates | `socketClient.UnifiedApi.Account.SubscribeToBalanceAndPositionUpdatesAsync(handler)` |
| Subscribe deposit updates | `socketClient.UnifiedApi.Account.SubscribeToDepositUpdatesAsync(handler)` |
| Subscribe withdrawal updates | `socketClient.UnifiedApi.Account.SubscribeToWithdrawalUpdatesAsync(handler)` |
| Subscribe order updates | `socketClient.UnifiedApi.Trading.SubscribeToOrderUpdatesAsync(InstrumentType.Spot, symbol, instrumentFamily, handler)` |
| Subscribe user trade updates | `socketClient.UnifiedApi.Trading.SubscribeToUserTradeUpdatesAsync(...)` |
| Subscribe position updates | `socketClient.UnifiedApi.Trading.SubscribeToPositionUpdatesAsync(...)` |
| Subscribe liquidation warnings | `socketClient.UnifiedApi.Trading.SubscribeToLiquidationWarningUpdatesAsync(...)` |
| Subscribe algo order updates | `socketClient.UnifiedApi.Trading.SubscribeToAlgoOrderUpdatesAsync(...)` |
| Subscribe advanced algo order updates | `socketClient.UnifiedApi.Trading.SubscribeToAdvanceAlgoOrderUpdatesAsync(...)` |
| Socket API place order | `socketClient.UnifiedApi.Trading.PlaceOrderAsync(...)` |
| Socket API place multiple orders | `socketClient.UnifiedApi.Trading.PlaceMultipleOrdersAsync(...)` |
| Socket API cancel order | `socketClient.UnifiedApi.Trading.CancelOrderAsync(...)` |
| Socket API cancel multiple orders | `socketClient.UnifiedApi.Trading.CancelMultipleOrdersAsync(...)` |
| Socket API amend order | `socketClient.UnifiedApi.Trading.AmendOrderAsync(...)` |
| Socket API amend multiple orders | `socketClient.UnifiedApi.Trading.AmendMultipleOrdersAsync(...)` |

WebSocket subscription methods return `WebSocketResult<UpdateSubscription>`. Socket API request/response methods return `QueryResult<T>`.

## SharedApis

| User intent | OKX.Net member or interface |
|---|---|
| Shared REST client | `new OKXRestClient().UnifiedApi.SharedClient` |
| Shared socket client | `new OKXSocketClient().UnifiedApi.SharedClient` |
| Discover shared capabilities | `client.UnifiedApi.SharedClient.Discover()` |
| Shared spot symbols and catalog | `ISpotSymbolRestClient.GetSpotSymbolsAsync(...)` and `ISpotSymbolRestClient.SpotSymbolCatalog` |
| Shared futures symbols and catalog | `IFuturesSymbolRestClient.GetFuturesSymbolsAsync(...)` and `IFuturesSymbolRestClient.FuturesSymbolCatalog` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared balances REST | `IBalanceRestClient.GetBalancesAsync(...)` |
| Shared klines REST | `IKlineRestClient.GetKlinesAsync(...)` |
| Shared order book REST | `IOrderBookRestClient.GetOrderBookAsync(...)` |
| Shared funding rate REST | `IFundingRateRestClient.GetFundingRateAsync(...)` |
| Shared leverage REST | `ILeverageRestClient.SetLeverageAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared trade socket | `ITradeSocketClient.SubscribeToTradeUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |
| Shared balance socket | `IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(...)` |
| Shared position socket | `IPositionSocketClient.SubscribeToPositionUpdatesAsync(...)` |

Shared REST methods return `HttpResult<T>` or `HttpResult`. Shared socket subscriptions return `WebSocketResult<UpdateSubscription>`. Shared symbol/cache helper methods can return `ExchangeCallResult<T>`.

Shared spot and futures symbols populate `DisplayName`, `BaseAssetType`, `BaseAssetSubType`, `QuoteAssetType`, and `QuoteAssetSubType`. Use this metadata for crypto, fiat, TradFi, stablecoin, equity, and commodity classification instead of parsing symbol strings.

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | REST methods return `HttpResult<T>` or `HttpResult`; use `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | Socket subscriptions return `WebSocketResult<UpdateSubscription>`; use `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Socket API request success check | Socket request/response methods return `QueryResult<T>`; use the same `.Success` and `.Error` pattern |
| Read result data | Read `result.Data` or `sub.Data` only after `.Success` |
| Shared helper success check | Shared symbol/cache helpers can return `ExchangeCallResult<T>`; use the same `.Success` and `.Error` pattern |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `OKXClient` | `OKXRestClient` |
| `ApiCredentials` in examples | `OKXCredentials("key", "secret", "passphrase")` |
| `SpotApi` | `UnifiedApi` with `InstrumentType.Spot` or spot symbol |
| `FuturesApi` / `SwapApi` | `UnifiedApi` with futures/swap symbol and `InstrumentType` |
| Binance-style `BTCUSDT` | OKX symbol format `BTC-USDT` |
| `.Data` without `.Success` check | Check `.Success` first |
| Recreating clients per request | Reuse clients or use `services.AddOKX(...)` |
| Forgetting WebSocket teardown | Store `UpdateSubscription` and call `UnsubscribeAsync` |
