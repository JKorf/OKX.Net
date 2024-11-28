using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Options;
/// <summary>
/// OKX options
/// </summary>
public class OKXOptions : LibraryOptions<OKXRestOptions, OKXSocketOptions, OKXApiCredentials, OKXEnvironment>
{
}
