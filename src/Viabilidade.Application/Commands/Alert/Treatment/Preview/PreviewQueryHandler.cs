using MediatR;
using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Interfaces.Services.Alert;

namespace Viabilidade.Application.Commands.Alert.Treatment.Preview
{
    public class PreviewQueryHandler : IRequestHandler<PreviewRequest, TreatmentPreviewDto>
    {
        private readonly ITreatmentService _alertaGeradoTratativaService;
        
        public PreviewQueryHandler(ITreatmentService alertaGeradoTratativaService)
        {
            _alertaGeradoTratativaService = alertaGeradoTratativaService;
        }
        public async Task<TreatmentPreviewDto> Handle(PreviewRequest request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoTratativaService.PreviewAsync(request.Id);
        }
    }
}