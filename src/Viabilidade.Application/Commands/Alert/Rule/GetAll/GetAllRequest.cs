using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.GetAll
{
    public class GetAllRequest : IRequest<IEnumerable<RuleModel>>
    {
    }
}
