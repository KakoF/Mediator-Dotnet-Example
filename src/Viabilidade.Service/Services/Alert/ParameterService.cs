using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class ParameterService : IParameterService
    {
        private readonly IParameterRepository _parametroRepository;
        private readonly IStorageCache _cache;
        public ParameterService(IParameterRepository parametroRepository, IStorageCache cache)
        {
            _parametroRepository = parametroRepository;
            _cache = cache;
        }
        public async Task<ParameterModel> CreateAsync(ParameterModel model)
        {
            return await _parametroRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _parametroRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ParameterModel>> GetAsync(bool? active)
        {
            var model = await _cache.GetOrCreateAsync("Parameter", () => _parametroRepository.GetAsync());
            if (active == null)
                return model;
            return model.Where(x => x.Active == active);
        }

        public async Task<ParameterModel> GetAsync(int id)
        {
            return await _parametroRepository.GetAsync(id);
        }

        public async Task<ParameterModel> UpdateAsync(int id, ParameterModel model)
        {
            return await _parametroRepository.UpdateAsync(id, model);
        }
    }
}
