using MediatR;
using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Pagination;

namespace Viabilidade.Application.Commands.Alert.Treatment.Group
{
    public class GroupQueryHandler : IRequestHandler<GroupRequest, PaginationModel<TreatmentGroupDto>>
    {
        private readonly ITreatmentService _alertaGeradoTratativaService;
        public GroupQueryHandler(ITreatmentService alertaGeradoTratativaService)
        {
            _alertaGeradoTratativaService = alertaGeradoTratativaService;
        }
        public async Task<PaginationModel<TreatmentGroupDto>> Handle(GroupRequest request, CancellationToken cancellationToken)
        {
           return await _alertaGeradoTratativaService.GroupAsync(request.QueryParams);
           
        }
    }
}