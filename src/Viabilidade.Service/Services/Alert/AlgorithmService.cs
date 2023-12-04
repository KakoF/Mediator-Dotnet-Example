using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class AlgorithmService : IAlgorithmService
    {
        private readonly IAlgorithmRepository _algoritmoTipoRepository;
        private readonly IStorageCache _cache;
        public AlgorithmService(IAlgorithmRepository algoritmoTipoRepository, IStorageCache cache)
        {
            _algoritmoTipoRepository = algoritmoTipoRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<AlgorithmModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Algorithm", () => _algoritmoTipoRepository.GetAsync());
            if(active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<AlgorithmModel> GetAsync(int id)
        {
            return await _algoritmoTipoRepository.GetAsync(id);
        }
    }
}
