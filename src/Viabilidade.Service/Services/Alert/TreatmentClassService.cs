using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class TreatmentClassService : ITreatmentClassService
    {
        private readonly ITreatmentClassRepository _classeTratativaRepository;
        private readonly IStorageCache _cache;
        public TreatmentClassService(ITreatmentClassRepository classeTratativaRepository, IStorageCache cache)
        {
            _classeTratativaRepository = classeTratativaRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<TreatmentClassModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("TreatmentClass", () => _classeTratativaRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<TreatmentClassModel> GetAsync(int id)
        {
            return await _classeTratativaRepository.GetAsync(id);
        }
    }
}
