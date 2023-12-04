using MediatR;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Client.Host;

namespace Viabilidade.Application.Commands.Host.GetUsers
{
    public class GetAllRequest : IRequest<UserListDetailModel>
    {
    }
}
