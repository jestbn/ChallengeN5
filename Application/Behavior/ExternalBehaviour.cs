﻿using Infraestructure.ElasticSearch;
using Infraestructure.Kafka;
using MediatR;

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
        await _elasticService.IndexDocument(request!, cancellationToken);

        var response = await next();

        await _producerService.ProduceMessageAsync(request!.ToString()!, cancellationToken);
        
        return response;
    }
}