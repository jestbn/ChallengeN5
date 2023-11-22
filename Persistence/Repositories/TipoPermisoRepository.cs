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

        public TipoPermiso GetById(int Id)
        {
            var tipo = _context.TipoPermisos.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            return tipo!;
        }
    }
}
