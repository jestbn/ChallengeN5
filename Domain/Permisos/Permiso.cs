namespace Domain.Permisos
{
    public class Permiso
    {
        public Permiso(PermisoId Id, string NombreEmpleado, string ApellidoEmpleado, DateTime FechaPermiso)
        {//Todo: Mirar a ver si pongo el ID XD
            this.Id = Id;
            this.NombreEmpleado = NombreEmpleado;
            this.ApellidoEmpleado = ApellidoEmpleado;
            //this.TipoPermiso = TipoPermiso;
            this.FechaPermiso = FechaPermiso;
        }

        public PermisoId Id { get; private set; }
        public string NombreEmpleado { get; private set; }
        public string ApellidoEmpleado { get; private set; }
        public TipoPermiso.TipoPermiso TipoPermiso { get; private set; }
        public DateTime FechaPermiso { get; private set; }
    }
}
