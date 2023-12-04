using MediatR;
using Viabilidade.Domain.DTO.Entity;

namespace Viabilidade.Application.Commands.Org.Entity.GetBySquads
{
    public class GetBySquadsRequest : IRequest<IEnumerable<EntitySquadDto>>
    {
        public IEnumerable<int> SquadIds { get; private set; }

        public GetBySquadsRequest(IEnumerable<int> squadIds)
        {
            SquadIds = squadIds;
        }
    }
}