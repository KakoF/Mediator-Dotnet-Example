using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Repositories.Org
{
    public interface IChannelRepository: IBaseRepository<ChannelModel>
    {
        Task<IEnumerable<ChannelModel>> GetBySubgroupAsync(int subgroupId, bool? active = null);
    }
}
