using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum AccountBillType
{
    [Map("1")]
    Transfer,
    [Map("2")]
    Trade,
    [Map("3")]
    Delivery,
    [Map("4")]
    AutoTokenConversion,
    [Map("5")]
    Liquidation,
    [Map("6")]
    MarginTransfer,
    [Map("7")]
    InterestDeduction,
    [Map("8")]
    FundingFee,
    [Map("9")]
    ADL,
    [Map("10")]
    Clawback,
    [Map("11")]
    SystemTokenConversion,
    [Map("12")]
    StrategyTransfer,
    [Map("13")]
    DDH,
    [Map("14")]
    BlockTrade,
    [Map("15")]
    QuickMargin,
    [Map("22")]
    Repay,
    [Map("24")]
    SpreadTrading,
    [Map("26")]
    StructuredProducts,
    [Map("250")]
    CopyTraderProfitShareExpense,
    [Map("251")]
    CopyTraderProfitShareRefund,
}