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

    public IOKXRestClientUnifiedApiShared SharedClient => this;

    internal OKXRestClientUnifiedApi(ILogger logger, HttpClient? httpClient, OKXRestOptions options)
            : base(logger, OKXExchange.Metadata.Id, httpClient, options.Environment.RestAddress, options, options.UnifiedOptions)
    {
        Account = new OKXRestClientUnifiedApiAccount(this);
        ExchangeData = new OKXRestClientUnifiedApiExchangeData(this);
        Trading = new OKXRestClientUnifiedApiTrading(this);
        SubAccounts = new OKXRestClientUnifiedApiSubAccounts(this);
        CopyTrading = new OKXRestClientUnifiedApiCopyTrading(this);

        if (options.Environment.Name == TradeEnvironmentNames.Testnet)
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
        => OKXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

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

    /// <inheritdoc />
    protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
        => ExchangeData.GetServerTimeAsync();

}
