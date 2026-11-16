---
name: okx-net
description: Generate and troubleshoot C#/.NET code consuming OKX.Net for Spot, margin, derivatives, Unified accounts, funding, subaccounts, copy trading, REST, WebSocket requests/streams, and Shared API V2 integrations.
---

# OKX.Net

Install the package (its ID differs from the namespace):

```sh
dotnet add package JK.OKX.Net
```

Use `OKXRestClient.UnifiedApi` with `ExchangeData`, `Account`, `Trading`, `SubAccounts`, and `CopyTrading`. `OKXSocketClient.UnifiedApi` groups `ExchangeData`, `Account`, and `Trading`. Spot and derivatives share the Unified root. Reuse and dispose clients, await calls, and retain successful subscriptions for unsubscribe cleanup.

Credentials are `OKXCredentials(key, secret, passphrase)`. Public market data needs no credentials. Match credentials and environment to the intended live, regional, or demo workflow; demo is configured explicitly.

Spot instruments use `ETH-USDT`; swaps use `ETH-USDT-SWAP`. Delivery and options require their actual instrument identifiers. Native socket order requests use numeric `OKXInstrument.SymbolCode` from metadata, not display symbol strings. Derivatives quantities represent contracts; inspect instrument contract value, lot sizes, precision and trading mode before conversion.

Check `Success` before `Data`: REST returns `HttpResult`, socket requests `QueryResult`, subscriptions `WebSocketResult<UpdateSubscription>`, and some Shared helpers `ExchangeCallResult`, with generic variants where applicable. Batch results require checking each nested item.

Use `.SharedApi` and narrow V2 capabilities for portable operations; `IOKXSharedApiClient` groups `Rest` and `Socket`. Use native APIs for account/position modes, detailed algo orders, subaccounts, funding and copy-trading workflows. Account mode, trade mode, margin mode and position side are distinct settings.

## Read when needed

- [API surfaces](references/api-surfaces.md): endpoint routing, socket codes, Shared capabilities, catalogs and quantities.
- [Usage](references/usage.md): setup, authentication, account reads, order checks, subscriptions, Shared API V2 and DI.
- [Operational considerations](references/safety.md): execution, modes, funds movement, batch outcomes and uncertain submissions.

For work on the library itself, use [repository instructions](../../../AGENTS.md) (checkout-only).
