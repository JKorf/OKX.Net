using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Clients.MessageHandlers;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Options;

namespace OKX.Net.Clients.UnifiedApi;

internal partial class OKXRestClientUnifiedApi : RestApiClient<OKXEnvironment, OKXAuthenticationProvider, OKXCredentials>, IOKXRestClientUnifiedApi
{
    #region Internal Fields
    private readonly OKXRestClientUnifiedSharedApi _sharedApi;
    public new OKXRestOptions ClientOptions => (OKXRestOptions)base.ClientOptions;

    protected override IRestMessageHandler MessageHandler { get; } = new OKXRestMessageHandler(OKXErrors.ErrorMapping);
    protected override ErrorMapping ErrorMapping => OKXErrors.ErrorMapping;
    #endregion

    public IOKXRestClientUnifiedApiAccount Account { get; private set; }
    public IOKXRestClientUnifiedApiExchangeData ExchangeData { get; private set; }
    public IOKXRestClientUnifiedApiTrading Trading { get; private set; }
    public IOKXRestClientUnifiedApiSubAccounts SubAccounts { get; private set; }
    public IOKXRestClientUnifiedApiCopyTrading CopyTrading { get; private set; }

    public string ExchangeName => "OKX";

    public IOKXRestClientUnifiedApiShared SharedClient => _sharedApi;
    public IOKXRestClientUnifiedSharedApi SharedApi => _sharedApi;

    internal OKXRestClientUnifiedApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, OKXRestOptions options)
            : base(loggerFactory, OKXExchange.Metadata.Id, httpClient, options.Environment.RestAddress, options, options.UnifiedOptions)
    {
        Account = new OKXRestClientUnifiedApiAccount(this);
        ExchangeData = new OKXRestClientUnifiedApiExchangeData(this);
        Trading = new OKXRestClientUnifiedApiTrading(this);
        SubAccounts = new OKXRestClientUnifiedApiSubAccounts(this);
        CopyTrading = new OKXRestClientUnifiedApiCopyTrading(this);

        _sharedApi = new OKXRestClientUnifiedSharedApi(this);

        if (options.Environment.Name == TradeEnvironmentNames.Testnet
            || options.Environment.Name == OKXEnvironment.EuropeDemo.Name
            || options.Environment.Name == OKXEnvironment.UsDemo.Name)
        {
            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "x-simulated-trading", "1" }
            };
        }
    }

    /// <inheritdoc />
    protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(OKXExchange._serializerContext));

    /// <inheritdoc />
    protected override OKXAuthenticationProvider CreateAuthenticationProvider(OKXCredentials credentials)
        => new OKXAuthenticationProvider(credentials);

    /// <inheritdoc />
    public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
    {
        if (EnvironmentName == OKXEnvironment.Europe.Name)
            return OKXExchange.FormatSymbolEurope(baseAsset, quoteAsset, tradingMode, deliverTime);
        else
            return OKXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }

    internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
    {
        var result = await base.SendAsync<OKXRestApiResponse<T>>(definition, parameters, cancellationToken, requestHeaders, weight, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
        if (!result.Success) return HttpResult.Fail<T>(result);
        if (result.Data.ErrorCode > 0) return HttpResult.Fail<T>(result, new ServerError(result.Data.ErrorCode, GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));

        return HttpResult.Ok(result, result.Data.Data!);
    }

    internal async Task<HttpResult<T>> SendRawAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
    {
        return await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
    }

    internal async Task<HttpResult<T>> SendGetSingleAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
    {
        var result = await SendAsync<T[]>(definition, parameters, cancellationToken, weight, requestHeaders, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<T>(result);

        if (!result.Data.Any())
            return HttpResult.Fail<T>(result, new ServerError(ErrorInfo.Unknown with { Message = "No response data" }));

        return HttpResult.Ok(result, result.Data.First());
    }

    /// <summary>
    /// Sends a single-order request and returns the rejected order with its error when available.
    /// </summary>
    internal async Task<HttpResult<T>> SendGetSingleOrderAsync<T>(
        RequestDefinition definition,
        Parameters? parameters,
        CancellationToken cancellationToken,
        string? rateLimitKeySuffix = null) where T : OKXOperationResponse
    {
        var result = await base.SendAsync<OKXRestApiResponse<T[]>>(definition, parameters, cancellationToken, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<T>(result);

        var response = result.Data;
        var orders = response.Data;
        var order = orders != null && orders.Length > 0 ? orders[0] : null;
        
        // The order result can explain a generic failure reported by envelope code 1.
        if ((response.ErrorCode == 0 || response.ErrorCode == 1) && order != null)
        {
            var code = order.Code;
            if (!string.IsNullOrEmpty(code) && code != OKXOperationResponse.SuccessCode)
                return HttpResult.Fail<T>(result, new ServerError(code, GetErrorInfo(code, order.Message)), order);
        }

        if (response.ErrorCode != 0)
            return HttpResult.Fail<T>(result, new ServerError(response.ErrorCode, GetErrorInfo(response.ErrorCode, response.ErrorMessage)));

        if (order == null)
            return HttpResult.Fail<T>(result, new ServerError(ErrorInfo.Unknown) { Message = "No response data" });

        return HttpResult.Ok(result, order);
    }

    /// <inheritdoc />
    protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
        => ExchangeData.GetServerTimeAsync();

}
