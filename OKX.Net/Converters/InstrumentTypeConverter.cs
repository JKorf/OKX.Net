using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class InstrumentTypeConverter : BaseConverter<OKXInstrumentType>
{
    public InstrumentTypeConverter() : this(true) { }
    public InstrumentTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXInstrumentType, string>> Mapping => new List<KeyValuePair<OKXInstrumentType, string>>
    {
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Any, "ANY"),
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Spot, "SPOT"),
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Margin, "MARGIN"),
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Swap, "SWAP"),
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Futures, "FUTURES"),
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Option, "OPTION"),
        new KeyValuePair<OKXInstrumentType, string>(OKXInstrumentType.Contracts, "CONTRACTS"),
    };
}