using MediatR;
using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Treatment;

namespace Viabilidade.Application.Commands.Alert.Treatment.Group
{
    public class GroupRequest : IRequest<PaginationModel<TreatmentGroupDto>>
    {
        public GroupRequest(TreatmentQueryParams queryParams)
        {
            QueryParams = queryParams;

        }
        public TreatmentQueryParams QueryParams { get; set; }
    }
}
