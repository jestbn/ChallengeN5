﻿using Nest;

namespace Infraestructure.ElasticSearch;

public class ElasticService : IElasticService
{
    private readonly IElasticClient _elasticClient;
    private const string defaultindex = "default_index";

    public ElasticService(ConnectionSettings connectionSettings)
    {
        var settings = connectionSettings
            .DefaultIndex(defaultindex);
        _elasticClient = new ElasticClient(settings);
    }

    public async Task LogService(object model, CancellationToken cancellationToken)
    {
        Serilog.Log.Information($"Man this :{model}");
    }
}