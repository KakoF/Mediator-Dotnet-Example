using MediatR;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.EntityRule;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GroupByEntity
{

    public class GroupByEntityRequest : IRequest<PaginationModel<EntityRuleGroupByEntityDto>>
    {
        public int EntityId { get; private set; }
        public EntityRuleQueryParams EntityRuleParams { get; private set; }

        public GroupByEntityRequest(int entityId, EntityRuleQueryParams entityRuleParams)
        {
            EntityId = entityId;
            EntityRuleParams = entityRuleParams;
        }
      
    }
}