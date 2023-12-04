using MediatR;
using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Interfaces.Services.Org;

namespace Viabilidade.Application.Commands.Org.Entity.GetBySquads
{
    public class GetBySquadsQueryHandler : IRequestHandler<GetBySquadsRequest, IEnumerable<EntitySquadDto>>
    {
        private readonly IEntityService _entidadeService;
        
        public GetBySquadsQueryHandler(IEntityService entidadeService)
        {
            _entidadeService = entidadeService;
        }

        public async Task<IEnumerable<EntitySquadDto>> Handle(GetBySquadsRequest request, CancellationToken cancellationToken)
        {
            return await _entidadeService.GetBySquadIdsAsync(request.SquadIds);    
        }
    }
}