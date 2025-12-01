using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using OKX.Net.Objects.Sockets.Models;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;

namespace OKX.Net.Clients.MessageHandlers
{
    internal class OKXSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(OKXExchange._serializerContext);

        public OKXSocketMessageHandler()
        {
            AddTopicMapping<OKXSocketUpdate>(x => x.Arg.InstrumentType + x.Arg.InstrumentFamily + x.Arg.Symbol);
        }

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("event") { Constraint = x => x!.Equals("error", StringComparison.Ordinal) },
                ],
                IdentifyMessageCallback = x => "error"! // TODO
            },

            new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("event"),
                    new PropertyFieldReference("channel") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("event")! + x.FieldValue("channel")!
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("channel") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel")!
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("event"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("event")!
            },
        ];

        public override string? GetTypeIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        {
            if (data.Length == 4)
                return "pong";

            return base.GetTypeIdentifier(data, webSocketMessageType);
        }
    }
}
