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
        #region Balance client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, 
            AccountTypeFilter.Funding, AccountTypeFilter.Spot, AccountTypeFilter.Futures, AccountTypeFilter.Margin, AccountTypeFilter.Option);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType != SharedAccountType.Funding)
            {
                var result = await _api.Account.GetAccountBalanceAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Details.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset, 
                        x.AvailableBalance ?? 0, 
                        x.Equity ?? 0)).ToArray());
            }
            else
            {
                var result = await _api.Account.GetFundingBalanceAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedBalance(
                        [], 
                        x.Asset,
                        x.Available,
                        x.Balance)).ToArray());
            }
        }

        #endregion
    }
}
