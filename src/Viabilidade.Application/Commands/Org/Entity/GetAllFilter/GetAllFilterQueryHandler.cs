using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Entity.GetAllFilter
{
    public class GetAllFilterQueryHandler : IRequestHandler<GetAllFilterRequest, IEnumerable<EntityModel>>
    {
        private readonly IEntityService _entidadeService;
        
        public GetAllFilterQueryHandler(IEntityService entidadeService)
        {
            _entidadeService = entidadeService;
        }
        public async Task<IEnumerable<EntityModel>> Handle(GetAllFilterRequest request, CancellationToken cancellationToken)
        {
            return await _entidadeService.GetAllFilter(request.Id, request.Nome, request.EntidadeIdOriginal);
            
        }
    }
}