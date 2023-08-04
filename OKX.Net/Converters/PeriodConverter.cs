using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class PeriodConverter : BaseConverter<OKXPeriod>
{
    public PeriodConverter() : this(true) { }
    public PeriodConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXPeriod, string>> Mapping => new List<KeyValuePair<OKXPeriod, string>>
    {
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.OneMinute, "1m"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.ThreeMinutes, "3m"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.FiveMinutes, "5m"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.FifteenMinutes, "15m"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.ThirtyMinutes, "30m"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.OneHour, "1H"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.TwoHours, "2H"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.FourHours, "4H"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.SixHours, "6H"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.TwelveHours, "12H"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.OneDay, "1D"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.OneWeek, "1W"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.OneMonth, "1M"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.ThreeMonths, "3M"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.SixMonths, "6M"),
        new KeyValuePair<OKXPeriod, string>(OKXPeriod.OneYear, "1Y"),
    };
}