using Confluent.Kafka;

namespace Infraestructure.Kafka;

public class KafkaProducerServiceService : IKafkaProducerService
{
    private readonly IProducer<Null, string> _producer;
    private const string topic = "defaultTopic";
    public KafkaProducerServiceService(ProducerConfig config)
    {
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }
    public async Task<object> ProduceMessageAsync(string message)
    {
        return await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
    public void Dispose()
    {
        _producer?.Dispose();
    }
}