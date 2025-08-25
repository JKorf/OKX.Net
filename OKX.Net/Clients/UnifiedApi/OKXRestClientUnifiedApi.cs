using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Options;
using System;

namespace OKX.Net.Clients.UnifiedApi;

internal partial class OKXRestClientUnifiedApi : RestApiClient, IOKXRestClientUnifiedApi
{
    #region Internal Fields
    public new OKXRestOptions ClientOptions => (OKXRestOptions)base.ClientOptions;

    private static TimeSyncState _timeSyncState = new("Unified Api");

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
            : base(logger, httpClient, options.Environment.RestAddress, options, options.UnifiedOptions)
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
    protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(OKXExchange._serializerContext));

    /// <inheritdoc />
    protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(OKXExchange._serializerContext));

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OKXAuthenticationProvider(credentials);

    /// <inheritdoc />
    public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        => OKXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

    internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
    {
        var result = await base.SendAsync<OKXRestApiResponse<T>>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
        if (!result.Success) return result.AsError<T>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<T>(new ServerError(result.Data.ErrorCode, GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage!)));

        return result.As<T>(result.Data.Data);
    }

    internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
        => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders, rateLimitKeySuffix: rateLimitKeySuffix);

    internal async Task<WebCallResult<T>> SendRawAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
    {
        return await base.SendAsync<T>(BaseAddress, definition, parameters, cancellationToken, null, weight, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
    }

    internal async Task<WebCallResult<T>> SendGetSingleAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
    {
        var result = await SendToAddressAsync<T[]>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);
        if (!result)
            return result.As<T>(default);

        if (!result.Data.Any())
            return result.AsError<T>(new ServerError(ErrorInfo.Unknown with { Message = "No response data" }));

        return result.As<T>(result.Data?.FirstOrDefault());
    }

    /// <inheritdoc />
    protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
        => ExchangeData.GetServerTimeAsync();

    /// <inheritdoc />
    public override TimeSyncInfo? GetTimeSyncInfo()
        => new(_logger, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, _timeSyncState);

    /// <inheritdoc />
    public override TimeSpan? GetTimeOffset()
        => _timeSyncState.TimeOffset;

    /// <inheritdoc />
    protected override Error? TryParseError(RequestDefinition request, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
    {
        if (!accessor.IsValid)
            return new ServerError(ErrorInfo.Unknown);

        var codePath = MessagePath.Get().Property("code");
        var msgPath = MessagePath.Get().Property("msg");
        var code = accessor.GetValue<string?>(codePath);
        var msg = accessor.GetValue<string>(msgPath);
        if (code == null || !int.TryParse(code, out var intCode))
            return new ServerError(ErrorInfo.Unknown with { Message = msg });

        if (intCode >= 50000)
            return new ServerError(intCode, GetErrorInfo(intCode, msg));

        return null;
    }

    /// <inheritdoc />
    protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
    {
        if (!accessor.IsValid)
            return new ServerError(ErrorInfo.Unknown, exception: exception);

        var codePath = MessagePath.Get().Property("code");
        var msgPath = MessagePath.Get().Property("msg");
        var code = accessor.GetValue<string?>(codePath);
        var msg = accessor.GetValue<string>(msgPath);
        if (msg == null)
            return new ServerError(ErrorInfo.Unknown, exception: exception);

        if (code == null || !int.TryParse(code, out var intCode))
            return new ServerError(ErrorInfo.Unknown with { Message = msg }, exception);

        return new ServerError(intCode, GetErrorInfo(intCode, msg), exception);
    }
}
