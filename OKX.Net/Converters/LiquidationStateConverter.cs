using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class LiquidationStateConverter : BaseConverter<OKXLiquidationState>
{
    public LiquidationStateConverter() : this(true) { }
    public LiquidationStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXLiquidationState, string>> Mapping => new List<KeyValuePair<OKXLiquidationState, string>>
    {
        new KeyValuePair<OKXLiquidationState, string>(OKXLiquidationState.Unfilled, "unfilled"),
        new KeyValuePair<OKXLiquidationState, string>(OKXLiquidationState.Filled, "filled"),
    };
}