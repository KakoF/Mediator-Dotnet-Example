using MediatR;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Pagination;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GroupByRule
{
    public class GroupByRuleQueryHandler : IRequestHandler<GroupByRuleRequest, PaginationModel<EntityRuleGroupByRuleDto>>
    {
        private readonly IEntityRuleService _regraEntidadeService;
        
        public GroupByRuleQueryHandler(IEntityRuleService regraEntidadeService)
        {
            _regraEntidadeService = regraEntidadeService;
        }
        public async Task<PaginationModel<EntityRuleGroupByRuleDto>> Handle(GroupByRuleRequest request, CancellationToken cancellationToken)
        {
            return await _regraEntidadeService.GroupByRuleAsync(request.RuleId, request.EntityRuleParams);
            
        }
    }
}