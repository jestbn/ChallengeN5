using Application.Permisos.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(CreatePermisosCommand req, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(req, cancellationToken);
            if (result.Success) return Ok(result);
            else
                return BadRequest(result.Errors);
        }
    }
}
