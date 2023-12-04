using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.Get
{
    public class PreviewQueryHandler : IRequestHandler<PreviewRequest<RuleModel>, RuleModel>
    {
        private readonly IRuleService _regraAlertaService;
        
        public PreviewQueryHandler(IRuleService regraAlertaService)
        {
            _regraAlertaService = regraAlertaService;
        }
        public async Task<RuleModel> Handle(PreviewRequest<RuleModel> request, CancellationToken cancellationToken)
        {
            return await _regraAlertaService.PreviewAsync(request.Id);
            
        }
    }
}