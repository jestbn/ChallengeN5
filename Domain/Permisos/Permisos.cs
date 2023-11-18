using Domain.TipoPermiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Permisos
{
    public class Permisos
    {
        public PermisoId Id { get; private set; }
        public string NombreEmpleado { get; private set; }
        public string ApellidoEmpleado { get; private set; }
        public TipoPermisos TipoPermiso { get; private set; }
        public DateTime FechaPermiso { get; private set; }
    }
}
