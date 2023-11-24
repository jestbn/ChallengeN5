namespace Infraestructure.ElasticSearch;

public interface IElasticService
{
    Task LogService(object model, CancellationToken cancellationToken);
}