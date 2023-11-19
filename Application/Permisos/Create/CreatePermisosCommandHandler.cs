using Domain.Abstractions;
using Domain.Permisos;
using MediatR;

namespace Application.Permisos.Create
{
    internal class CreatePermisosCommandHandler : IRequestHandler<CreatePermisosCommand>
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePermisosCommandHandler(IPermisoRepository permisoRepository, IUnitOfWork unitOfWork)
        {
            _permisoRepository = permisoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreatePermisosCommand request, CancellationToken cancellationToken)
        {
            Permiso permiso = new(
                Id: new PermisoId(1),
                NombreEmpleado: request.Nombre,
                ApellidoEmpleado: request.Apellido,
                //TipoPermiso: new TipoPermiso(request.Tipo, ""),
                FechaPermiso: request.Fecha
                );
            _permisoRepository.Add(permiso);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
