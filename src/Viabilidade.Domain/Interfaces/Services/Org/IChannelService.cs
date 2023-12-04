using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface IChannelService
    {
        Task<IEnumerable<ChannelModel>> GetAsync(bool? active);
        Task<ChannelModel> GetAsync(int id);
        Task<IEnumerable<ChannelModel>> GetBySubgroupAsync(int subgroupId, bool? active);
    }
}
