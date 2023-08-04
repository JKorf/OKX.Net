using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class MarginTransferModeConverter : BaseConverter<OKXMarginTransferMode>
{
    public MarginTransferModeConverter() : this(true) { }
    public MarginTransferModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXMarginTransferMode, string>> Mapping => new List<KeyValuePair<OKXMarginTransferMode, string>>
    {
        new KeyValuePair<OKXMarginTransferMode, string>(OKXMarginTransferMode.AutoTransfer, "automatic"),
        new KeyValuePair<OKXMarginTransferMode, string>(OKXMarginTransferMode.ManualTransfer, "autonomy"),
    };
}