using MediatR;
using Viabilidade.Domain.DTO.Rule;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Pagination;

namespace Viabilidade.Application.Commands.Alert.Rule.Group
{
    public class GroupQueryHandler : IRequestHandler<GroupRequest, PaginationModel<RuleGroupDto>>
    {
        private readonly IRuleService _regraAlertaService;
        public GroupQueryHandler(IRuleService regraAlertaService)
        {
            _regraAlertaService = regraAlertaService;
        }
        public async Task<PaginationModel<RuleGroupDto>> Handle(GroupRequest request, CancellationToken cancellationToken)
        {
           return await _regraAlertaService.GroupAsync(request.QueryParams);
           
        }
    }
}