using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class TradeSideConverter : BaseConverter<OKXTradeSide>
{
    public TradeSideConverter() : this(true) { }
    public TradeSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXTradeSide, string>> Mapping => new List<KeyValuePair<OKXTradeSide, string>>
    {
        new KeyValuePair<OKXTradeSide, string>(OKXTradeSide.Buy, "buy"),
        new KeyValuePair<OKXTradeSide, string>(OKXTradeSide.Sell, "sell"),
    };
}