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

        #region Get Withdrawal History

        async Task<ICallResult<SharedWithdrawal[]>> IGetWithdrawalHistory.GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetWithdrawalHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawalHistoryAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Time)),
                         result.Data.Length,
                         result.Data.Select(x => x.Time),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedWithdrawal(
                        x.Asset,
                        x.To,
                        x.Quantity, 
                        x.State == WithdrawalState.Success, 
                        x.Time,
                        GetWithdrawalStatus(x))
                    {
                        Network = x.Network,
                        TransactionId = x.TransactionId,
                        Fee = x.Fee
                    })
                .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus GetWithdrawalStatus(OKXWithdrawalHistory x)
        {
            if (x.State == WithdrawalState.Canceled
                || x.State == WithdrawalState.Failed
                || x.State == WithdrawalState.InsufficientHotWalletBalance)
            {
                return SharedTransferStatus.Failed;
            }

            if (x.State == WithdrawalState.Success)
                return SharedTransferStatus.Completed;

            if (x.State == WithdrawalState.Approved
                || x.State == WithdrawalState.AwaitingManualReview
                || x.State == WithdrawalState.AwaitingTransfer
                || x.State == WithdrawalState.Canceling
                || x.State == WithdrawalState.Pending
                || x.State == WithdrawalState.PendingTransactionValidation
                || x.State == WithdrawalState.PendingTravelRule
                || x.State == WithdrawalState.Withdrawing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #region Withdraw

        async Task<ICallResult<SharedId>> IWithdraw.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
            => await WithdrawAsync(request, ct).ConfigureAwait(false);

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName) {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                ExchangeParameterDescription.Required(
                    "withdrawFee",
                    aliases: ["fee"],
                    description: "Fee to use for the withdrawal",
                    exampleValue: 0.001m)
            }
        };

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var target = request.Address;
            if (request.AddressTag != null)
                target += ":" + request.AddressTag;

            var fee = request.GetParamValue<decimal>(Exchange, "withdrawFee", "fee");

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                request.Quantity,
                WithdrawalDestination.DigitalCurrencyAddress,
                target,
                fee,
                network: request.Network,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.WithdrawalId));
        }

        #endregion

    }
}
