using MediatR;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Entity.GetBySegmentSquad
{
    public class GetBySegmentSquadQueryHandler : IRequestHandler<GetBySegmentSquadRequest, IEnumerable<EntityModel>>
    {
        private readonly IEntityService _entidadeService;
        
        public GetBySegmentSquadQueryHandler(IEntityService entidadeService)
        {
            _entidadeService = entidadeService;
        }
        public async Task<IEnumerable<EntityModel>> Handle(GetBySegmentSquadRequest request, CancellationToken cancellationToken)
        {
            return await _entidadeService.GetBySegmentSquadAsync(request.SquadId, request.SegmentId);
            
        }
    }
}