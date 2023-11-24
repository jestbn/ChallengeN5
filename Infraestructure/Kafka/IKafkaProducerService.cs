namespace Infraestructure.Kafka;

public interface IKafkaProducerService
{
    Task ProduceMessageAsync(string message);
    void Dispose();
}