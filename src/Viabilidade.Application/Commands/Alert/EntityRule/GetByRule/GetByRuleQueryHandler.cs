using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GetByRule
{
    public class GetByEntityQueryHandler : IRequestHandler<GetByRuleRequest, IEnumerable<EntityRuleModel>>
    {
        private readonly IEntityRuleService _regraEntidadeService;
        
        public GetByEntityQueryHandler(IEntityRuleService regraEntidadeService)
        {
            _regraEntidadeService = regraEntidadeService;
        }
        public async Task<IEnumerable<EntityRuleModel>> Handle(GetByRuleRequest request, CancellationToken cancellationToken)
        {
            return await _regraEntidadeService.GetByRuleAsync(request.RuleId);
            
        }
    }
}