using MediatR;
using Viabilidade.Domain.Interfaces.Services.Host;
using Viabilidade.Domain.Models.Client.Host;

namespace Viabilidade.Application.Commands.Host.GetUsers
{
    public class GetAllQueryHandler : IRequestHandler<GetAllRequest, UserListDetailModel>
    {
        private readonly IHostService _hostService;
        public GetAllQueryHandler(IHostService hostService)
        {
            _hostService = hostService;
        }
        public async Task<UserListDetailModel> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            return await _hostService.GetUsersAsync();

        }
    }
}