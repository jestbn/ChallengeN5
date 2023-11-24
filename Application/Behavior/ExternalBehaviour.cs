using Infraestructure.ElasticSearch;
using Infraestructure.Kafka;
using MediatR;
using Serilog;

namespace Application.Behavior;

public class ExternalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IElasticService _elasticService;
    private readonly IKafkaProducerService _producerService;

    public ExternalBehaviour(IElasticService elasticService, IKafkaProducerService producerService)
    {
        _elasticService = elasticService;
        _producerService = producerService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //_elasticService.LogService(request!, cancellationToken); Implementation via Serilog
        var elasticResponse = await _elasticService.IndexDocument(request!, cancellationToken);

        var response = await next();

        var kafkaresponse = await _producerService.ProduceMessageAsync(request!.ToString()!, cancellationToken);

        Log.Information($"Manejando servicios externos de elasticsearch [{elasticResponse}] y kafka [{kafkaresponse.Status}]");

        return response;
    }
}