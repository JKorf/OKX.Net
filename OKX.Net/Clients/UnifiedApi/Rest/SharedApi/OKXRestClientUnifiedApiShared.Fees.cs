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
        #region Get Fees

        async Task<ICallResult<SharedFee>> IGetFees.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
            => await GetFeesAsync(request, ct).ConfigureAwait(false);

        public GetFeeOptions GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        public async Task<HttpResult<SharedFee>> GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var type = request.Symbol!.TradingMode == TradingMode.Spot ? InstrumentType.Spot : request.Symbol.TradingMode == TradingMode.PerpetualLinear || request.Symbol.TradingMode == TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await _api.Account.GetFeeRatesAsync(type,
                type == InstrumentType.Spot ? request.Symbol!.GetSymbol(FormatSymbol) : null,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(Math.Abs(result.Data.Maker ?? 0) * 100, Math.Abs(result.Data.Taker ?? 0) * 100));
        }

        #endregion
    }
}
