using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class DeliveryExerciseHistoryTypeConverter : BaseConverter<OKXDeliveryExerciseHistoryType>
{
    public DeliveryExerciseHistoryTypeConverter() : this(true) { }
    public DeliveryExerciseHistoryTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXDeliveryExerciseHistoryType, string>> Mapping => new List<KeyValuePair<OKXDeliveryExerciseHistoryType, string>>
    {
        new KeyValuePair<OKXDeliveryExerciseHistoryType, string>(OKXDeliveryExerciseHistoryType.Delivery, "delivery"),
        new KeyValuePair<OKXDeliveryExerciseHistoryType, string>(OKXDeliveryExerciseHistoryType.Exercised, "exercised"),
        new KeyValuePair<OKXDeliveryExerciseHistoryType, string>(OKXDeliveryExerciseHistoryType.ExpiredOtm, "expired_otm"),
    };
}