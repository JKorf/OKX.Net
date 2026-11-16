# OKX.Net usage

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis;
using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
```

## Public setup and results

```csharp
using var client = new OKXRestClient();
var result = await client.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

## Credentials and account read

```csharp
var key = Environment.GetEnvironmentVariable("OKX_KEY")
    ?? throw new InvalidOperationException("API key is required");
var secret = Environment.GetEnvironmentVariable("OKX_SECRET")
    ?? throw new InvalidOperationException("API secret is required");
var pass = Environment.GetEnvironmentVariable("OKX_PASSPHRASE")
    ?? throw new InvalidOperationException("API passphrase is required");
using var client = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXCredentials(key, secret, pass);
});
var result = await client.UnifiedApi.Account.GetAccountBalanceAsync();
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data);
```

Set `options.Environment = OKXEnvironment.Demo` explicitly for a Demo workflow with suitable credentials; the default client targets live APIs.

## Spot order check

This assumes the credentialed client above. It checks an order without submitting it; see [execution considerations](safety.md#orders-and-nested-outcomes) before adapting to live placement.

```csharp
var result = await client.UnifiedApi.Trading.CheckOrderAsync(
    symbol: "ETH-USDT", side: OrderSide.Buy, type: OrderType.Limit,
    quantity: 0.01m, price: 2000m, tradeMode: TradeMode.Cash);
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data);
```

## Derivatives positions

```csharp
var result = await client.UnifiedApi.Account.GetPositionsAsync(
    InstrumentType.Swap, symbol: "ETH-USDT-SWAP");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
foreach (var position in result.Data)
    Console.WriteLine($"{position.Symbol} {position.PositionSide}: {position.PositionsQuantity}");
```

Closing and leverage operations depend on the actual account/position mode; read [mode considerations](safety.md#account-and-position-modes).

## Socket instrument code lookup

Socket order requests require a numeric instrument code rather than a string symbol:

```csharp
var result = await client.UnifiedApi.ExchangeData.GetSymbolsAsync(
    InstrumentType.Spot, symbol: "ETH-USDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
var instrument = result.Data.FirstOrDefault();
if (instrument?.SymbolCode is not long code)
    return;
Console.WriteLine($"Socket symbol code: {code}");
```

Socket `UnifiedApi.Trading.PlaceOrderAsync`, amend and cancel requests use the code and return query results. They are live account-changing operations; query results are not subscription handles.

## WebSocket subscription lifecycle

```csharp
using var socketClient = new OKXSocketClient();
var subscription = await socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync(
    "ETH-USDT", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Private account/trading streams use credentialed socket options with the same credential shape and method-specific instrument filters.

## Shared API V2

```csharp
IGetTickerRest ticker = client.UnifiedApi.SharedApi;
var result = await ticker.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "ETH", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Load symbols before reading their catalog:

```csharp
IGetSpotSymbolsRest symbolsClient = client.UnifiedApi.SharedApi;
var result = await symbolsClient.GetSpotSymbolsAsync(new GetSymbolsRequest());
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
foreach (var symbol in result.Data.Take(5))
    Console.WriteLine($"{symbol.Name}: {symbol.BaseAsset}/{symbol.QuoteAsset}");
var catalog = symbolsClient.SpotSymbolCatalog;
```

## Dependency injection and dynamic lookup

```csharp
var services = new ServiceCollection();
services.AddOKX();
using var provider = services.BuildServiceProvider();
var aggregate = provider.GetRequiredService<IOKXSharedApiClient>();
var match = aggregate.GetCapability(
    SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot);
if (match is null)
    return;
var result = await match.Capability.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "ETH", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Configure `options.ApiCredentials` for authenticated consumers, or `options.Rest.ApiCredentials` and `options.Socket.ApiCredentials` independently. Inject `IOKXRestClient`, `IOKXSocketClient`, the aggregate, or registered narrow capabilities. [API routing](api-surfaces.md#shared-api-v2) describes selection when several exchanges are registered.

## Complete programs (checkout-only)

[Examples](../../../../Examples/ai-friendly/README.md) contains independent Unified/Spot, derivatives, socket, Shared API V2, and public-read retry programs. These links require a checkout; the snippets above work with the installed package.
