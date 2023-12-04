using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IRChannelEntityRuleService
    {
        Task<RChannelEntityRuleModel> CreateAsync(RChannelEntityRuleModel model);
        Task<bool> DeleteByEntityRuleAsync(IEnumerable<int> entityRuleIds);
    }
}
