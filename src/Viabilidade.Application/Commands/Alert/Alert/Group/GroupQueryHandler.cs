using MediatR;
using Viabilidade.Domain.DTO.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Pagination;

namespace Viabilidade.Application.Commands.Alert.Alert.Group
{
    public class GroupQueryHandler : IRequestHandler<GroupRequest, PaginationModel<AlertGroupDto>>
    {
        private readonly IAlertService _alertaGeradoService;
        public GroupQueryHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        public async Task<PaginationModel<AlertGroupDto>> Handle(GroupRequest request, CancellationToken cancellationToken)
        {
           return await _alertaGeradoService.GroupAsync(request.QueryParams);
           
        }
    }
}