using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Service.Services.Org
{
    public class SegmentService : ISegmentService
    {
        private readonly ISegmentRepository _segmentoRepository;
        private readonly IStorageCache _cache;
        public SegmentService(ISegmentRepository segmentoRepository, IStorageCache cache)
        {
            _segmentoRepository = segmentoRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<SegmentModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Segment", () => _segmentoRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<SegmentModel> GetAsync(int id)
        {
            return await _segmentoRepository.GetAsync(id);
        }
    }
}
