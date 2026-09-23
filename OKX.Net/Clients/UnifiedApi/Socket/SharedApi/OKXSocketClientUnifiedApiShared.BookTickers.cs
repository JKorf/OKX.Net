using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXSocketClientUnifiedSharedApi
    {

        #region Subscribe Book Ticker

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.ExchangeData.SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedBookTicker(
                    ExchangeSymbolCache.ParseSymbol(request.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, _api.EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol,
                    update.Data.BestAskPrice ?? 0,
                    new SharedOrderQuantity(
                    request.TradingMode == TradingMode.Spot ? update.Data.BestAskQuantity : null,
                    null,
                    request.TradingMode != TradingMode.Spot ? update.Data.BestAskQuantity : null),
                    update.Data.BestBidPrice ?? 0,
                    new SharedOrderQuantity(
                        request.TradingMode == TradingMode.Spot ? update.Data.BestBidQuantity : null,
                        null,
                        request.TradingMode != TradingMode.Spot ? update.Data.BestBidQuantity : null)))), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
