using Domain.Abstractions;
using Domain.Shared;
using MediatR;

namespace Application.Permisos.Get
{
    public class GetPermisosQueryHandler : IRequestHandler<GetPermisosQuery, Result>
    {
        private readonly IPermisoRepository _permisoRepository;

        public GetPermisosQueryHandler(IPermisoRepository permisoRepository)
        {
            _permisoRepository = permisoRepository;
        }

        public async Task<Result> Handle(GetPermisosQuery request, CancellationToken cancellationToken)
        {
            var res = new Result(await _permisoRepository.GetAll());
            return res;
        }
    }
}
