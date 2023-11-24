using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Serilog;

namespace Infraestructure.Kafka;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly IProducer<Null, string> _producer;
    private const string topic = "defaultTopic";
    public KafkaProducerService(ProducerConfig config)
    {
        _producer = new ProducerBuilder<Null, string>(config).Build();
        topicConfig(config);
    }

    public async Task<object> ProduceMessageAsync(string message)
    {
        return await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
    public void Dispose()
    {
        _producer?.Dispose();
    }

    private void topicConfig(ProducerConfig config)
    {
        using var adminClient = new AdminClientBuilder(config).Build();
        try
        {
            var topicMetadata = adminClient.GetMetadata(topic, TimeSpan.FromSeconds(10));
            if (topicMetadata.Topics.Find(t => t.Topic == topic) == null)
            {
                var topicConfig = new TopicSpecification
                {
                    Name = topic,
                    NumPartitions = 1,
                    ReplicationFactor = 1
                };

                adminClient.CreateTopicsAsync(new[] { topicConfig }).Wait();
            }
            else
            {
                Log.Information($"El topic '{topic}' ya existe.");
            }
        }
        catch (CreateTopicsException e)
        {
            Log.Error($"Error al crear el topic: {e.Results[0].Error.Reason}");
        }
    }

}