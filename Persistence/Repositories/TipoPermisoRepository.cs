using Domain.Abstractions;
using Domain.TipoPermiso;

namespace Persistence.Repositories
{
    public class TipoPermisoRepository : ITipoPermisoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoPermisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TipoPermiso GetById(TipoPermisoId Id)
        {
            var tipo = _context.TipoPermisos.Find(Id);
            return tipo!;
        }
    }
}
