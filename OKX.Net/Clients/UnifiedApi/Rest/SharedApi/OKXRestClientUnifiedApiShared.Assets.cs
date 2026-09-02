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
        #region Asset client

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.Account.GetAssetsAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            if (!assets.Data.Any())
                return HttpResult.Fail<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(request.Asset)
            {
                FullName = assets.Data.First().Name,
                Networks = assets.Data.Select(x => new SharedAssetNetwork(x.Network)
                {
                    MinConfirmations = x.MinDepositConfirmations,
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinimumWithdrawalAmount,
                    WithdrawEnabled = x.AllowWithdrawal,
                    WithdrawFee = x.MinimumWithdrawalFee,
                    ContractAddress = x.ContractAddress,
                }).ToArray()
            });
        }

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.Account.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.GroupBy(x => x.Asset).Select(x => new SharedAsset(x.Key)
            {
                FullName = x.First().Name,
                Networks = x.Select(x => new SharedAssetNetwork(x.Network)
                {
                    MinConfirmations = x.MinDepositConfirmations,
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinimumWithdrawalAmount,
                    WithdrawEnabled = x.AllowWithdrawal,
                    WithdrawFee = x.MinimumWithdrawalFee,
                    ContractAddress = x.ContractAddress,
                }).ToArray()
            }).ToArray());
        }

        #endregion
    }
}
