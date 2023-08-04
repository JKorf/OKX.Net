using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AccountBillSubTypeConverter : BaseConverter<OKXAccountBillSubType>
{
    public AccountBillSubTypeConverter() : this(true) { }
    public AccountBillSubTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAccountBillSubType, string>> Mapping => new()
    {
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.Buy, "1"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.Sell, "2"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.OpenLong, "3"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.OpenShort, "4"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.CloseLong, "5"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.CloseShort, "6"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.InterestDeduction, "9"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.TransferIn, "11"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.TransferOut, "12"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ManualMarginIncrease, "160"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ManualMarginDecrease, "161"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.AutoMarginIncrease, "162"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.AutoBuy, "110"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.AutoSell, "111"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.SystemTokenConversionTransferIn, "118"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.SystemTokenConversionTransferOut, "119"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.PartialLiquidationCloseLong, "100"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.PartialLiquidationCloseShort, "101"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.PartialLiquidationBuy, "102"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.PartialLiquidationSell, "103"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.LiquidationLong, "104"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.LiquidationShort, "105"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.LiquidationBuy, "106"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.LiquidationSell, "107"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.LiquidationTransferIn, "110"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.LiquidationTransferOut, "111"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ADLCloseLong, "125"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ADLCloseShort, "126"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ADLBuy, "127"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ADLSell, "128"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.Exercised, "170"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.CounterpartyExercised, "171"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ExpiredOTM, "172"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.DeliveryLong, "112"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.DeliveryShort, "113"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.DeliveryExerciseClawback, "117"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.FundingFeeExpense, "173"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.FundingFeeIncome, "174"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.SystemTransferIn, "200"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ManuallyTransferIn, "201"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.SystemTransferOut, "202"),
            new KeyValuePair<OKXAccountBillSubType, string>(OKXAccountBillSubType.ManuallyTransferOut, "203"),
        };
}