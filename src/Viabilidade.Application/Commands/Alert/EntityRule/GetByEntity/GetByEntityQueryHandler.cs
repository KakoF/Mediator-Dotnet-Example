using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GetByEntity
{
    public class GetByEntityQueryHandler : IRequestHandler<GetByEntityRequest, IEnumerable<EntityRuleModel>>
    {
        private readonly IEntityRuleService _regraEntidadeService;
        
        public GetByEntityQueryHandler(IEntityRuleService regraEntidadeService)
        {
            _regraEntidadeService = regraEntidadeService;
        }
        public async Task<IEnumerable<EntityRuleModel>> Handle(GetByEntityRequest request, CancellationToken cancellationToken)
        {
            return await _regraEntidadeService.GetByEntityAsync(request.EntityId);
            
        }
    }
}