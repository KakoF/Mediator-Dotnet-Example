using MediatR;
using Viabilidade.Domain.DTO.Entity;

namespace Viabilidade.Application.Commands.Org.Entity.Validate
{
    public class ValidateRequest : IRequest<EntityValidateDto>
    {
        public int SegmentId { get; set; }
        public IEnumerable<string> EntityIds { get; set; }

        public ValidateRequest(int segmentId, IEnumerable<string> entityIds)
        {
            SegmentId = segmentId;
            EntityIds = entityIds;
        }
    }
}