using MediatR;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.EntityRule;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GroupByRule
{
    public class GroupByRuleRequest : IRequest<PaginationModel<EntityRuleGroupByRuleDto>>
    {
        public int RuleId { get; private set; }
        public EntityRuleQueryParams EntityRuleParams { get; private set; }

        public GroupByRuleRequest(int ruleId, EntityRuleQueryParams entityRuleParams)
        {
            RuleId = ruleId;
            EntityRuleParams = entityRuleParams;
        }

    }
}