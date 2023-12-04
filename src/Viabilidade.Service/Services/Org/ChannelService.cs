using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Service.Services.Org
{
    public class ChannelService : IChannelService
    {

        private readonly IChannelRepository _canalRepository;
        private readonly IStorageCache _cache;
        public ChannelService(IChannelRepository canalRepository, IStorageCache cache)
        {
            _canalRepository = canalRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<ChannelModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Channel", () => _canalRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<ChannelModel> GetAsync(int id)
        {
            return await _canalRepository.GetAsync(id);
        }

        public async Task<IEnumerable<ChannelModel>> GetBySubgroupAsync(int subgroupId, bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Channel", () => _canalRepository.GetAsync());
            if (active == null)
                return models.Where(x => x.SubgroupId == subgroupId);
            return models.Where(x => x.SubgroupId == subgroupId && x.Active == active);
        }
    }
}
