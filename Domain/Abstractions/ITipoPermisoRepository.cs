namespace Domain.Abstractions
{
    public interface ITipoPermisoRepository
    {
        TipoPermiso.TipoPermiso GetById(int Id);
    }
}
