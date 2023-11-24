namespace Infraestructure.Kafka;

public interface IKafkaProducerService
{
    Task<object> ProduceMessageAsync(string message);
    void Dispose();
}