using Domain.Abstractions;
using Domain.Shared;
using MediatR;

namespace Application.Permisos.Update
{
    public class UpdatePermisoCommandHandler : IRequestHandler<UpdatePermisoCommand, Result>
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoPermisoRepository _tipoPermiso;
        public UpdatePermisoCommandHandler(
            IPermisoRepository permisoRepository,
            IUnitOfWork unitOfWork,
            ITipoPermisoRepository tipoPermiso)
        {
            _permisoRepository = permisoRepository;
            _unitOfWork = unitOfWork;
            _tipoPermiso = tipoPermiso;
        }
        public async Task<Result> Handle(UpdatePermisoCommand request, CancellationToken cancellationToken)
        {
            var permiso = _permisoRepository.GetById(new Domain.Permisos.PermisoId(request.Id));
            if (permiso is null) return new Result("El permiso a actualizar no existe");

            var newtipo = _tipoPermiso.GetById(new Domain.TipoPermiso.TipoPermisoId(request.Tipo));
            if (newtipo is null) return new Result("Nuevo tipo de permiso no existe ");


            permiso.Update(request.Nombre, request.Apellido, request.Fecha, newtipo);

            _permisoRepository.Add(permiso);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(permiso);
        }
    }
}
