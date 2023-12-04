using MediatR;
using Viabilidade.Domain.DTO.Rule;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Rule;

namespace Viabilidade.Application.Commands.Alert.Rule.Group
{
    public class GroupRequest : IRequest<PaginationModel<RuleGroupDto>>
    {
        public GroupRequest(RuleQueryParams queryParams)
        {
            QueryParams = queryParams;

        }
        public RuleQueryParams QueryParams { get; set; }
    }
}
