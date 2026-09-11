using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKX.Net.Objects.Options;

/// <inheritdoc/>
public class OKXSharedApiOptions : SharedApiOptions
{
    /// <summary>
    /// Whether to use XPerps as perpetual linear contracts when using the Shared API's on the Europe environment
    /// </summary>
    public bool EuropeUseXPerps { get; set; }
}
