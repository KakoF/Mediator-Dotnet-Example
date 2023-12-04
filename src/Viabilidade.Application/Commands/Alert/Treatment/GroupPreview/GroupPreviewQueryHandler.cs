using MediatR;
using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Interfaces.Services.Alert;

namespace Viabilidade.Application.Commands.Alert.Treatment.GroupPreview
{
    public class GroupPreviewQueryHandler : IRequestHandler<GroupPreviewRequest, TreatmentGroupPreviewDto>
    {
        private readonly ITreatmentService _alertaGeradoTratativaService;
        
        public GroupPreviewQueryHandler(ITreatmentService alertaGeradoTratativaService)
        {
            _alertaGeradoTratativaService = alertaGeradoTratativaService;
        }
        public async Task<TreatmentGroupPreviewDto> Handle(GroupPreviewRequest request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoTratativaService.GroupPreviewAsync(request.Id, request.RegraAlertaEntidadeId);
        }
    }
}