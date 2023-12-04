using MediatR;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Channel.GetBySubgroup
{
    public class GetBySubgroupQueryHandler : IRequestHandler<GetBySubgroupRequest, IEnumerable<ChannelModel>>
    {
        private readonly IChannelService _canalService;
        
        public GetBySubgroupQueryHandler(IChannelService canalService)
        {
            _canalService = canalService;
        }
        public async Task<IEnumerable<ChannelModel>> Handle(GetBySubgroupRequest request, CancellationToken cancellationToken)
        {
            return await _canalService.GetBySubgroupAsync(request.SubgroupId, true);
            
        }
    }
}