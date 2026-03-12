using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Rule type
/// </summary>
[JsonConverter(typeof(EnumConverter<SymbolRuleType>))]
public enum SymbolRuleType
{
    /// <summary>
    /// ["<c>normal</c>"] Normal trading
    /// </summary>
    [Map("normal")]
    Normal,
    /// <summary>
    /// ["<c>pre_market</c>"] Pre-market trading
    /// </summary>
    [Map("pre_market")]
    PreMarket,
    /// <summary>
    /// ["<c>rebase_contract</c>"] Pre-market rebase contract
    /// </summary>
    [Map("rebase_contract")]
    RebaseContract,
}
