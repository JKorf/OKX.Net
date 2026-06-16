# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package JK.OKX.Net` and it builds.
- **Self-contained** - single file, no shared helpers.
- **Heavily commented** - explains OKX-specific choices like `UnifiedApi`, `TradeMode`, and passphrase credentials.
- **Idiomatic** - follows current OKX.Net 4.x patterns.

## Files

| File | What it shows |
|---|---|
| `01-unified-quickstart.cs` | Client setup, public ticker, authenticated balance, spot limit order, order lookup |
| `02-derivatives.cs` | Swap/futures flow: symbols, leverage, market order, positions, close position |
| `03-websocket.cs` | Public ticker/klines and private order streams with proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `HttpResult` patterns, transient retry, common error metadata |

## Running

```bash
dotnet new console -n MyOKXApp
cd MyOKXApp
dotnet add package JK.OKX.Net
# Copy one example .cs file into Program.cs
# Replace API_KEY / API_SECRET / PASSPHRASE placeholders for private endpoints
dotnet run
```
