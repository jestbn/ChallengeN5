using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Serilog;

namespace Infraestructure.Kafka;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly IProducer<Null, string> _producer;
    private const string Topic = "defaultTopic";
    public KafkaProducerService(ProducerConfig config)
    {
        _producer = new ProducerBuilder<Null, string>(config).Build();
        TopicConfig(config);
    }

    public async Task<DeliveryResult<Null, string>> ProduceMessageAsync(string message, CancellationToken cancellationToken)
    {
        return await _producer.ProduceAsync(Topic, new Message<Null, string> { Value = message }, cancellationToken);
    }
    public void Dispose()
    {
        _producer?.Dispose();
    }

    private static void TopicConfig(ProducerConfig config)
    {
        using var adminClient = new AdminClientBuilder(config).Build();
        try
        {
            var topicMetadata = adminClient.GetMetadata(Topic, TimeSpan.FromSeconds(10));
            if (topicMetadata.Topics.Find(t => t.Topic == Topic) == null)
            {
                var topicConfig = new TopicSpecification
                {
                    Name = Topic,
                    NumPartitions = 1,
                    ReplicationFactor = 1
                };

                adminClient.CreateTopicsAsync(new[] { topicConfig }).Wait();
            }
            else
            {
                Log.Information($"El topic '{Topic}' ya existe.");
            }
        }
        catch (CreateTopicsException e)
        {
            Log.Error($"Error al crear el topic: {e.Results[0].Error.Reason}");
        }
    }

}