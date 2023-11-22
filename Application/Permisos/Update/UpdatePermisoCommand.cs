using Domain.Shared;
using MediatR;

namespace Application.Permisos.Update
{
    public record UpdatePermisoCommand(
        int Id,
        string Nombre,
        string Apellido,
        int Tipo,
        DateTime Fecha) : IRequest<Result>
    {
    }
}
