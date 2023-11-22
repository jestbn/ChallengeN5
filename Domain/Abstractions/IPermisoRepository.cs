using Domain.Permisos;

namespace Domain.Abstractions
{
    public interface IPermisoRepository
    {
        void Add(Permiso permisos);

        Permiso GetById(PermisoId Id);
    }
}
