using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class InstrumentStateConverter : BaseConverter<OKXInstrumentState>
{
    public InstrumentStateConverter() : this(true) { }
    public InstrumentStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXInstrumentState, string>> Mapping => new List<KeyValuePair<OKXInstrumentState, string>>
    {
        new KeyValuePair<OKXInstrumentState, string>(OKXInstrumentState.Live, "live"),
        new KeyValuePair<OKXInstrumentState, string>(OKXInstrumentState.Suspend, "suspend"),
        new KeyValuePair<OKXInstrumentState, string>(OKXInstrumentState.PreOpen, "preopen"),
        new KeyValuePair<OKXInstrumentState, string>(OKXInstrumentState.Test, "test"),
    };
}