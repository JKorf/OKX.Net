using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class ContractTypeConverter : BaseConverter<OKXContractType>
{
    public ContractTypeConverter() : this(true) { }
    public ContractTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXContractType, string>> Mapping => new List<KeyValuePair<OKXContractType, string>>
    {
        new KeyValuePair<OKXContractType, string>(OKXContractType.Linear, "linear"),
        new KeyValuePair<OKXContractType, string>(OKXContractType.Inverse, "inverse"),
    };
}