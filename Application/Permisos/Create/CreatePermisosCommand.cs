using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
