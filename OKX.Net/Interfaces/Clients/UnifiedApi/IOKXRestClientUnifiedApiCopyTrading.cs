using OKX.Net.Objects.CopyTrading;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Copy trading endpoints
/// </summary>
public interface IOKXRestClientUnifiedApiCopyTrading
{
    /// <summary>
    /// Retrieve current account configuration related to copy/lead tradingt.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-get-account-configuration" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/copytrading/config
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCopyTradingAccount>> GetAccountConfigurationAsync(CancellationToken ct = default);

    /// <summary>
    /// Get current leading positions of lead trader.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-get-lead-trader-current-lead-positions" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/copytrading/public-current-subpositions
    /// </para>
    /// </summary>
    /// <param name="uniqueCode">["<c>uniqueCode</c>"] Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters</param>
    /// <param name="instType">["<c>instType</c>"] Instrument type. SWAP, the default value.</param>
    /// <param name="after">["<c>after</c>"] Pagination of data to return records earlier than the requested PositionId.</param>
    /// <param name="before">["<c>before</c>"] Pagination of data to return records newer than the requested PositionId.</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCurrentSubposition[]>> GetLeadTraderCurrentLeadPositionsAsync(string uniqueCode, string instType = "SWAP", string? after = null, string? before = null, int limit = 100,  CancellationToken ct = default);

    /// <summary>
    /// Retrieve lead positions that are not closed.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-get-existing-lead-positions" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/copytrading/current-subpositions
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Instrument ID</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="after">["<c>after</c>"] Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">["<c>before</c>"] Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. Maximum is 500. Default is 500.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCurrentSubposition[]>> GetLeadPositionsAsync(string? symbol = null, Enums.InstrumentType? instrumentType = null, string? after = null, string? before = null, int limit = 500, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the completed lead position of the last 3 months.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-get-lead-position-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/copytrading/subpositions-history
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Instrument ID</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="after">["<c>after</c>"] Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">["<c>before</c>"] Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSubpositionHistory[]>> GetLeadPositionHistoryAsync(string? symbol = null, Enums.InstrumentType? instrumentType = null, string? after = null, string? before = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// The leading trader sets TP/SL for the current leading position that are not closed.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-post-place-lead-stop-order" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/copytrading/algo-order
    /// </para>
    /// </summary>
    /// <param name="subPositionId">["<c>subPosId</c>"] Lead position ID</param>
    /// <param name="takeProfitTriggerPrice">["<c>tpTriggerPx</c>"] Take-profit trigger price</param>
    /// <param name="takeProfitOrderPrice">["<c>tpOrdPx</c>"] Take-profit order price. -1 for market price</param>
    /// <param name="stopLossTriggerPrice">["<c>slTriggerPx</c>"] Stop-loss trigger price</param>
    /// <param name="stopLossOrderPrice">["<c>slOrdPx</c>"] Stop-loss order price. -1 for market price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCopyTradingActionResponse>> PlaceLeadStopOrderAsync(string subPositionId, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, CancellationToken ct = default);

    /// <summary>
    /// Close a lead position
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-post-close-lead-position" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/copytrading/close-subposition
    /// </para>
    /// </summary>
    /// <param name="subPositionId">["<c>subPosId</c>"] Lead position ID</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXCopyTradingActionResponse>> CloseLeadPositionAsync(string subPositionId, Enums.InstrumentType? instrumentType = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve instruments that are supported to lead by the platform and the lead trader has set.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-get-leading-instruments" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/copytrading/instruments
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLeadingInstrument[]>> GetLeadingInstrumentsAsync(Enums.InstrumentType? instrumentType = null, CancellationToken ct = default);

    /// <summary>
    /// Amend leading instruments
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-copy-trading-post-amend-leading-instruments" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/copytrading/set-instruments
    /// </para>
    /// </summary>
    /// <param name="symbols">["<c>instId</c>"] Instrument IDs to set as leading instruments, separated by comma</param>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLeadingInstrument[]>> AmendLeadingInstrumentsAsync(IEnumerable<string> symbols, Enums.InstrumentType? instrumentType = null, CancellationToken ct = default);
}

