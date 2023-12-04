using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GetByRule
{
    public class GetByRuleRequest : IRequest<IEnumerable<EntityRuleModel>>
    {
        public int RuleId { get; private set; }

        public GetByRuleRequest(int ruleId)
        {
            RuleId = ruleId;
        }

    }
}