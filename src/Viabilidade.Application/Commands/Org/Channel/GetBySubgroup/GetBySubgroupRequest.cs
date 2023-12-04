using MediatR;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Channel.GetBySubgroup
{
    public class GetBySubgroupRequest : IRequest<IEnumerable<ChannelModel>>
    {
        public int SubgroupId { get; private set; }

        public GetBySubgroupRequest(int subgroupId)
        {
            SubgroupId = subgroupId;
        }

    }
}