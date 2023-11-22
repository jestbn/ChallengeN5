using Domain.TipoPermiso;

namespace Domain.Abstractions
{
    public interface ITipoPermisoRepository
    {
        TipoPermiso.TipoPermiso GetById(TipoPermisoId Id);
    }
}
