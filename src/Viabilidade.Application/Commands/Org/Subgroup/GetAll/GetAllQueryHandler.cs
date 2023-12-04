using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Subgroup.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<SubgroupModel>, IEnumerable<SubgroupModel>>
    {
        private readonly ISubgroupService _tipoSubGrupoService;
        
        public GetAllQueryHandler(ISubgroupService tipoSubGrupoService)
        {
            _tipoSubGrupoService = tipoSubGrupoService;
        }
        public async Task<IEnumerable<SubgroupModel>> Handle(GetActivesRequest<SubgroupModel> request, CancellationToken cancellationToken)
        {
            return await _tipoSubGrupoService.GetAsync(request.Active);
            
        }
    }
}