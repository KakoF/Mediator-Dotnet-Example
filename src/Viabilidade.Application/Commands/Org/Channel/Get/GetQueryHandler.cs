using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Channel.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<ChannelModel>, ChannelModel>
    {
        private readonly IChannelService _canalService;
        
        public GetQueryHandler(IChannelService canalService)
        {
            _canalService = canalService;
        }
        public async Task<ChannelModel> Handle(GetRequest<ChannelModel> request, CancellationToken cancellationToken)
        {
            return await _canalService.GetAsync(request.Id);
            
        }
    }
}