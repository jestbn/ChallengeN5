using Domain.Shared;
using MediatR;

namespace Application.Permisos.Get
{
    public record GetPermisoQuery(int Id) : IRequest<Result>
    {
    }
}
