using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class InsuranceTypeConverter : BaseConverter<OKXInsuranceType>
{
    public InsuranceTypeConverter() : this(true) { }
    public InsuranceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXInsuranceType, string>> Mapping => new List<KeyValuePair<OKXInsuranceType, string>>
    {
        new KeyValuePair<OKXInsuranceType, string>(OKXInsuranceType.All, "all"),
        new KeyValuePair<OKXInsuranceType, string>(OKXInsuranceType.LiquidationBalanceDeposit, "liquidation_balance_deposit"),
        new KeyValuePair<OKXInsuranceType, string>(OKXInsuranceType.BankruptcyLoss, "bankruptcy_loss"),
        new KeyValuePair<OKXInsuranceType, string>(OKXInsuranceType.PlatformRevenue, "platform_revenue"),
    };
}