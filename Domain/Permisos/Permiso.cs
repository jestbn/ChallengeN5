﻿namespace Domain.Permisos
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

        public void Update(string nombre, string apellido, DateTime fecha, TipoPermiso.TipoPermiso tipo)
        {
            this.NombreEmpleado = nombre;
            this.ApellidoEmpleado = apellido;
            this.FechaPermiso = fecha;
            this.TipoPermiso = tipo;
        }
    }
}
