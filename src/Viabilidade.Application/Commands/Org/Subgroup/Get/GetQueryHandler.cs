using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Subgroup.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<SubgroupModel>, SubgroupModel>
    {
        private readonly ISubgroupService _tipoSubGrupoService;
        
        public GetQueryHandler(ISubgroupService tipoSubGrupoService)
        {
            _tipoSubGrupoService = tipoSubGrupoService;
        }
        public async Task<SubgroupModel> Handle(GetRequest<SubgroupModel> request, CancellationToken cancellationToken)
        {
            return await _tipoSubGrupoService.GetAsync(request.Id);
            
        }
    }
}