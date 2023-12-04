using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class RuleActiveService : IRuleActiveService
    {
        private readonly IRuleActiveRepository _ruleActiveRepository;
        public RuleActiveService(IRuleActiveRepository ruleActiveRepository)
        {
            _ruleActiveRepository = ruleActiveRepository;
        }
        public async Task<RuleActiveModel> CreateAsync(RuleActiveModel model)
        {
            return await _ruleActiveRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteAsync(int ruleId)
        {
            return await _ruleActiveRepository.DeleteAsync(ruleId);
        }

    }
}
