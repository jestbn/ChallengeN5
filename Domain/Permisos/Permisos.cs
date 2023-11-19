using Domain.TipoPermiso;

namespace Domain.Permisos
{
    public class Permisos
    {
        public Permisos(PermisoId id, string Nombre, string Apellido, TipoPermisos Tipo, DateTime Fecha)
        {//Todo: Mirar a ver si pongo el ID XD
            Id = id;
            NombreEmpleado = Nombre;
            ApellidoEmpleado = Apellido;
            TipoPermiso = Tipo;
            FechaPermiso = Fecha;
        }
        public PermisoId Id { get; private set; }
        public string NombreEmpleado { get; private set; }
        public string ApellidoEmpleado { get; private set; }
        public TipoPermisos TipoPermiso { get; private set; }
        public DateTime FechaPermiso { get; private set; }
    }
}
