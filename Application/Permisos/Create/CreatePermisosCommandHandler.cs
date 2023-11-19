using Domain.Abstractions;
using Domain.Permisos;
using Domain.TipoPermiso;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permisos.Create
{
    internal class CreatePermisosCommandHandler : IRequestHandler<CreatePermisosCommand>
    {
        private readonly IPermisosRepository _permisoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePermisosCommandHandler(IPermisosRepository permisoRepository, IUnitOfWork unitOfWork)
        {
            _permisoRepository = permisoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreatePermisosCommand request, CancellationToken cancellationToken)
        {
            var permiso = new Domain.Permisos.Permisos(
                id: new PermisoId(1),
                Nombre: request.Nombre,
                Apellido: request.Apellido,
                Tipo: new TipoPermisos(),
                Fecha: request.Fecha
                );
            _permisoRepository.Add(permiso);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
