using Domain.Abstractions;
using Domain.Permisos;
using Domain.Shared;
using MediatR;

namespace Application.Permisos.Create
{
    public class CreatePermisoCommandHandler : IRequestHandler<CreatePermisoCommand, Result>
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoPermisoRepository _tipoPermiso;
        public CreatePermisoCommandHandler(
            IPermisoRepository permisoRepository,
            IUnitOfWork unitOfWork,
            ITipoPermisoRepository tipoPermiso)
        {
            _permisoRepository = permisoRepository;
            _unitOfWork = unitOfWork;
            _tipoPermiso = tipoPermiso;
        }

        public async Task<Result> Handle(CreatePermisoCommand request, CancellationToken cancellationToken)
        {
            var tipo = _tipoPermiso.GetById(new Domain.TipoPermiso.TipoPermisoId(request.Tipo));

            if (tipo is null) return new Result("El tipo de permiso no existe");

            Permiso permiso = new(
                NombreEmpleado: request.Nombre,
                ApellidoEmpleado: request.Apellido,
                FechaPermiso: request.Fecha, 
                tipoPermiso: tipo);

            _permisoRepository.Add(permiso);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(permiso);
        }
    }
}
