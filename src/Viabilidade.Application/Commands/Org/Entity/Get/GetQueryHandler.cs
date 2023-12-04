using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Entity.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<EntityModel>, EntityModel>
    {
        private readonly IEntityService _entidadeService;
        
        public GetQueryHandler(IEntityService entidadeService)
        {
            _entidadeService = entidadeService;
        }
        public async Task<EntityModel> Handle(GetRequest<EntityModel> request, CancellationToken cancellationToken)
        {
            return await _entidadeService.GetAsync(request.Id);
            
        }
    }
}