# ![Icon!](https://github.com/JKorf/OKX.Net/blob/358d31f58d8ee51fc234bff1940878a8d0ce5676/Okex.Net/Icon/icon.png "OKX.Net") OKX.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/OKX.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/OKX.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/OKX.Net?style=for-the-badge)
![Since](https://img.shields.io/badge/since-2023-brightgreen?style=for-the-badge)

OKX.Net is strongly typed client library for accessing the [OKX REST and Websocket API](https://www.okx.com/docs-v5/en/).
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* High performance
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Benchmark
Performance is a core focus. For a benchmark comparing OKX.Net performance to CCXT and OKX.Api, see [docs/okx-net-benchmark.md](docs/okx-net-benchmark.md).

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as the latest dotnet versions to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.OKX.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JK.OKX.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.OKX.Net)

	dotnet add package JK.OKX.Net
	
### GitHub packages
OKX.Net is available on [GitHub packages](https://github.com/JKorf/OKX.Net/pkgs/nuget/JK.OKX.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/OKX.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/OKX.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/OKX.Net/releases).

## How to use
*Basic request:* 

```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new OKXRestClient();
var tickerResult = await restClient.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
var lastPrice = tickerResult.Data.LastPrice;
```
	
*Place order:*
```csharp
var restClient = new OKXRestClient(opts => {
	opts.ApiCredentials = new OKXCredentials("APIKEY", "APISECRET", "PASS");
});

// Place Limit order to buy 10 long contracts of ETH/USD at 2000
var orderResult = await restClient.UnifiedApi.Trading.PlaceOrderAsync(
    "ETH-USD-SWAP",
    OrderSide.Buy,
    OrderType.Limit,
    quantity: 10m,
    price: 2000,
    positionSide: PositionSide.Long,
    tradeMode: TradeMode.Cross
    );
```

*WebSocket subscription:* 

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new OKXSocketClient();
var tickerSubscriptionResult = socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETH-USDT", (update) =>
{
	var lastPrice = update.Data.LastPrice;
});
```

**Note that for European clients the environment should be updated to Europe like so:**
```csharp
// With DI
services.AddOKX(options =>
{
    options.Environment = OKXEnvironment.Europe;
});

// Or when constructing client
var client = new OKXRestClient(options => 
{
    options.Environment = OKXEnvironment.Europe;
});
```


For information on the clients, dependency injection, response processing and more see the [OKX.Net documentation](https://cryptoexchange.jkorf.dev?library=OKX.Net) or have a look at the examples [here](https://github.com/JKorf/OKX.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## AI / LLM documentation

OKX.Net includes AI-oriented documentation and examples for code generation tools:

|File|Purpose|
|--|--|
|[`AGENTS.md`](AGENTS.md)|Assistant skill with core OKX.Net patterns, pitfalls, and examples|
|[`llms.txt`](llms.txt)|Short LLM index with links to docs, examples, and critical usage rules|
|[`llms-full.txt`](llms-full.txt)|Detailed LLM context with endpoint routing, code patterns, and anti-hallucination checks|
|[`docs/ai-api-map.md`](docs/ai-api-map.md)|Table-style intent-to-method map for Unified REST, WebSocket, account, trading, sub-account, copy trading, and SharedApis|
|[`Examples/ai-friendly`](Examples/ai-friendly)|Compilable single-file examples for common REST, WebSocket, shared API, and error handling workflows|

See [cryptoexchange-skills-hub](https://github.com/JKorf/cryptoexchange-skills-hub) for installable skills.

## CryptoExchange.Net
OKX.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Aster|[JKorf/Aster.Net](https://github.com/JKorf/Aster.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Aster.Net)|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bitstamp|[JKorf/Bitstamp.Net](https://github.com/JKorf/Bitstamp.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitstamp.Net.svg?style=flat-square)](https://www.nuget.org/packages/Bitstamp.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Lighter|[JKorf/Lighter.Net](https://github.com/JKorf/Lighter.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Lighter.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Lighter.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|Pionex|[JKorf/Pionex.Net](https://github.com/JKorf/Pionex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Pionex.net.svg?style=flat-square)](https://www.nuget.org/packages/Pionex.Net)|
|Polymarket|[JKorf/Polymarket.Net](https://github.com/JKorf/Polymarket.Net)|[![Nuget version](https://img.shields.io/nuget/v/Polymarket.net.svg?style=flat-square)](https://www.nuget.org/packages/Polymarket.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|Weex|[JKorf/Weex.Net](https://github.com/JKorf/Weex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Weex.net.svg?style=flat-square)](https://www.nuget.org/packages/Weex.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Supported functionality

### Unified API
|API|Supported|Location|
|--|--:|--|
|Trading Account Rest|✓|`restClient.UnifiedApi.Account`|
|Trading Account Websocket|✓|`socketClient.UnifiedApi.Account`|
|Order Book Trade Rest|✓|`restClient.UnifiedApi.Trading`|
|Order Book Trade Websocket|✓|`socketClient.UnifiedApi.Trading`|
|Order Book Algo Trading Rest|✓|`restClient.UnifiedApi.Trading`|
|Order Book Algo Trading Websocket|✓|`socketClient.UnifiedApi.Trading`|
|Order Book Grid Trading Rest|x||
|Order Book Grid Trading Websocket|x||
|Order Book Signal Trading Rest|x||
|Order Book Recurring Buy Rest|x||
|Order Book Recurring Buy Websocket|x||
|Order Book Copy Trading Rest|x||
|Order Book Copy Trading Websocket|x||
|Order Book Market Data Rest|✓|`restClient.UnifiedApi.ExchangeData`|
|Order Book Market Data Websocket|✓|`socketClient.UnifiedApi.ExchangeData`|
|Block Trading|X||
|Spread Trading|X||
|Public Data Rest|✓|`restClient.UnifiedApi.ExchangeData`|
|Public Data Websocket|✓|`socketClient.UnifiedApi.ExchangeData`|
|Trading Statistics|✓|`restClient.UnifiedApi.ExchangeData`|
|Funding Account Rest|✓|`restClient.UnifiedApi.Account`|
|Funding Account Websocket|✓|`socketClient.UnifiedApi.Account`|
|Sub-Account|✓|`restClient.UnifiedApi.SubAccounts`|
|Financial Product|X||
|Affiliate|X||
|Status Rest|✓|`restClient.UnifiedApi.ExchangeData`|
|Status Websocket|✓|`socketClient.UnifiedApi.ExchangeData`|

### Broker API
|API|Supported|Location|
|--|--:|--|
|Trading Account Rest|X||

## Support the project
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://okx.com/join/48046699)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 5.2.0 - 21 Jul 2026
    * Updated CryptoExchange.Net to v12.2.0 
    * Added SpotSymbolCatalog to Shared ISpotSymbolRestClient interface
    * Added FuturesSymbolCatalog to Shared IFuturesSymbolRestClient interface
    * Added BaseAssetType, BaseAssetSubType, QuoteAssetType and QuoteAssetSubType to GetSymbolsRequest model
    * Added DisplayName to SharedSpotSymbol and SharedFuturesSymbol models
    * Added BaseAssetType, BaseAssetSubType, QuoteAssetType and QuoteAssetSubType to SharedSpotSymbol and SharedFuturesSymbol models
    * Added DebuggerDisplay attributes to Shared models
    * Updated websocket event handlers to handle all items in array instead of first
    * Fixed Shared websocket order updates labeling filled quantity as base asset instead of contracts

* Version 5.1.0 - 09 Jul 2026
    * Updated CryptoExchange.Net to v12.1.0
    * Added Us environment
    * Added EuropeDemo environment
    * Added CollateralRestricted to OKXDiscountInfo model
    * Added PreviousFundingTime to OKXFundingRate model
    * Added BuyApr, SellApr and Discount to OKXOptionSummary
    * Added PriceLimitPercentage, InitialPriceLimitPercentage, MaxPlatformOpenInterestLimit, MaxPriceLimitCap and UpcomingChanges to OKXInstrument model
    * Added custodyType parameter to restClient.UnifiedApi.Account.GetFundingBillHistoryAsync endpoint
    * Updated FundingBillType enum values
    * Fixed OKXDeliveryExerciseHistory Symbol deserialization
    * Removed NextFundingRate from OKXFundingRate model

* Version 5.0.2 - 30 Jun 2026
    * Fixed symbol name mapping for XPerps in Europe environment

* Version 5.0.1 - 30 Jun 2026
    * Fixed Shared GetFuturesSymbolsAsync failing if underlying type is not set

* Version 5.0.0 - 29 Jun 2026
    * Result types:
      * (Web)CallResult types are replaced by HttpResult, WebSocketResult and QueryResult with the same logic
      * WebSocketResult and QueryResult now return additional info for websocket operations
      * Updated result types to record type
      * Removed implicit result type conversion to bool, `if (result)` no longer works, instead use `if (result.Success)`
      * Fixed result object nullability hinting, for example Data might be null if Success isn't checked for true
    * Clients:
      * Added ToString overrides on base API types
      * Added Exchange property on BaseApiClient
      * Added ApiCredentials property on Api clients
      * Updated ILogger source from client name to topic specific client name
      * Removed logging from client creation
      * Fixed issue in SocketApiClient.GetSocketConnection causing requests to always wait the full max 10 seconds when there was a reconnecting socket
    * Shared APIs:
      * Added missing dedicated option types
      * Added Discover method on ISharedClient interface, returning info on supported capabilities and operations
      * Added ResetStaticExchangeParameters method on ExchangeParameters
      * Added Status property to SharedWithdrawal model
      * Added TradingModes property to SharedBalance model
      * Added support for Europe environment XPerps when using Shared APIs by setting SharedApiEuropeUseXPerps in the client options
      * Updated Shared ExchangeParameters parameter names to be case insensitive
      * Updated code comments
      * Replaced ExchangeResult with ExchangeCallResult type
      * Removed TradingMode from the response model, only maintained on models where it makes sense
      * Fixed futures symbols cache overwriting when using different trading modes
    * Added async streaming on UserDataTracker items with StreamUpdatesAsync
    * Added cancellation token support to UserDataTracker starting
    * Added SupportedEnvironments property to PlatformInfo
    * Added Clear() method on UserClientProvider to clear all cached clients
    * Added setter to OKXExchange.RateLimiter to allow custom rate limit settings
    * Various small performance improvements
    * Fixed websocket connection attempts counting towards rate limit even when server could not be reached
