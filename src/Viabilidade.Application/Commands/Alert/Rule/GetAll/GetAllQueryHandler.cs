using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllRequest, IEnumerable<RuleModel>>
    {
        private readonly IRuleService _regraAlertaService;
        public GetAllQueryHandler(IRuleService regraAlertaService)
        {
            _regraAlertaService = regraAlertaService;
        }
        public async Task<IEnumerable<RuleModel>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            return await _regraAlertaService.GetAsync();
        }

    }
}