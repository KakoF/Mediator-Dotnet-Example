using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.GetAll
{
    public class GetAllRequest : IRequest<IEnumerable<AlertModel>>
    {
    }
}
