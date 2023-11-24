using Confluent.Kafka;

namespace Infraestructure.Kafka;

public interface IKafkaProducerService
{
    Task<DeliveryResult<Null, string>> ProduceMessageAsync(string message, CancellationToken cancellationToken);
    void Dispose();
}