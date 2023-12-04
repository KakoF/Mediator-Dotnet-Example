using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.GetByEntityRule
{
    public class GetByEntityRuleRequest : IRequest<IEnumerable<AlertModel>>
    {
        public GetByEntityRuleRequest(int entityRuleId, bool? treated)
        {
            EntityRuleId = entityRuleId;
            Treated = treated;
        }
        public int EntityRuleId { get; private set; }
        public bool? Treated { get; private set; }
    }
}
