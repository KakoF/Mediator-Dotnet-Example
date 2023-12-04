using MediatR;

namespace Viabilidade.Application.Commands.Alert.Alert.Generate
{
    public class GenerateRequest : IRequest
    {
        public GenerateRequest(int entityRuleId, int quantity)
        {
            EntityRuleId = entityRuleId;
            Quantity = quantity;
        }

        public int EntityRuleId { get; set; }
        public int Quantity { get; set; } = 1;
       
    }
}
