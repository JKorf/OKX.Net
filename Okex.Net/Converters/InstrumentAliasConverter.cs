using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class InstrumentAliasConverter : BaseConverter<OKXInstrumentAlias>
{
    public InstrumentAliasConverter() : this(true) { }
    public InstrumentAliasConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXInstrumentAlias, string>> Mapping => new List<KeyValuePair<OKXInstrumentAlias, string>>
    {
        new KeyValuePair<OKXInstrumentAlias, string>(OKXInstrumentAlias.ThisWeek, "this_week"),
        new KeyValuePair<OKXInstrumentAlias, string>(OKXInstrumentAlias.NextWeek, "next_week"),
        new KeyValuePair<OKXInstrumentAlias, string>(OKXInstrumentAlias.Quarter, "quarter"),
        new KeyValuePair<OKXInstrumentAlias, string>(OKXInstrumentAlias.NextQuarter, "next_quarter"),
    };
}