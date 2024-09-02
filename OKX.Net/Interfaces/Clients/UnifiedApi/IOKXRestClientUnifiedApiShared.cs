using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Interfaces.Clients.UnifiedApi
{
    public interface IOKXRestClientUnifiedApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ITickerRestClient,
        //ITradeHistoryRestClient
        IWithdrawalRestClient,
        IWithdrawRestClient
    {
    }
}
