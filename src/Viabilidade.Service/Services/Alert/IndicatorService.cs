using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class IndicatorService : IIndicatorService
    {

        private readonly IIndicatorRepository _IndicadorRepository;
        private readonly IStorageCache _cache;
        public IndicatorService(IIndicatorRepository IndicadorRepository, IStorageCache cache)
        {
            _IndicadorRepository = IndicadorRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<IndicatorModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Indicator", () => _IndicadorRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<IndicatorModel> GetAsync(int id)
        {
            return await _IndicadorRepository.GetAsync(id);
        }

        public async Task<IEnumerable<IndicatorModel>> GetBySegmentAsync(int segmentId, bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Indicator", () => _IndicadorRepository.GetAsync());
            if (active == null)
                return models.Where(x => x.SegmentId == segmentId);
            return models.Where(x => x.SegmentId == segmentId && x.Active == active);
        }

       
    }
}
