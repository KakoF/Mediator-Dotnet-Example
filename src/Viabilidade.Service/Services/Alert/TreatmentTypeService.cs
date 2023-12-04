using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class TreatmentTypeService : ITreatmentTypeService
    {

        private readonly ITreatmentTypeRepository _tipoTratativaRepository;
        private readonly IStorageCache _cache;
        public TreatmentTypeService(ITreatmentTypeRepository tipoTratativaRepository, IStorageCache cache)
        {
            _tipoTratativaRepository = tipoTratativaRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<TreatmentTypeModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("TreatmentType", () => _tipoTratativaRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<TreatmentTypeModel> GetAsync(int id)
        {
            return await _tipoTratativaRepository.GetAsync(id);
        }

        public async Task<IEnumerable<TreatmentTypeModel>> GetByTreatmentClassAsync(int treatmentClassId, bool? active)
        {
            var models = await _cache.GetOrCreateAsync("TreatmentType", () => _tipoTratativaRepository.GetAsync());
            if (active == null)
                return models.Where(x => x.TreatmentClassId == treatmentClassId);
            return models.Where(x => x.TreatmentClassId == treatmentClassId && x.Active == active);
        }

       
    }
}
