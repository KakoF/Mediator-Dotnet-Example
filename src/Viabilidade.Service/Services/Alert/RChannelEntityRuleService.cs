using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class RChannelEntityRuleService : IRChannelEntityRuleService
    {
        private readonly IRChannelEntityRuleRepository _rRegraEntidadeCanalRepository;
        public RChannelEntityRuleService(IRChannelEntityRuleRepository rRegraEntidadeCanalRepository)
        {
            _rRegraEntidadeCanalRepository = rRegraEntidadeCanalRepository;
        }
        public async Task<RChannelEntityRuleModel> CreateAsync(RChannelEntityRuleModel model)
        {
            return await _rRegraEntidadeCanalRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteByEntityRuleAsync(IEnumerable<int> entityRuleIds)
        {
            return await _rRegraEntidadeCanalRepository.DeleteByEntityRuleAsync(entityRuleIds);
        }
    }
}
