using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Service.Services.Org
{
    public class SquadService : ISquadService
    {
        private readonly ISquadRepository _squadRepository;
        private readonly IStorageCache _cache;
        public SquadService(ISquadRepository squadRepository, IStorageCache cache)
        {
            _squadRepository = squadRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<SquadModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Squad", () => _squadRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<SquadModel> GetAsync(int id)
        {
            return await _squadRepository.GetAsync(id);
        }
    }
}
