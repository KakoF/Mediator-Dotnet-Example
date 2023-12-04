using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Channel.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<ChannelModel>, IEnumerable<ChannelModel>>
    {
        private readonly IChannelService _canalService;
        
        public GetAllQueryHandler(IChannelService canalService)
        {
            _canalService = canalService;
        }
        public async Task<IEnumerable<ChannelModel>> Handle(GetActivesRequest<ChannelModel> request, CancellationToken cancellationToken)
        {
            return await _canalService.GetAsync(request.Active);
        }
    }
}