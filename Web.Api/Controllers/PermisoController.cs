using Application.Permisos.Create;
using Application.Permisos.Get;
using Application.Permisos.Update;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IElasticClient _elasticClient;

        public PermisoController(IMediator mediator, IElasticClient elasticClient)
        {
            _mediator = mediator;
            _elasticClient = elasticClient;
        }

        [HttpPost("Request")]
        public async Task<IActionResult> Create(CreatePermisoCommand req, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(req, cancellationToken);

            return result.Success switch
            {
                true when await IndexDocumentElasticSearch(result.Data, cancellationToken) => Ok(result),
                false => BadRequest(result.Errors),
                _ => BadRequest("Elastic search error")
            };
        }

        [HttpPut("Modify")]
        public async Task<IActionResult> Update(UpdatePermisoCommand req, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(req, cancellationToken);
            return result.Success switch
            {
                true when await IndexDocumentElasticSearch(result.Data, cancellationToken) => Ok(result),
                false => BadRequest(result.Errors),
                _ => BadRequest("Elastic search error")
            };
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetPermisosQuery(), cancellationToken);
            return result.Success switch
            {
                true when await IndexDocumentElasticSearch(result.Data, cancellationToken) => Ok(result),
                false => BadRequest(result.Errors),
                _ => BadRequest("Elastic search error")
            };
        }

        #region Helper

        private async Task<bool> IndexDocumentElasticSearch(object model, CancellationToken token)
        {
            var indexResponse = await _elasticClient.IndexDocumentAsync(model, token);
            return indexResponse.IsValid;
        }

        #endregion
    }
}
