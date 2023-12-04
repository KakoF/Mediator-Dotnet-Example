using MediatR;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Entity.GetBySegmentSquad
{
    public class GetBySegmentSquadRequest : IRequest<IEnumerable<EntityModel>>
    {
        public int SquadId { get; private set; }
        public int SegmentId { get; private set; }

        public GetBySegmentSquadRequest(int squadId, int segmentId)
        {
            SquadId = squadId;
            SegmentId = segmentId;
        }

    }
}