using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class OperatorService : IOperatorService
    {
        private readonly IOperatorRepository _operadorRepository;
        private readonly IStorageCache _cache;
        public OperatorService(IOperatorRepository operadorRepository, IStorageCache cache)
        {
            _operadorRepository = operadorRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<OperatorModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Operator", () => _operadorRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<OperatorModel> GetAsync(int id)
        {
            return await _operadorRepository.GetAsync(id);
        }
    }
}
