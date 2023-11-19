using MediatR;

namespace Application.Permisos.Create
{
    public record CreatePermisosCommand(
        string Nombre,
        string Apellido,
        int Tipo,
        DateTime Fecha ) : IRequest
    {
    }
}
