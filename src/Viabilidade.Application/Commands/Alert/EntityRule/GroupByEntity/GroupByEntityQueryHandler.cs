using MediatR;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Pagination;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GroupByEntity
{
    public class GroupByEntityQueryHandler : IRequestHandler<GroupByEntityRequest, PaginationModel<EntityRuleGroupByEntityDto>>
    {
        private readonly IEntityRuleService _regraEntidadeService;
        
        public GroupByEntityQueryHandler(IEntityRuleService regraEntidadeService)
        {
            _regraEntidadeService = regraEntidadeService;
        }
        public async Task<PaginationModel<EntityRuleGroupByEntityDto>> Handle(GroupByEntityRequest request, CancellationToken cancellationToken)
        {
            return await _regraEntidadeService.GroupByEntityAsync(request.EntityId, request.EntityRuleParams);
            
        }
    }
}