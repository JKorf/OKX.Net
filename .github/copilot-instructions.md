# Copilot Instructions for OKX.Net

This repository is **OKX.Net**, a strongly typed C#/.NET client library for the OKX cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes OKX.Net, follow these conventions:

## Use OKX.Net, Not Raw HTTP

Never generate raw `HttpClient` calls to OKX REST endpoints or hand-written WebSocket clients. Use `OKXRestClient` or `OKXSocketClient` for signing, passphrase authentication, rate limit integration, reconnects, and typed responses.

## Client Setup

```csharp
using OKX.Net;
using OKX.Net.Clients;

var restClient = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});
```

## Result Handling

Methods return `WebCallResult<T>` for REST and `CallResult<T>` for WebSocket. Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API Structure

- `restClient.UnifiedApi.ExchangeData` - public market data, public info, announcements, trading stats
- `restClient.UnifiedApi.Account` - balances, positions, funding, deposits, withdrawals, leverage
- `restClient.UnifiedApi.Trading` - order placement, amendment, cancellation, history, fills, algo orders
- `restClient.UnifiedApi.SubAccounts` - sub-account management
- `restClient.UnifiedApi.CopyTrading` - copy trading endpoints
- `socketClient.UnifiedApi.ExchangeData` - public streams
- `socketClient.UnifiedApi.Account` - private account/funding streams
- `socketClient.UnifiedApi.Trading` - private trading streams and socket trading requests

## OKX-Specific Rules

- Use `OKXCredentials("key", "secret", "passphrase")`.
- Use `UnifiedApi`, not separate Spot/Futures roots.
- Use OKX symbols such as `BTC-USDT` and `BTC-USDT-SWAP`, not `BTCUSDT`.
- For product-specific calls use `InstrumentType.Spot`, `InstrumentType.Swap`, `InstrumentType.Futures`, or `InstrumentType.Option`.
- For orders use `TradeMode.Cash`, `TradeMode.Cross`, or `TradeMode.Isolated` as appropriate.
- Store WebSocket `UpdateSubscription` values and unsubscribe on shutdown.

## Cross-Exchange

For exchange-agnostic code, use `CryptoExchange.Net.SharedApis` via `.UnifiedApi.SharedClient`. Same pattern works for Binance, Bybit, Kraken, CoinEx, and other CryptoExchange.Net libraries.

## Avoid

- Legacy or guessed API roots such as `SpotApi` or `FuturesApi`
- Generic `ApiCredentials` in OKX examples
- Synchronous `.Result` / `.Wait()`
- Instantiating clients per request
- Manual ticker polling when a WebSocket stream fits
- Reading `.Data` without checking `.Success`

## Reference

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, `llms-full.txt`, and `docs/ai-api-map.md` in the repository root, and `Examples/ai-friendly/` for compilable examples.
