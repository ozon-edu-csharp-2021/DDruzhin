using System;
using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace OzonEdu.MerchandiseApi.Infrastructure.KafkaProducers
{
    public class SerializerMerchPack<TMessage>: ISerializer<TMessage>, IDeserializer<TMessage>
    {
        public byte[] Serialize(TMessage data, SerializationContext context)
        {
            return data switch
            {
                string => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)),
                _ => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
            };
        }

        public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return isNull ? default : JsonSerializer.Deserialize<TMessage>(data);
        }
    }
}