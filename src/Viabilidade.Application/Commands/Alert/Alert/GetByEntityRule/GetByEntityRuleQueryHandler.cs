using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.GetByEntityRule
{
    public class GetByEntityRuleQueryHandler : IRequestHandler<GetByEntityRuleRequest, IEnumerable<AlertModel>>
    {
        private readonly IAlertService _alertaGeradoService;
        public GetByEntityRuleQueryHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        public async Task<IEnumerable<AlertModel>> Handle(GetByEntityRuleRequest request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoService.GetByEntityRuleAsync(request.EntityRuleId, request.Treated);
        }
    }
}