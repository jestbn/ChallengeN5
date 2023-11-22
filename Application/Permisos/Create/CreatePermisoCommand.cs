using Domain.Shared;
using MediatR;

namespace Application.Permisos.Create
{
    public record CreatePermisoCommand(
        string Nombre,
        string Apellido,
        int Tipo,
        DateTime Fecha) : IRequest<Result>
    {
    }
}
