using Domain.Shared;
using MediatR;

namespace Application.Permisos.Get
{
    public record GetPermisosQuery() : IRequest<Result>
    {
    }
}
