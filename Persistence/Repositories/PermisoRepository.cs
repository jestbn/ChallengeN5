using Domain.Abstractions;
using Domain.Permisos;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class PermisoRepository : IPermisoRepository
    {
        private readonly ApplicationDbContext _context;

        public PermisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Permiso permiso)
        {
            if (permiso.Id is null) _context.Set<Permiso>().Add(permiso);
            else _context.Entry(permiso).State = EntityState.Modified;
        }

        public Permiso GetById(PermisoId Id)
        {
            var permiso = _context.Permisos.Find(Id);
            return permiso!;
        }
    }
}
