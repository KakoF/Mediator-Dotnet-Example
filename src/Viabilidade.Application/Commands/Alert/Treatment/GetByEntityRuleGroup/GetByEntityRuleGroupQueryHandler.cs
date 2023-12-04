using MediatR;
using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Interfaces.Services.Alert;

namespace Viabilidade.Application.Commands.Alert.Treatment.GetByEntityRuleGroup
{
    public class GetByEntityRuleGroupQueryHandler : IRequestHandler<GetByEntityRuleGroupRequest, IEnumerable<TreatmentByEntityRuleGroupDto>>
    {
        private readonly ITreatmentService _alertaGeradoTratativaService;
        public GetByEntityRuleGroupQueryHandler(ITreatmentService alertaGeradoTratativaService)
        {
            _alertaGeradoTratativaService = alertaGeradoTratativaService;
        }
        public async Task<IEnumerable<TreatmentByEntityRuleGroupDto>> Handle(GetByEntityRuleGroupRequest request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoTratativaService.GetByEntityRuleGroupAsync(request.EntityRuleId);
        }
    }
}