using OKX.Net.Clients;
using OKX.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Net.Clients
{
    public static class CryptoExchangeClientExtensions
    {
        public static IOKXRestClient OKX(this ICryptoExchangeClient baseClient) => baseClient.TryGet<IOKXRestClient>() ?? new OKXRestClient();
    }
}
