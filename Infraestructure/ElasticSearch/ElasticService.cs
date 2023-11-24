using Nest;

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

    public void LogService(object model, CancellationToken cancellationToken)
    {
        Serilog.Log.Information($"Se guardo: :{model}");
    }
    public async Task<bool> IndexDocument(object model, CancellationToken cancellationToken)
    {
        var indexResponse = await _elasticClient.IndexDocumentAsync(model, cancellationToken);
        return indexResponse.IsValid;
    }
}