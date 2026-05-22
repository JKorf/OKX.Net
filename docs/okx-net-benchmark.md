# OKX.Net vs CCXT and OKX.Api

This document compares measured `OKX.Net`, `OKX.Net` shared interfaces, `CCXT`, and `OKX.Api` performance for C#/.NET users building against OKX REST and WebSocket APIs.

The results show `OKX.Net` as the clear winner. It has the strongest REST allocation profile in every measured scenario, leads the direct ticker and server-time workloads, and has a large WebSocket trade-ingestion advantage over both CCXT and `OKX.Api`.

The shared-interface results are especially useful when comparing against CCXT's unified API style. `OKX.Net` shared clients keep most of the performance of the direct OKX.Net clients while still exposing exchange-agnostic abstractions.

## Benchmark source

The benchmark source is available in `Benchmark/Client/Program.cs`. It contains the direct OKX.Net benchmarks, shared-interface benchmarks, CCXT benchmarks, and `OKX.Api` benchmarks in one BenchmarkDotNet project: `Benchmark/Client/OKX.Net.Benchmark.Client.csproj`.

The benchmarks run against the local mock OKX server in `Benchmark/Server`, including the REST and WebSocket mock endpoints in `Benchmark/Server/Controllers/RestController.cs` and `Benchmark/Server/Controllers/WsController.cs`.

## Summary

| Area | Result |
|---|---|
| REST server time on .NET 10 | `OKX.Net` has the lowest allocation profile; CCXT is close on mean but allocates 3.05x more memory, and `OKX.Api` is 1.21x slower |
| REST server time on .NET Framework 4.8 | `OKX.Net` is fastest; CCXT is 1.09x slower and `OKX.Api` is 1.33x slower |
| REST ticker on .NET 10 | Direct `OKX.Net` is fastest; shared `OKX.Net` is close and allocates far less than CCXT and `OKX.Api` |
| REST ticker on .NET Framework 4.8 | Direct and shared `OKX.Net` are fastest and keep allocations low |
| WebSocket trade stream on .NET 10 | `OKX.Net` is fastest; shared `OKX.Net` is 1.06x slower, `OKX.Api` is 3.47x slower, and CCXT is 7.97x slower |
| WebSocket trade stream on .NET Framework 4.8 | `OKX.Net` is fastest; shared `OKX.Net` is 1.02x slower, `OKX.Api` is 2.09x slower, and CCXT is 3.91x slower |
| Unified/shared API comparison | `OKX.Net` shared interfaces outperform CCXT unified results by a wide margin |
| Allocation profile | Direct `OKX.Net` allocates the least memory in every measured scenario; shared `OKX.Net` remains close |

The REST results show that `OKX.Net` has very low request/response overhead and consistently lower memory usage. The WebSocket results show a much larger difference under sustained message processing, where parsing, dispatch, and allocation behavior dominate.

## REST comparison

| Method | Runtime | Mean | Error | StdDev | Ratio | RatioSD | Gen0 | Gen1 | Allocated | Alloc Ratio |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| OKXNet_Ticker | .NET 10.0 | 113.5 us | 1.97 us | 2.41 us | 0.95 | 0.08 | 0.4883 | - | 7.64 KB | 1.26 |
| CCXT_ServerTime | .NET 10.0 | 114.3 us | 5.29 us | 7.07 us | 0.95 | 0.09 | 1.4648 | - | 18.49 KB | 3.05 |
| OKXNetShared_Ticker | .NET 10.0 | 118.7 us | 1.64 us | 2.13 us | 0.99 | 0.08 | 0.4883 | - | 8.53 KB | 1.41 |
| OKXNet_ServerTime | .NET 10.0 | 120.5 us | 7.34 us | 9.80 us | 1.01 | 0.11 | 0.4883 | - | 6.05 KB | 1.00 |
| CCXT_Ticker | .NET 10.0 | 130.4 us | 1.75 us | 2.22 us | 1.09 | 0.09 | 1.9531 | - | 27.22 KB | 4.50 |
| OKXApi_ServerTime | .NET 10.0 | 145.1 us | 1.81 us | 2.42 us | 1.21 | 0.10 | 1.9531 | 0.4883 | 23.69 KB | 3.91 |
| OKXApi_Ticker | .NET 10.0 | 165.4 us | 2.77 us | 3.60 us | 1.38 | 0.11 | 3.4180 | 0.4883 | 42.08 KB | 6.95 |
| OKXNet_ServerTime | .NET Framework 4.8 | 173.5 us | 1.91 us | 2.55 us | 1.00 | 0.02 | 3.1738 | - | 20.04 KB | 1.00 |
| CCXT_ServerTime | .NET Framework 4.8 | 189.0 us | 1.61 us | 2.09 us | 1.09 | 0.02 | 6.5918 | 0.2441 | 40.84 KB | 2.04 |
| OKXNet_Ticker | .NET Framework 4.8 | 195.3 us | 1.47 us | 1.96 us | 1.13 | 0.02 | 3.4180 | - | 21.11 KB | 1.05 |
| OKXNetShared_Ticker | .NET Framework 4.8 | 196.4 us | 1.37 us | 1.78 us | 1.13 | 0.02 | 3.6621 | - | 22.66 KB | 1.13 |
| CCXT_Ticker | .NET Framework 4.8 | 206.5 us | 2.30 us | 2.65 us | 1.19 | 0.02 | 7.8125 | 0.2441 | 48.78 KB | 2.43 |
| OKXApi_ServerTime | .NET Framework 4.8 | 230.6 us | 1.00 us | 1.23 us | 1.33 | 0.02 | 6.5918 | 0.7324 | 41.85 KB | 2.09 |
| OKXApi_Ticker | .NET Framework 4.8 | 273.7 us | 2.45 us | 3.27 us | 1.58 | 0.03 | 9.7656 | 0.9766 | 62.41 KB | 3.12 |

### REST findings

For server-time requests, `OKX.Net` has the best allocation profile and leads clearly on .NET Framework 4.8:

- .NET 10: `OKX.Net` allocates 6.05 KB, compared with 18.49 KB for CCXT and 23.69 KB for `OKX.Api`. CCXT is close on mean time at 114.3 us, but the error ranges overlap with `OKX.Net`.
- .NET Framework 4.8: `OKX.Net` completes in 173.5 us and allocates 20.04 KB, compared with 189.0 us and 40.84 KB for CCXT, and 230.6 us and 41.85 KB for `OKX.Api`.

For ticker requests, direct `OKX.Net` is fastest on both runtimes and has the lowest allocation profile:

- .NET 10: direct `OKX.Net` completes in 113.5 us and allocates 7.64 KB. Shared `OKX.Net` completes in 118.7 us and allocates 8.53 KB. CCXT takes 130.4 us and allocates 27.22 KB, while `OKX.Api` takes 165.4 us and allocates 42.08 KB.
- .NET Framework 4.8: direct `OKX.Net` completes in 195.3 us and allocates 21.11 KB. Shared `OKX.Net` completes in 196.4 us and allocates 22.66 KB. CCXT takes 206.5 us and allocates 48.78 KB, while `OKX.Api` takes 273.7 us and allocates 62.41 KB.

The shared-interface ticker result is the most direct REST comparison with CCXT's unified approach. On .NET 10, shared `OKX.Net` is faster than CCXT and allocates about 69% less memory. On .NET Framework 4.8, shared `OKX.Net` is faster than CCXT and allocates about 54% less memory.

## WebSocket comparison

| Method | Runtime | Mean | Error | StdDev | Ratio | RatioSD | Gen0 | Gen1 | Gen2 | Allocated | Alloc Ratio |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| OKXNet_Trades | .NET 10.0 | 190.0 ms | 4.92 ms | 6.40 ms | 1.00 | 0.05 | 10000.0000 | - | - | 122.7 MB | 1.00 |
| OKXNetShared_Trades | .NET 10.0 | 201.2 ms | 3.22 ms | 4.30 ms | 1.06 | 0.04 | 12000.0000 | - | - | 147.95 MB | 1.21 |
| OKXApi_Trades | .NET 10.0 | 659.1 ms | 9.63 ms | 12.85 ms | 3.47 | 0.13 | 144000.0000 | 3000.0000 | - | 1734.95 MB | 14.14 |
| CCXT_Trades | .NET 10.0 | 1,511.5 ms | 97.30 ms | 129.89 ms | 7.97 | 0.72 | 359000.0000 | 59000.0000 | 1000.0000 | 4262.37 MB | 34.74 |
| OKXNet_Trades | .NET Framework 4.8 | 893.6 ms | 8.54 ms | 11.40 ms | 1.00 | 0.02 | 37000.0000 | - | - | 227.04 MB | 1.00 |
| OKXNetShared_Trades | .NET Framework 4.8 | 914.3 ms | 15.36 ms | 20.50 ms | 1.02 | 0.03 | 41000.0000 | - | - | 249.58 MB | 1.10 |
| OKXApi_Trades | .NET Framework 4.8 | 1,869.7 ms | 14.45 ms | 18.78 ms | 2.09 | 0.03 | 327000.0000 | 1000.0000 | - | 1969.06 MB | 8.67 |
| CCXT_Trades | .NET Framework 4.8 | 3,490.7 ms | 35.09 ms | 45.63 ms | 3.91 | 0.07 | 801000.0000 | 201000.0000 | 7000.0000 | 4776.85 MB | 21.04 |

### WebSocket findings

The WebSocket benchmark is the clearest result. On .NET 10, direct `OKX.Net` completes the trade benchmark in 190.0 ms, and shared `OKX.Net` completes in 201.2 ms. `OKX.Api` takes 659.1 ms, and CCXT takes 1,511.5 ms. That makes `OKX.Api` 3.47x slower and CCXT 7.97x slower than the direct OKX.Net socket path.

The allocation difference is even larger. On .NET 10, direct `OKX.Net` allocates 122.7 MB and shared `OKX.Net` allocates 147.95 MB, while `OKX.Api` allocates 1,734.95 MB and CCXT allocates 4,262.37 MB. On .NET Framework 4.8, direct `OKX.Net` allocates 227.04 MB and shared `OKX.Net` allocates 249.58 MB, while `OKX.Api` allocates 1,969.06 MB and CCXT allocates 4,776.85 MB.

For market-data consumers, this is the most important part of the comparison. WebSocket workloads run continuously, often across many symbols. Higher allocation pressure increases GC activity, and slower dispatch reduces headroom for strategy logic, persistence, aggregation, and downstream event processing.

The shared-interface socket result is important for multi-exchange designs. It shows that the exchange-agnostic OKX.Net path remains close to the direct path, while CCXT's unified socket path is much slower and allocates dramatically more memory in this benchmark.
