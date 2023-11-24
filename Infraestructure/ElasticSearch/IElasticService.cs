namespace Infraestructure.ElasticSearch;

public interface IElasticService
{
    Task<bool> IndexDocument(object model, CancellationToken cancellationToken);
}