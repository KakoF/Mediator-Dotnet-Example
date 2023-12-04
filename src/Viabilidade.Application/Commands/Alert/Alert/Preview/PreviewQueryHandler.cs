using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.Preview
{
    public class PreviewQueryHandler : IRequestHandler<PreviewRequest<AlertModel>, AlertModel>
    {
        private readonly IAlertService _alertaGeradoService;
        
        public PreviewQueryHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        public async Task<AlertModel> Handle(PreviewRequest<AlertModel> request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoService.PreviewAsync(request.Id);
            
        }
    }
}