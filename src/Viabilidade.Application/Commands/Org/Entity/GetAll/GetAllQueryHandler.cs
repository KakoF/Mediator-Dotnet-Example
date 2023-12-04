using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Entity.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<EntityModel>, IEnumerable<EntityModel>>
    {
        private readonly IEntityService _entidadeService;
        
        public GetAllQueryHandler(IEntityService entidadeService)
        {
            _entidadeService = entidadeService;
        }
        public async Task<IEnumerable<EntityModel>> Handle(GetActivesRequest<EntityModel> request, CancellationToken cancellationToken)
        {
            return await _entidadeService.GetAsync(request.Active);
            
        }
    }
}