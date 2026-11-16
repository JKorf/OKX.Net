# OKX.Net examples

Each file is an independent console program. Install `JK.OKX.Net` in a console project and copy one program to `Program.cs`.

| Program | Workflow |
|---|---|
| [01-unified-quickstart.cs](01-unified-quickstart.cs) | Public market data, account reads and Spot trading |
| [02-derivatives.cs](02-derivatives.cs) | Swap instruments, leverage, orders, positions and close workflow |
| [03-websocket.cs](03-websocket.cs) | Public/private subscriptions and cleanup |
| [04-multi-exchange.cs](04-multi-exchange.cs) | Shared API V2 ticker, catalog and subscription |
| [05-error-handling.cs](05-error-handling.cs) | Results and bounded public-read retry |

Replace key, secret and passphrase placeholders for intended private operations. Review each program before execution: trading/leverage/close calls change account state, and limit orders may fill before cancellation. Read [operational considerations](../../.claude/skills/okx-net/references/safety.md) and use the [usage snippets](../../.claude/skills/okx-net/references/usage.md) for focused patterns.

`AiExampleCompileTests` compiles these programs without executing exchange operations.
