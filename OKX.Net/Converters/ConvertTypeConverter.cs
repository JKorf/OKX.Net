using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class ConvertTypeConverter : BaseConverter<OKXConvertType>
{
    public ConvertTypeConverter() : this(true) { }
    public ConvertTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXConvertType, string>> Mapping => new List<KeyValuePair<OKXConvertType, string>>
    {
        new KeyValuePair<OKXConvertType, string>(OKXConvertType.CurrencyToContract, "1"),
        new KeyValuePair<OKXConvertType, string>(OKXConvertType.ContractToCurrency, "2"),
    };
}