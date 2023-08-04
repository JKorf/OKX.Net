using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class MarginAddReduceConverter : BaseConverter<OKXMarginAddReduce>
{
    public MarginAddReduceConverter() : this(true) { }
    public MarginAddReduceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXMarginAddReduce, string>> Mapping => new List<KeyValuePair<OKXMarginAddReduce, string>>
    {
        new KeyValuePair<OKXMarginAddReduce, string>(OKXMarginAddReduce.Add, "add"),
        new KeyValuePair<OKXMarginAddReduce, string>(OKXMarginAddReduce.Reduce, "reduce"),
    };
}