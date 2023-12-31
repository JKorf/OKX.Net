﻿namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXAccountBillSubType
{
    Buy,
    Sell,
    OpenLong,
    OpenShort,
    CloseLong,
    CloseShort,
    InterestDeduction,
    TransferIn,
    TransferOut,
    ManualMarginIncrease,
    ManualMarginDecrease,
    AutoMarginIncrease,
    AutoBuy,
    AutoSell,
    SystemTokenConversionTransferIn,
    SystemTokenConversionTransferOut,
    PartialLiquidationCloseLong,
    PartialLiquidationCloseShort,
    PartialLiquidationBuy,
    PartialLiquidationSell,
    LiquidationLong,
    LiquidationShort,
    LiquidationBuy,
    LiquidationSell,
    LiquidationTransferIn,
    LiquidationTransferOut,
    ADLCloseLong,
    ADLCloseShort,
    ADLBuy,
    ADLSell,
    Exercised,
    CounterpartyExercised,
    ExpiredOTM,
    DeliveryLong,
    DeliveryShort,
    DeliveryExerciseClawback,
    FundingFeeExpense,
    FundingFeeIncome,
    SystemTransferIn,
    ManuallyTransferIn,
    SystemTransferOut,
    ManuallyTransferOut,
}