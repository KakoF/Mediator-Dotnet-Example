using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.Generate
{
    public class GenerateCommandHandler : IRequestHandler<GenerateRequest>
    {
        private readonly IAlertService _alertaGeradoService;
        public GenerateCommandHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        
        public async Task Handle(GenerateRequest request, CancellationToken cancellationToken)
        {
            await _alertaGeradoService.GenerateAsync(request.EntityRuleId, request.Quantity);
        }
    }
}