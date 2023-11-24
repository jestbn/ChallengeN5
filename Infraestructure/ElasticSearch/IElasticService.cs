namespace Infraestructure.ElasticSearch;

public interface IElasticService
{
    void LogService(object model, CancellationToken cancellationToken);
}