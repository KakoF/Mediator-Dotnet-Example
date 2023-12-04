using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Service.Services.Org
{
    public class SubgroupService : ISubgroupService
    {
        private readonly ISubgroupRepository _tipoSubGrupoRepository;
        private readonly IStorageCache _cache;
        public SubgroupService(ISubgroupRepository tipoSubGrupoRepository, IStorageCache cache)
        {
            _tipoSubGrupoRepository = tipoSubGrupoRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<SubgroupModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Subgroup", () => _tipoSubGrupoRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<SubgroupModel> GetAsync(int id)
        {
            return await _tipoSubGrupoRepository.GetAsync(id);
        }
    }
}
