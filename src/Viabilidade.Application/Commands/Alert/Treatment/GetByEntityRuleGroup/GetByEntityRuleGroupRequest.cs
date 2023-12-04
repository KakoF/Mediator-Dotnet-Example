using MediatR;
using Viabilidade.Domain.DTO.Treatment;

namespace Viabilidade.Application.Commands.Alert.Treatment.GetByEntityRuleGroup
{
    public class GetByEntityRuleGroupRequest : IRequest<IEnumerable<TreatmentByEntityRuleGroupDto>>
    {
        public GetByEntityRuleGroupRequest(int entityRuleId)
        {
            EntityRuleId = entityRuleId;
        }
        public int EntityRuleId { get; private set; }
    }
}
