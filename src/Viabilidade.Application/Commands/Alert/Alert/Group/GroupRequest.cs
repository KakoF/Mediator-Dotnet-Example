using MediatR;
using Viabilidade.Domain.DTO.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.Group
{
    public class GroupRequest : IRequest<PaginationModel<AlertGroupDto>>
    {
        public GroupRequest(AlertQueryParams queryParams)
        {
            QueryParams = queryParams;

        }
        public AlertQueryParams QueryParams { get; set; }
    }
}
