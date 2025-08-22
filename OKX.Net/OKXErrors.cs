using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKX.Net
{
    internal static class OKXErrors
    {
        public static ErrorMapping ErrorMapping { get; } = new ErrorMapping([

            new ErrorInfo(ErrorType.Unauthorized, false, "API key does not match the environment", "50101"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Passphrase invalid", "50105", "60024"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "50110"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key invalid", "50111", "50119", "60005", "60032"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Authorization invalid", "50114"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "50120"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Not allowed", "51024"),

            new ErrorInfo(ErrorType.Unauthorized, false, "User blocked", "50007", "58004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Account frozen", "50009"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Account restricted from trading", "50027"),
            new ErrorInfo(ErrorType.Unauthorized, false, "No permissions for endpoint", "50030"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Symbol not allowed", "50033", "51155"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Endpoint not allowed without IP bound API key", "50035"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Feature not allowed due to region", "50051", "50052", "59113", "59114"),
            new ErrorInfo(ErrorType.Unauthorized, false, "KYC required", "50060", "51732", "51734", "51737", "58011", "58131", "58132", "58233", "58235", "58240", "58305", "58306", "58308", "58309"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Invalid signature", "50113"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Timestamp invalid", "50102", "50112"),

            new ErrorInfo(ErrorType.SystemError, true, "Service temporarily unavailable", "50001"),
            new ErrorInfo(ErrorType.SystemError, false, "Request internal timeout", "50004"),
            new ErrorInfo(ErrorType.SystemError, true, "Systems busy", "50013"),
            new ErrorInfo(ErrorType.SystemError, true, "System error", "50026"),

            new ErrorInfo(ErrorType.Timeout, true, "Request timed out", "51054"),
            new ErrorInfo(ErrorType.Timeout, true, "Order timed out", "51149"),
            new ErrorInfo(ErrorType.Timeout, true, "Cancellation timed out", "51412"),

            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "50011", "50040", "58102"),

            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open orders", "50061", "51025", "51102", "51103", "51174", "51182"),
            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open trailing stop orders", "51260"),
            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open stop orders", "51261"),
            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open iceberg orders", "51262"),
            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open TWAP orders", "51263"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "One of parameters is required", "50015"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameters don't match", "50016", "58129"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameters can't be send together", "50024"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter count exceeded", "50025"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid expireTime", "50036"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "51000"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Either orderId or clientOrderId is required", "51003"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Max number of order operations in request exceeded", "51111", "51501"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Quantity or price decimal precision exceeded", "51150"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "One of parameters need to be provided", "51176"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter range exceeded", "51180", "54009"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage range invalid", "51370"),

            new ErrorInfo(ErrorType.MissingParameter, false, "Parameter empty", "50014"),
            new ErrorInfo(ErrorType.MissingParameter, false, "One of parameters need to be provided", "51175", "51409"),
            new ErrorInfo(ErrorType.MissingParameter, false, "Either orderId or clientOrderId should be provided", "51407"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too small", "50122", "51020", "51120", "51286", "58206", "59118", "59123"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too large", "51005", "51101", "51202", "51203", "58205", "59124"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Close order quantity larger than position size", "51112"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity needs to be a multiple of lot size", "51121"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order value too high", "51185", "51201"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity should be a multiple of lot size", "70307"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Price parameter missing", "51204"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price not within range", "51006", "51031"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price or trigger price exceeds limit", "51116"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price too low", "51122", "51138", "51194"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price too high", "51137", "51195"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Price should be a multiple of tick size", "70304"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "51008", "51127", "51131", "51502", "51736", "58229", "58350", "59200"),
            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient available margin", "59303"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "50071", "51016", "51065", "52909", "70301"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol", "51001", "58009", "70004", "60018"),
            new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol does not match instrument type", "51015"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "51063", "51603", "52907", "52908"),

            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol not yet listed", "51021"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol suspended", "51022"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol expired", "51027"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol in delivery", "51028"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol in settlement", "51029"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol in funding fee settlement", "51030"),

            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take profit trigger price must be higher than order price", "51046"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop loss trigger price must be lower than order price", "51047"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take profit trigger price must be lower than order price", "51048"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop loss trigger price must be higher than order price", "51049"),

            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take profit trigger price must be higher than the best ask price", "51050"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop loss trigger price must be lower than the best ask price", "51051"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take profit trigger price must be lower than the best bid price", "51052"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop loss trigger price must be higher than the best bid price", "51053"),

            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take Profit trigger price cannot be higher than last price", "51277"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop loss trigger price cannot be lower than last price", "51278"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take Profit trigger price cannot be lower  than last price", "51279"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop loss trigger price cannot be higher than last price", "51280"),

            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Trailing stop callback rate range invalid", "51311"),
            new ErrorInfo(ErrorType.InvalidStopParameters, false, "Trailing stop order quantity range invalid", "51312"),

            new ErrorInfo(ErrorType.NoPosition, false, "Position does not exist", "51023", "51043"),

            new ErrorInfo(ErrorType.MaxPosition, false, "Position limit reached", "54031"),
            new ErrorInfo(ErrorType.MaxPosition, false, "Value of orders and positions exceeds limit", "51004", "51105", "51106", "51107", "51129", "51187", "54030"),

            new ErrorInfo(ErrorType.IncorrectState, false, "Position frozen due to auto deleveraging", "50017"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Asset frozen due to auto deleveraging", "50018"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Account frozen due to auto deleveraging", "50019"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Position frozen due to liquidation", "50020"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Asset frozen due to liquidation", "50021"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Account frozen due to liquidation", "50022"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Order cancellation failed as order has been filled, canceled or doesn't exist", "51400", "51503"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Order has been triggered and can't be canceled", "51416"),

            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Market orders only allowed at least 5 minutes after listing", "51507"),

            new ErrorInfo(ErrorType.RiskError, false, "Might lead to liquidation", "50048"),

            ]);
    }
}
