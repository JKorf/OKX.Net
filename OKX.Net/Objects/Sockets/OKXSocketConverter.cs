//using CryptoExchange.Net.Objects.Sockets;
//using OKX.Net.Objects.Account;
//using OKX.Net.Objects.Market;
//using OKX.Net.Objects.Public;
//using OKX.Net.Objects.Sockets.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace OKX.Net.Objects.Sockets;
//internal class OKXSocketConverter : SocketConverter
//{
//    private static readonly Dictionary<string, Type> _channelTypeMap = new Dictionary<string, Type>()
//    {
//        { "open-interest", typeof(OKXSocketUpdate<IEnumerable<OKXOpenInterest>>) },
//        { "tickers", typeof(OKXSocketUpdate<IEnumerable<OKXTicker>>) },
//        { "balance_and_position", typeof(OKXSocketUpdate<IEnumerable<OKXPositionAndBalanceUpdate>>) },
//    };

//    public override MessageInterpreterPipeline InterpreterPipeline { get; } = new MessageInterpreterPipeline
//    {
//        PreInspectCallbacks = new List<PreInspectCallback>
//            {
//                new PreInspectCallback
//                {
//                    Callback = PreInspectForPong
//                }
//            },
//        GetIdentity = GetIdentity,

//        //PostInspectCallbacks = new List<object>
//        //    {
//        //        new PostInspectCallback
//        //        {
//        //            TypeFields = new List<TypeField> 
//        //            { 
//        //                new TypeField("data"),
//        //                new TypeField("arg:channel"),
//        //                new TypeField("arg:instId", false)
//        //            },
//        //            Callback = GetDeserializationTypeStream
//        //        },
//        //        new PostInspectCallback
//        //        {
//        //            TypeFields = new List<TypeField> 
//        //            { 
//        //                new TypeField("event"),
//        //                new TypeField("arg:channel", false),
//        //                new TypeField("arg:instId", false) 
//        //            },
//        //            Callback = GetDeserializationTypeChannelEvent
//        //        },
//        //    }
//    };

//    public static PreInspectResult? PreInspectForPong(Stream stream)
//    {
//        return new PreInspectResult
//        {
//            Matched = stream.Length == 4,
//            Identifier = "pong"
//        };
//    }

//    private static string GetIdentity(IMessageAccessor accessor)
//    {
//        var evnt = accessor.GetStringValue("event");
//        var channel = accessor.GetStringValue("arg:channel");
//        var instId = accessor.GetStringValue("arg:instId");
//        return $"{evnt}{channel?.ToLowerInvariant()}{instId?.ToLowerInvariant()}";
//    }

//    public static PostInspectResult GetDeserializationTypeChannelEvent(IMessageAccessor accessor, Dictionary<string, Type> processors)
//    {
//        var channel = accessor.GetStringValue("arg:channel");
//        var instId = accessor.GetStringValue("arg:instId");

//        return new PostInspectResult
//        {
//            Identifier = $"{accessor.GetStringValue("event")}{channel?.ToLowerInvariant()}{instId?.ToLowerInvariant()}",
//            Type = typeof(OKXSocketResponse)
//        };
//    }

//    public static PostInspectResult GetDeserializationTypeStream(IMessageAccessor accessor, Dictionary<string, Type> processors)
//    {
//        if (_channelTypeMap.TryGetValue(accessor.GetStringValue("arg:channel").ToLowerInvariant(), out var type))
//        {
//            return new PostInspectResult
//            {
//                Identifier = $"{accessor.GetStringValue("arg:channel").ToLowerInvariant()}{accessor.GetStringValue("arg:instId")?.ToLowerInvariant()}",
//                Type = type
//            };
//        }

//        return new PostInspectResult();
//    }
//}
