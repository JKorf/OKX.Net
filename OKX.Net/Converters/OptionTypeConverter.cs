using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class OptionTypeConverter : BaseConverter<OKXOptionType>
{
    public OptionTypeConverter() : this(true) { }
    public OptionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXOptionType, string>> Mapping => new List<KeyValuePair<OKXOptionType, string>>
    {
        new KeyValuePair<OKXOptionType, string>(OKXOptionType.Call, "C"),
        new KeyValuePair<OKXOptionType, string>(OKXOptionType.Put, "P"),
    };
}