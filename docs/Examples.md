---
title: Examples
nav_order: 3
---

## Basic operations
Make sure to read the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#processing-request-responses) on processing responses.

<BlockQuote>

### Get market data
```csharp
// Getting info on all symbols
var spotSymbols = await okxRestClient.UnifiedApi.ExchangeData.GetSymbolsAsync(OKXInstrumentType.Spot);
var futuresSymbols = await okxRestClient.UnifiedApi.ExchangeData.GetSymbolsAsync(OKXInstrumentType.Futures);

// Getting tickers for all symbols
var tickerData = await okxRestClient.UnifiedApi.ExchangeData.GetTickersAsync(OKXInstrumentType.Spot);

// Getting the order book of a symbol
var orderBookData = await okxRestClient.UnifiedApi.ExchangeData.GetOrderBookAsync("BTC-USDT");

// Getting recent trades of a symbol
var tradeHistoryData = await okxRestClient.UnifiedApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT");
```

### Requesting balances
```csharp
var balances = await okxRestClient.UnifiedApi.Account.GetAccountBalanceAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await okxRestClient.UnifiedApi.Trading.PlaceOrderAsync("ETH-USDT", OKXOrderSide.Buy, OKXOrderType.LimitOrder, 0.5m, 1800m);
```

### Requesting a specific order
```csharp
// Request info on order with id `123`
var orderData = await okxRestClient.UnifiedApi.Trading.GetOrderDetailsAsync("ETH-USDT", 123);
```

### Requesting order history
```csharp
// Get all orders conform the parameters. This example gets all BTC-USDT limit orders which are currently active
 var ordersData = await okxRestClient.UnifiedApi.Trading.GetOrdersAsync(symbol: "ETH-USDT");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await okxRestClient.UnifiedApi.Trading.CancelOrderAsync("ETH-USDT", 123);
```

### Get user trades
```csharp
var userTradesResult = await okxRestClient.UnifiedApi.Trading.GetUserTradesAsync();
```

### Subscribing to market data updates
```csharp
var subscribeResult = okxSocketClient.UnifiedApi.SubscribeToTickerUpdatesAsync("ETH-USDT", data =>
{
    // Handle ticker data
});
```

### Subscribing to order updates
```csharp
var subscribeResult = await okxSocketClient.UnifiedApi.SubscribeToOrderUpdatesAsync(OKXInstrumentType.Any, null, null, data =>
{
    // Handle order updates
});
```

</BlockQuote>

