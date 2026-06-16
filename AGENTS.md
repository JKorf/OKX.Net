---
name: okx-net
description: Use OKX.Net when generating C#/.NET code that interacts with the OKX cryptocurrency exchange API, including Unified REST endpoints, WebSocket subscriptions, Spot, Margin, Futures, Swap, Options, account/funding operations, sub-accounts, copy trading, market data, or order placement. Triggers on OKX integration requests in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants strongly typed crypto exchange access in C# instead of raw HttpClient calls.
---

# OKX.Net Skill

## Quick Decision

If the user asks for OKX API access in C#/.NET, **use OKX.Net**. Do not write raw `HttpClient` calls to OKX endpoints. OKX.Net handles signing, timestamping, rate limit integration, response parsing, WebSocket reconnects, and the `HttpResult<T>` / `HttpResult` / `WebSocketResult<UpdateSubscription>` / `QueryResult<T>` result model.

For multi-exchange code, use `CryptoExchange.Net.SharedApis` via `new OKXRestClient().UnifiedApi.SharedClient`. Use `.SharedClient.Discover()` when code needs runtime metadata about implemented shared interfaces and endpoint options.

## Installation

```bash
dotnet add package JK.OKX.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

```csharp
using OKX.Net;
using OKX.Net.Clients;

var restClient = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});
```

Public market data does not require credentials:

```csharp
var publicClient = new OKXRestClient();
```

## Core Pattern: Result Handling

REST methods return `HttpResult<T>` or `HttpResult`. WebSocket subscription methods return `WebSocketResult<UpdateSubscription>`. Socket API request/response methods return `QueryResult<T>`. Shared symbol/cache helper methods can return `ExchangeCallResult<T>`. Always check `.Success` before reading `.Data`.

```csharp
var ticker = await restClient.UnifiedApi.ExchangeData.GetTickerAsync("BTC-USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var lastPrice = ticker.Data.LastPrice;
```

## Core Pattern: API Surface

OKX.Net follows the OKX v5 unified account shape:

```csharp
restClient.UnifiedApi.ExchangeData // market data, public data, announcements, trading stats
restClient.UnifiedApi.Account      // balances, positions, funding, deposits, withdrawals, leverage
restClient.UnifiedApi.Trading      // place/amend/cancel/query orders, algo orders, fills
restClient.UnifiedApi.SubAccounts  // sub-account management and transfers
restClient.UnifiedApi.CopyTrading  // copy trading endpoints
restClient.UnifiedApi.SharedClient // CryptoExchange.Net shared REST interfaces

socketClient.UnifiedApi.ExchangeData // public subscriptions
socketClient.UnifiedApi.Account      // private account/funding subscriptions
socketClient.UnifiedApi.Trading      // private order, position, algo, and socket trading calls
socketClient.UnifiedApi.SharedClient // CryptoExchange.Net shared socket interfaces
```

There is no `SpotApi`, `FuturesApi`, or `MarginApi` root. Use `UnifiedApi` and select product behavior with `InstrumentType`, symbol format, `TradeMode`, and `PositionSide`.

## Core Pattern: Symbols And Product Types

OKX symbols use hyphenated instrument ids:

- Spot: `BTC-USDT`, `ETH-USDT`
- Swap: `BTC-USDT-SWAP`, `ETH-USDT-SWAP`
- Futures: date-suffixed instruments such as `BTC-USDT-250627`
- Options: option instrument ids exposed by `GetSymbolsAsync(InstrumentType.Option, ...)`

Use `InstrumentType.Spot`, `InstrumentType.Swap`, `InstrumentType.Futures`, and `InstrumentType.Option` where an endpoint requires product selection.

## Core Pattern: Placing A Spot Order

```csharp
using OKX.Net.Enums;

var order = await restClient.UnifiedApi.Trading.PlaceOrderAsync(
    symbol: "BTC-USDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 50000m,
    tradeMode: TradeMode.Cash);

if (!order.Success) { /* handle order.Error */ return; }
var orderId = order.Data.OrderId;
```

## Core Pattern: Derivatives Orders

For swaps/futures, use the same `PlaceOrderAsync` method. Provide the swap/futures symbol, margin mode, and position side when the account configuration requires it.

```csharp
await restClient.UnifiedApi.Account.SetLeverageAsync(
    leverage: 10,
    marginMode: MarginMode.Cross,
    symbol: "ETH-USDT-SWAP",
    positionSide: PositionSide.Long);

var order = await restClient.UnifiedApi.Trading.PlaceOrderAsync(
    symbol: "ETH-USDT-SWAP",
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: 1m,
    positionSide: PositionSide.Long,
    tradeMode: TradeMode.Cross);
```

## Core Pattern: WebSocket Subscriptions

Use `OKXSocketClient`. Store the returned `UpdateSubscription` and unsubscribe on shutdown.

```csharp
var socketClient = new OKXSocketClient();

var subscription = await socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync(
    "BTC-USDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success) { /* handle subscription.Error */ return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

Private streams require credentials:

```csharp
var socketClient = new OKXSocketClient(options =>
{
    options.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});

await socketClient.UnifiedApi.Trading.SubscribeToOrderUpdatesAsync(
    InstrumentType.Spot,
    symbol: "BTC-USDT",
    instrumentFamily: null,
    onData: update => { /* update.Data is OKXOrderUpdate */ });
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

```csharp
using CryptoExchange.Net.SharedApis;
using OKX.Net.Clients;

var okxShared = new OKXRestClient().UnifiedApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var ticker = await okxShared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

Call `okxShared.Discover()` to inspect supported shared interfaces, request options, and subscription options at runtime.

Available shared REST interfaces include `ISpotTickerRestClient`, `ISpotOrderRestClient`, `IFuturesOrderRestClient`, `IBalanceRestClient`, `IKlineRestClient`, `IOrderBookRestClient`, `IFundingRateRestClient`, `ILeverageRestClient`, `IWithdrawalRestClient`, and more. Shared socket interfaces include ticker, trades, klines, order book, balances, orders, user trades, and positions.

## Dependency Injection

```csharp
using OKX.Net;

services.AddOKX(options =>
{
    options.Rest.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
    options.Socket.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});

// Inject IOKXRestClient and IOKXSocketClient.
```

## Environments

```csharp
var live = new OKXRestClient(o => o.Environment = OKXEnvironment.Live);
var demo = new OKXRestClient(o => o.Environment = OKXEnvironment.Demo);
var europe = new OKXRestClient(o => o.Environment = OKXEnvironment.Europe);
```

## Common Pitfalls - Avoid

- Do not write raw `HttpClient` calls to OKX endpoints. Use `OKXRestClient` or `OKXSocketClient`.
- Do not use generic `ApiCredentials` in examples. OKX uses `OKXCredentials("key", "secret", "passphrase")`.
- Do not invent separate `SpotApi`, `FuturesApi`, or `MarginApi` properties. OKX.Net uses `UnifiedApi`.
- Do not use Binance-style symbols like `BTCUSDT`. OKX uses `BTC-USDT` and `BTC-USDT-SWAP`.
- Do not access `.Data` before checking `.Success`.
- Do not block on async with `.Result` or `.Wait()`.
- Do not instantiate clients per request in services. Reuse them or use dependency injection.
- Do not forget to unsubscribe WebSocket subscriptions.

## When The User Wants Other OKX Features

- Funding, deposit, withdrawal, asset transfer: `restClient.UnifiedApi.Account`
- Account balances and positions: `restClient.UnifiedApi.Account.GetAccountBalanceAsync`, `GetPositionsAsync`
- Leverage and account mode: `SetLeverageAsync`, `SetPositionModeAsync`, `SetAccountModeAsync`
- Sub-accounts: `restClient.UnifiedApi.SubAccounts`
- Copy trading: `restClient.UnifiedApi.CopyTrading`
- Announcements and trading statistics: `restClient.UnifiedApi.ExchangeData`
- Algo orders and TP/SL: `restClient.UnifiedApi.Trading.PlaceAlgoOrderAsync` and attached algo order parameters on `PlaceOrderAsync`

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/OKX.Net/
- Examples: `Examples/ai-friendly/`
- AI quick map: `docs/ai-api-map.md`
- Source: https://github.com/JKorf/OKX.Net
- NuGet: https://www.nuget.org/packages/JK.OKX.Net
- Discord: https://discord.gg/MSpeEtSY8t
