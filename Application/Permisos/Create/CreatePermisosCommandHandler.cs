using Domain.Abstractions;
using Domain.Permisos;
using Domain.Shared;
using MediatR;

namespace Application.Permisos.Create
{
    public class CreatePermisosCommandHandler : IRequestHandler<CreatePermisosCommand, Result>
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoPermisoRepository _tipoPermiso;
        public CreatePermisosCommandHandler(
            IPermisoRepository permisoRepository,
            IUnitOfWork unitOfWork,
            ITipoPermisoRepository tipoPermiso)
        {
            _permisoRepository = permisoRepository;
            _unitOfWork = unitOfWork;
            _tipoPermiso = tipoPermiso;
        }

        public async Task<Result> Handle(CreatePermisosCommand request, CancellationToken cancellationToken)
        {
            var tipo = _tipoPermiso.GetById(request.Tipo);

            if (tipo is null)
            {
                return new Result { Success = false, Errors = new List<string> { "El tipo de permiso no existe" } };

            }

            Permiso permiso = new(
                NombreEmpleado: request.Nombre,
                ApellidoEmpleado: request.Apellido,
                FechaPermiso: request.Fecha
                )
            {
                TipoPermiso = tipo
            };

            _permisoRepository.Add(permiso);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result { Success = true };
        }
    }
}
