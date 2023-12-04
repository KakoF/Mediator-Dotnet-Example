using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.EntityRule.GetByEntity
{
    public class GetByEntityRequest : IRequest<IEnumerable<EntityRuleModel>>
    {
        public int EntityId { get; private set; }

        public GetByEntityRequest(int entityId)
        {
            EntityId = entityId;
        }

    }
}