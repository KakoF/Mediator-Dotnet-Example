using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.SetRootRuleToRules
{
    public class SetRootRuleToRulesQueryHandler : IRequestHandler<SetRootRuleToRulesRequest>
    {
        private readonly IRuleService _regraAlertaService;
        public SetRootRuleToRulesQueryHandler(IRuleService regraAlertaService)
        {
            _regraAlertaService = regraAlertaService;
        }
        public async Task Handle(SetRootRuleToRulesRequest request, CancellationToken cancellationToken)
        {
            await _regraAlertaService.SetRootRuleToRulesAsync();
        }

    }
}