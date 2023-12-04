using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class IndicatorFilterService : IIndicatorFilterService
    {

        private readonly IIndicatorFilterRepository _filtroIndicadorRepository;
        private readonly IStorageCache _cache;
        public IndicatorFilterService(IIndicatorFilterRepository filtroIndicadorRepository, IStorageCache cache)
        {
            _filtroIndicadorRepository = filtroIndicadorRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<IndicatorFilterModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("IndicatorFilter", () => _filtroIndicadorRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<IndicatorFilterModel> GetAsync(int id)
        {
            return await _filtroIndicadorRepository.GetAsync(id);
        }
    }
}
