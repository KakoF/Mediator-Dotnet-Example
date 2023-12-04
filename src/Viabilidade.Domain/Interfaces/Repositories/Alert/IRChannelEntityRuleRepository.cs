using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IRChannelEntityRuleRepository
    {
        Task<RChannelEntityRuleModel> CreateAsync(RChannelEntityRuleModel entity);

        Task<bool> DeleteByEntityRuleAsync(IEnumerable<int> entityRuleIds);

        Task<RChannelEntityRuleModel> GetByEntityRuleAsync(int entityRuleId);
    }
}
