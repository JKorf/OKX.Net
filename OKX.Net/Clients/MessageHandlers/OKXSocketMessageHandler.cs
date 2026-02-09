using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using OKX.Net.Objects.Sockets.Models;
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
            AddTopicMapping<OKXSocketResponse>(x => x.Arg?.InstrumentType + x.Arg?.InstrumentFamily + (x.Arg?.Symbol ?? x.Arg?.Asset));
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event").WithEqualConstraint("error"),
                    new PropertyFieldReference("msg").WithStartsWithConstraint("Wrong URL or channel:"),
                ],
                TypeIdentifierCallback = x => {
                     var subParams = x.FieldValue("msg")!.Substring(21, x.FieldValue("msg")!.IndexOf(" doesn't exist") - 21);
                     var subParamsSplit = subParams.Split(',');
                    return $"error{string.Join("", subParamsSplit.Select(x => x.Split(':').Last()))}";
                }
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event").WithEqualConstraint("error"),
                ],
                TypeIdentifierCallback = x => "error"!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event"),
                    new PropertyFieldReference("channel") { Depth = 2 },
                ],
                TypeIdentifierCallback = x => x.FieldValue("event")! + x.FieldValue("channel")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel") { Depth = 2 },
                ],
                TypeIdentifierCallback = x => x.FieldValue("channel")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("event")!
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
