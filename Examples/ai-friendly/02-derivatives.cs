using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Enums;

var client = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});

const string swapSymbol = "ETH-USDT-SWAP";

// Derivatives instruments are still under UnifiedApi. Use InstrumentType to
// select swap/futures/options public data, and use TradeMode/PositionSide on orders.
var symbols = await client.UnifiedApi.ExchangeData.GetSymbolsAsync(
    InstrumentType.Swap,
    symbol: swapSymbol);

if (!symbols.Success)
{
    Console.WriteLine($"Symbol lookup failed: {symbols.Error}");
    return;
}

Console.WriteLine($"Found {symbols.Data.Length} matching swap instrument(s).");

var leverage = await client.UnifiedApi.Account.SetLeverageAsync(
    leverage: 5,
    marginMode: MarginMode.Cross,
    symbol: swapSymbol,
    positionSide: PositionSide.Long);

if (!leverage.Success)
{
    Console.WriteLine($"Leverage update failed: {leverage.Error}");
    return;
}

var order = await client.UnifiedApi.Trading.PlaceOrderAsync(
    symbol: swapSymbol,
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: 1m,
    positionSide: PositionSide.Long,
    tradeMode: TradeMode.Cross);

if (!order.Success)
{
    Console.WriteLine($"Swap order failed: {order.Error}");
    return;
}

Console.WriteLine($"Placed swap order id: {order.Data.OrderId}");

var positions = await client.UnifiedApi.Account.GetPositionsAsync(
    InstrumentType.Swap,
    symbol: swapSymbol);

if (!positions.Success)
{
    Console.WriteLine($"Position request failed: {positions.Error}");
    return;
}

foreach (var position in positions.Data)
    Console.WriteLine($"{position.Symbol} {position.PositionSide}: {position.PositionsQuantity}");

var close = await client.UnifiedApi.Trading.ClosePositionAsync(
    symbol: swapSymbol,
    marginMode: MarginMode.Cross,
    positionSide: PositionSide.Long);

if (!close.Success)
{
    Console.WriteLine($"Close position failed: {close.Error}");
    return;
}

Console.WriteLine($"Close position request accepted: {close.Data.Symbol}");
