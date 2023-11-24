using Application.Permisos.Create;
using Application.Permisos.Get;
using Application.Permisos.Update;
using Infraestructure.ElasticSearch;
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

        public PermisoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Request")]
        public async Task<IActionResult> Create(CreatePermisoCommand req, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(req, cancellationToken);
            if (result.Success) return Ok(result);
            else
                return BadRequest(result.Errors);
        }

        [HttpPut("Modify")]
        public async Task<IActionResult> Update(UpdatePermisoCommand req, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(req, cancellationToken);
            if (result.Success) return Ok(result);
            else
                return BadRequest(result.Errors);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetPermisosQuery(), cancellationToken);
            if (result.Success) return Ok(result);
            else
                return BadRequest(result.Errors);
        }
    }
}
