namespace Domain.TipoPermiso
{
    public class TipoPermiso
    {
        public TipoPermiso()
        {
            //this.Id = new TipoPermisoId(id);
            //this.Descripcion = desc;
        }
        public TipoPermisoId Id { get; private set; }
        public string Descripcion { get; private set; }
    }
}
