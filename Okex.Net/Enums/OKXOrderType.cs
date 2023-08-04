namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXOrderType
{
    MarketOrder,
    LimitOrder,
    PostOnly,
    FillOrKill,
    ImmediateOrCancel,
    OptimalLimitOrder,
}