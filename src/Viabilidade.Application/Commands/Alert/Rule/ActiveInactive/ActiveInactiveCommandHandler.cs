using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.ActiveInactive
{
    public class ActiveInactiveCommandHandler : IRequestHandler<SetActiveInactiveRequest<RuleModel>, RuleModel>
    {
        private readonly IRuleService _regraAlertaService;
        public ActiveInactiveCommandHandler(IRuleService regraAlertaService)
        {
            _regraAlertaService = regraAlertaService;
        }
        public async Task<RuleModel> Handle(SetActiveInactiveRequest<RuleModel> request, CancellationToken cancellationToken)
        {
            return await _regraAlertaService.ActiveInactiveAsync(request.Id, request.Active);
        }

    }
}