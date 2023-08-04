using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class AccountBillTypeConverter : BaseConverter<OKXAccountBillType>
{
    public AccountBillTypeConverter() : this(true) { }
    public AccountBillTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXAccountBillType, string>> Mapping => new()
    {
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.Transfer, "1"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.Trade, "2"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.Delivery, "3"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.AutoTokenConversion, "4"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.Liquidation, "5"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.MarginTransfer, "6"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.InterestDeduction, "7"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.FundingFee, "8"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.ADL, "9"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.Clawback, "10"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.SystemTokenConversion, "11"),
        new KeyValuePair<OKXAccountBillType, string>(OKXAccountBillType.StrategyTransfer, "12"),
    };
}