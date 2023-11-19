using Domain.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public sealed class PermisosRepository : IPermisosRepository
    {
        private readonly ApplicationDbContext _context;

        public PermisosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Permisos permisos)
        {
            _context.Set<Permisos>().Add(permisos);
        }
    }
}
