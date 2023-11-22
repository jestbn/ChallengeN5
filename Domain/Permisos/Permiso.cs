namespace Domain.Permisos
{
    public class Permiso
    {
        public Permiso(string NombreEmpleado, string ApellidoEmpleado, DateTime FechaPermiso)
        {

            this.NombreEmpleado = NombreEmpleado;
            this.ApellidoEmpleado = ApellidoEmpleado;
            this.FechaPermiso = FechaPermiso;
        }

        public PermisoId Id { get; private set; }
        public string NombreEmpleado { get; private set; }
        public string ApellidoEmpleado { get; private set; }
        public TipoPermiso.TipoPermiso TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; private set; }
    }
}
