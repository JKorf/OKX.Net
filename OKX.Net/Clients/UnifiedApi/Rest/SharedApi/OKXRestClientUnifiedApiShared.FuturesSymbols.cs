using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Funding;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXRestClientUnifiedSharedApi
    {
        #region Futures Symbol client

        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicFuturesId, _api.EnvironmentName, null);
        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            // For Europe environment there are 2 different types of perp contracts
            //   1. Swaps, do not appear to be tradable, but still returned by the server
            //   2. XPerps, which are returned under InstrumentType.Futures with a delivery time of 5 years
            // Which one we return is determined by the configuration

            var europeXPerps = _api.EnvironmentName == OKXEnvironment.Europe.Name && _api.ClientOptions.SharedApiEuropeUseXPerps;

            IEnumerable<OKXInstrument> data;
            HttpResult<OKXInstrument[]> result;
            if (request.TradingMode == null)
            {
                // No trading mode filter, request both Swap (perps) and Futures (delivery)
                var request1 = _api.ExchangeData.GetSymbolsAsync(InstrumentType.Swap, ct: ct);
                var request2 = _api.ExchangeData.GetSymbolsAsync(InstrumentType.Futures, ct: ct);
                await Task.WhenAll(request1, request2).ConfigureAwait(false);
                if (!request1.Result.Success)
                    return HttpResult.Fail<SharedFuturesSymbol[]>(request1.Result);
                if (!request2.Result.Success)
                    return HttpResult.Fail<SharedFuturesSymbol[]>(request2.Result);

                result = request1.Result;
                data = request1.Result.Data.Concat(request2.Result.Data);
            }
            else
            {
                InstrumentType requestType;
                if (europeXPerps)
                    requestType = InstrumentType.Futures; // Europe XPerps are returned as futures with 5y delivery
                else
                    requestType = (!request.TradingMode.HasValue || request.TradingMode == TradingMode.PerpetualLinear || request.TradingMode == TradingMode.PerpetualInverse) ? InstrumentType.Swap : InstrumentType.Futures;

                result = await _api.ExchangeData.GetSymbolsAsync(requestType, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedFuturesSymbol[]>(result);

                data = result.Data;
            }

            var resultData =
                 data
                .Select(x => ParseFuturesSymbol(x)!)
                .Where(x => x != null)
                .ToArray();

            foreach (var distinctMode in resultData.GroupBy(x => x.TradingMode))
                ExchangeSymbolCache.UpdateSymbolInfo(_topicFuturesId, _api.EnvironmentName, distinctMode.Key.ToString(), distinctMode!.ToArray());
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(resultData, request));
        }

        private SharedFuturesSymbol? ParseFuturesSymbol(OKXInstrument x)
        {
            var underlyingParts = x.Underlying.Split('-');
            if (underlyingParts.Length != 2)
                return null!;

            var result = new SharedFuturesSymbol(
                x.InstrumentType == InstrumentType.Swap
                ? (x.ContractType == ContractType.Linear ? TradingMode.PerpetualLinear : TradingMode.PerpetualInverse)
                : (x.ContractType == ContractType.Linear ? (x.RuleType == SymbolRuleType.Perp ? TradingMode.PerpetualLinear : TradingMode.DeliveryLinear) : (x.RuleType == SymbolRuleType.Perp ? TradingMode.PerpetualInverse : TradingMode.DeliveryInverse)),
                    underlyingParts[0],
                    underlyingParts[1],
                    x.Symbol,
                    x.State == InstrumentState.Live)
            {
                ContractSize = x.ContractValue,
                DeliveryTime = x.ExpiryTime,
                MaxTradeQuantity = x.MaxLimitQuantity,
                MinTradeQuantity = x.MinimumOrderSize,
                PriceStep = x.TickSize,
                QuantityStep = x.LotSize,
                DisplayName = x.Symbol,
                MaxLongLeverage = x.MaximumLeverage,
                MaxShortLeverage = x.MaximumLeverage,
                BaseAssetType = SharedAssetType.Crypto,
                UpperPriceLimitPercentage = x.PriceLimitPercentage * 100,
                LowerPriceLimitPercentage = -x.PriceLimitPercentage * 100
            };

            if (x.SymbolCategory == SymbolCategory.Stocks
                || x.SymbolCategory == SymbolCategory.Bonds)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Equity;
            }
            else if (x.SymbolCategory == SymbolCategory.Commodities)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Commodity;
            }
            else if (x.SymbolCategory == SymbolCategory.Forex)
            {
                result.BaseAssetType = SharedAssetType.Fiat;
            }
            else if (x.SymbolCategory == SymbolCategory.Crypto)
            {
                result.BaseAssetType = SharedAssetType.Crypto;
            }

            if (LibraryHelpers.IsStableCoin(result.QuoteAsset))
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicFuturesId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, _api.EnvironmentName, null, symbolName));
        }
        #endregion
    }
}
