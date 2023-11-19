using Domain.Abstractions;
using Domain.Permisos;

namespace Persistence.Repositories
{
    public sealed class PermisoRepository : IPermisoRepository
    {
        private readonly ApplicationDbContext _context;

        public PermisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Permiso permisos)
        {
            _context.Set<Permiso>().Add(permisos);
        }
    }
}
