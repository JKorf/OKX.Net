using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class TradeHistoryPaginationTypeConverter : BaseConverter<OKXTradeHistoryPaginationType>
{
    public TradeHistoryPaginationTypeConverter() : this(true) { }
    public TradeHistoryPaginationTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXTradeHistoryPaginationType, string>> Mapping => new List<KeyValuePair<OKXTradeHistoryPaginationType, string>>
    {
        new KeyValuePair<OKXTradeHistoryPaginationType, string>(OKXTradeHistoryPaginationType.TradeId, "1"),
        new KeyValuePair<OKXTradeHistoryPaginationType, string>(OKXTradeHistoryPaginationType.Timestamp, "2"),
    };
}