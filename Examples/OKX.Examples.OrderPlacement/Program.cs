using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Enums;

const string spotSymbol = "BTC-USDT";
const string futuresSymbol = "ETH-USDT-SWAP";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";
var passphrase = "PASS";

Console.WriteLine("OKX.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXCredentials(apiKey, apiSecret, passphrase);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(OKXRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.UnifiedApi.ExchangeData.GetTickerAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    if (ticker.Data.LastPrice == null)
    {
        Console.WriteLine("Failed to get spot ticker: last price was not available");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice.Value * 0.95m, 2);
    var order = await client.UnifiedApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: OrderType.Limit,
        quantity: 0.001m,
        price: safePrice,
        tradeMode: TradeMode.Cash);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId}");

    var orderStatus = await client.UnifiedApi.Trading.GetOrderDetailsAsync(spotSymbol, order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.OrderState}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.UnifiedApi.Trading.CancelOrderAsync(spotSymbol, order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesReduceOnlyOrderExampleAsync(OKXRestClient client)
{
    Console.WriteLine($"Placing futures reduce-only limit sell order for {futuresSymbol}...");

    var ticker = await client.UnifiedApi.ExchangeData.GetTickerAsync(futuresSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {ticker.Error}");
        return;
    }

    if (ticker.Data.LastPrice == null)
    {
        Console.WriteLine("Failed to get futures ticker: last price was not available");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice.Value * 1.05m, 2);
    var order = await client.UnifiedApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        side: OrderSide.Sell,
        type: OrderType.Limit,
        quantity: 0.01m,
        price: safePrice,
        tradeMode: TradeMode.Cross,
        reduceOnly: true);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.OrderId}");

    var orderStatus = await client.UnifiedApi.Trading.GetOrderDetailsAsync(futuresSymbol, order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.OrderState}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.UnifiedApi.Trading.CancelOrderAsync(futuresSymbol, order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.OrderId}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
