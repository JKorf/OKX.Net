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
        #region Open Interest client

        public GetOpenInterestOptions GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, false);
        public async Task<HttpResult<SharedOpenInterest>> GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var instrumentType = request.Symbol!.TradingMode.IsPerpetual() ? InstrumentType.Swap : InstrumentType.Futures;
            var europeXPerps = _api.EnvironmentName == OKXEnvironment.Europe.Name && _api.ClientOptions.SharedApiEuropeUseXPerps;
            if (europeXPerps && request.Symbol.TradingMode.IsPerpetual())
                // XPerps are categorized under futures
                instrumentType = InstrumentType.Futures;

            var result = await _api.ExchangeData.GetOpenInterestsAsync(instrumentType, symbol: request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            return HttpResult.Ok(result, new SharedOpenInterest(new SharedOrderQuantity(contractQuantity: result.Data.First().OpenInterest ?? 0)));
        }

        #endregion
    }
}
