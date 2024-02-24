using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class MaintenanceServiceConverter : BaseConverter<OKXMaintenanceService>
{
    public MaintenanceServiceConverter() : this(true) { }
    public MaintenanceServiceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXMaintenanceService, string>> Mapping => new List<KeyValuePair<OKXMaintenanceService, string>>
    {
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.WebSocket, "0"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.SpotMargin, "1"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.Futures, "2"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.Perpetual, "3"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.Options, "4"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.Trading, "5"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.BlockTrading, "6"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.TradingBot, "7"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.TradingServiceAccounts, "8"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.TradingServiceProducts, "9"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.SpreadTrading, "10"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.CopyTrading, "11"),
        new KeyValuePair<OKXMaintenanceService, string>(OKXMaintenanceService.Other, "99"),
    };
}