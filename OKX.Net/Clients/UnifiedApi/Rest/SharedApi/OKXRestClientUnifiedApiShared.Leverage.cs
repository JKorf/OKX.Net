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
        #region Get Leverage

        async Task<ICallResult<SharedLeverage>> IGetLeverage.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
            => await GetLeverageAsync(request, ct).ConfigureAwait(false);

        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSide;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true)
        {
            ParameterRuleOverwrites = [
                RequestParameterRuleOverride<GetLeverageRequest>.Required(x => x.MarginMode),
            ]
        };
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetLeverageAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            var side = request.PositionSide == null ? result.Data.First() : result.Data.FirstOrDefault(d => d.PositionSide == (request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long));
            if (side == null)
                return HttpResult.Fail<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.NoPosition, "Position not found")));
            
            return HttpResult.Ok(result, new SharedLeverage(side.Leverage ?? 0)
            {
                MarginMode = side.MarginMode == MarginMode.Isolated ? SharedMarginMode.Isolated : SharedMarginMode.Cross,
                Side = side.PositionSide == PositionSide.Net ? null : side.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short
            });
        }

        #endregion

        #region Set Leverage

        async Task<ICallResult<SharedLeverage>> ISetLeverage.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
            => await SetLeverageAsync(request, ct).ConfigureAwait(false);

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName) {
            ParameterRuleOverwrites = [
                RequestParameterRuleOverride<SetLeverageRequest>.Required(x => x.MarginMode),
            ]
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.SetLeverageAsync(
                (int)request.Leverage,
                request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                positionSide: request.Side == null ? null : request.Side == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(result.Data.First().Leverage ?? 0)
            {
                Side = request.Side
            });
        }

        #endregion
    }
}
